using CarRental.Data;
using CarRental.Data.Models;
using CarRental.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Services.CarService
{
    public class CarService : ICarService
    {
        private readonly CarRentalDbContext context;

        public CarService(CarRentalDbContext context)
        {
            this.context = context;
        }

        public Model GetModelById(int modelId)
            => context.Models.FirstOrDefault(x => x.Id == modelId);

        public List<Model> GetModelsForGivenMake(int makeId)
        {
            var models = context.Models.Where(x => x.MakeId == makeId).ToList();

            return models;
        }

        public void ValidateCar(Car car, ModelStateDictionary ms)
        {
            if(!context.Makes.Any(x=> x.Id == car.MakeId))
            {
                ms.AddModelError("Make", "Полето е задължително!");
            }
            if(!context.Models.Any(x=> x.Id == int.Parse(car.Model)))
            {
                ms.AddModelError("Model", "Полето е задължително!");
            }
            if(!context.Engines.Any(x=> x.Id == car.EngineId))
            {
                ms.AddModelError("Engine", "Полето е задължително!");
            }
            if (!context.Categories.Any(x => x.Id == car.CategoryId))
            {
                ms.AddModelError("Category", "Полето е задължително!");
            }
            if (!context.Transmissions.Any(x => x.Id == car.TransmissionId))
            {
                ms.AddModelError("Transmission", "Полето е задължително!");
            }
            if (!context.Cities.Any(x => x.Id == car.CityId))
            {
                ms.AddModelError("City", "Полето е задължително!");
            }
        }

        public AllQueryCarsModel GetAllPerPage(IList<Car> cars, AllQueryCarsModel model)
        {
            int totalCars;

            IList<Car> currPageCars;

            if(model.SearchTerm != null)
            {
                cars = FilterCarsBySearchTerm(cars, model.SearchTerm);
            }

            cars = model.Sorting switch
            {
                Sorting.Default => cars,
                Sorting.YearAcs => cars.OrderBy(x => x.Year).ToList(),
                Sorting.YearDesc => cars.OrderByDescending(x => x.Year).ToList(),
                Sorting.PriceAsc => cars.OrderBy(x => x.PricePerDay).ToList(),
                Sorting.PriceDesc => cars.OrderByDescending(x => x.PricePerDay).ToList(),
                _ => cars
            };

            totalCars = cars.Count;

            var maxPage = CalcMaxPage(totalCars, model.CarsPerPage);

            model.CurrentPage = GetCurrPage(model.CurrentPage, ref maxPage);

            currPageCars = cars
                .Skip((model.CurrentPage - 1) * model.CarsPerPage)
                .Take(model.CarsPerPage).ToList();

            foreach (var car in currPageCars)
            {
                car.PhotosCollection.AddRange(car.Photos.Select(x => Convert.ToBase64String(x.Bytes)).ToList());
            }

            var queryModel = new AllQueryCarsModel()
            {
                Cars = currPageCars,
                CarsPerPage = model.CarsPerPage,
                MaxPage = maxPage,
                CurrentPage = model.CurrentPage,
                SearchTerm = model.SearchTerm
            };

            return queryModel;
        }


        private IList<Car> FilterCarsBySearchTerm(IList<Car> cars, SearchTerm searchTerm)
        {
            if (GetMakeById(searchTerm.SearchByMake) != null)
            {
                cars = cars.Where(x => x.Make.Name.Contains(GetMakeById(searchTerm.SearchByMake).Name)).ToList();
            }
            if (GetModelById(searchTerm.SearchByModel) != null)
            {
                cars = cars.Where(x => x.Model.Contains(GetModelById(searchTerm.SearchByModel).Name)).ToList();
            }
            if (GetCityById(searchTerm.SearchByCity) != null)
            {
                cars = cars.Where(x => x.City.Name.Contains(GetCityById(searchTerm.SearchByCity).Name)).ToList();
            }
            if (GetEngineById(searchTerm.SearchByEngine) != null)
            {
                cars = cars.Where(x => x.Engine.Type.Contains(GetEngineById(searchTerm.SearchByEngine).Type)).ToList();
            }
            if (GetCategoryById(searchTerm.SearchByCategory) != null)
            {
                cars = cars.Where(x => x.Category.Name.Contains(GetCategoryById(searchTerm.SearchByCategory).Name)).ToList();
            }
            if (GetTransmissionById(searchTerm.SearchByTransmission) != null)
            {
                cars = cars.Where(x => x.Transmission.Type.Contains(GetTransmissionById(searchTerm.SearchByTransmission).Type)).ToList();
            }
            if (searchTerm.PricePerDayMin != null || searchTerm.PricePerDayMax != null)
            {
                if (searchTerm.PricePerDayMin != null && searchTerm.PricePerDayMax != null)
                {
                    cars = cars.Where(x => x.PricePerDay >= searchTerm.PricePerDayMin && x.PricePerDay <= searchTerm.PricePerDayMax).ToList();
                }
                else if(searchTerm.PricePerDayMin != null && searchTerm.PricePerDayMax == null)
                {
                    cars = cars.Where(x => x.PricePerDay >= searchTerm.PricePerDayMin).ToList();
                }
                else if (searchTerm.PricePerDayMax == null && searchTerm.PricePerDayMax != null)
                {
                    cars = cars.Where(x => x.PricePerDay <= searchTerm.PricePerDayMax).ToList();
                }
            }
            if(searchTerm.HorsePowerMin != null || searchTerm.HorsePowerMax != null)
            {
                if(searchTerm.HorsePowerMin != null && searchTerm.HorsePowerMax != null)
                {
                    cars = cars.Where(x => x.HorsePower >= searchTerm.HorsePowerMin && x.HorsePower <= searchTerm.HorsePowerMax).ToList();
                }
                else if(searchTerm.HorsePowerMin != null && searchTerm.HorsePowerMax == null)
                {
                    cars = cars.Where(x => x.HorsePower >= searchTerm.HorsePowerMin).ToList();
                }
                else if(searchTerm.HorsePowerMin == null && searchTerm.HorsePowerMax != null)
                {
                    cars = cars.Where(x => x.HorsePower <= searchTerm.HorsePowerMax).ToList();
                }
            }
            if (searchTerm.YearFrom != null || searchTerm.YearTo != null)
            {
                if(searchTerm.YearFrom != null && searchTerm.YearTo != null)
                {
                    cars = cars.Where(x => x.Year >= searchTerm.YearFrom && x.Year <= searchTerm.YearTo).ToList();
                }
                if (searchTerm.YearFrom != null || searchTerm.YearTo == null)
                {
                    cars = cars.Where(x => x.Year >= searchTerm.YearFrom).ToList();
                }
                if (searchTerm.YearFrom == null || searchTerm.YearTo != null)
                {
                    cars = cars.Where(x => x.Year <= searchTerm.YearTo).ToList();
                }
            }
            if (searchTerm.DateToTake != null || searchTerm.DateToReturn != null)
            {
                if (searchTerm.DateToTake != null && searchTerm.DateToReturn == null)
                {
                    cars = GetAllAvailableFromGivenDate(searchTerm.DateToTake, cars);
                }
                else
                {
                    cars = GetAllAvailableBetweenTwoDates(searchTerm.DateToTake, searchTerm.DateToReturn, cars);
                }
            }

            return cars;
        }

        private Make GetMakeById(int? id)
            => context.Makes.FirstOrDefault(x => x.Id == id);

        private Model GetModelById(int? id)
            => context.Models.FirstOrDefault(x => x.Id == id);

        private City GetCityById(int? id)
            => context.Cities.FirstOrDefault(x => x.Id == id);

        private Engine GetEngineById(int? id)
            => context.Engines.FirstOrDefault(x => x.Id == id);

        private Category GetCategoryById(int? id)
            => context.Categories.FirstOrDefault(x => x.Id == id);

        private Transmission GetTransmissionById(int? id)
            => context.Transmissions.FirstOrDefault(x => x.Id == id);

        private IList<Car> GetAllAvailableFromGivenDate(DateTime? startDate, IList<Car> cars)
        {
            IList<Car> carsToReturn = new List<Car>();

            foreach (var car in cars)
            {
                var reservations = context
                                   .ReservedCars
                                   .Where(x => x.CarId == car.Id
                                          && x.StartDate.Year == startDate.Value.Year)
                                   .ToList();
                if (reservations.Count > 0)
                {
                    if(!reservations.Any(x=> x.StartDate.Day == startDate.Value.Day
                                       && x.StartDate.Month == startDate.Value.Month &&
                                       x.StartDate.Year == startDate.Value.Year))
                    {
                        carsToReturn.Add(car);
                    }
                }
                else
                {
                    carsToReturn.Add(car);
                }
            }
            return carsToReturn;
        }

        private IList<Car> GetAllAvailableBetweenTwoDates(DateTime? startDate, DateTime? endDate,IList<Car> cars)
        {
            IList<Car> carsToReturn = new List<Car>();

            foreach (var car in cars)
            {
                var reservations = context
                                   .ReservedCars
                                   .Where(x => x.CarId == car.Id 
                                          && x.StartDate.Year == startDate.Value.Year
                                          || x.EndDate.Year == endDate.Value.Year)
                                   .ToList();
                if(reservations.Count > 0)
                {
                    if(!reservations.Any(x=> HasReservation(x.StartDate, x.EndDate, startDate, endDate)))
                    {
                        carsToReturn.Add(car);
                    }
                }
                else
                {
                    carsToReturn.Add(car);
                }
            }

            return carsToReturn;
        }

        public List<ReservedCar> GetReservedByDealer(string dealerId)
        {

            return context.ReservedCars
                .Include(c => c.Car).ThenInclude(c => c.Creator)
                .Include(c => c.Car).ThenInclude(c => c.Make)
                .Include(c=> c.Tenant)
                .Where(x => x.Car.CreatorId == dealerId)
                .ToList();
        }

        public List<ReservedCar> GetMyReservations(string userId)
        {
            return context.ReservedCars
                .Include(c => c.Car).ThenInclude(c=> c.Creator)
                .Include(c=> c.Car).ThenInclude(c=> c.Make)
                .Where(x => x.Tenant.Id == userId)
                .ToList();
        }

        public bool DeleteCar(string carId,string userId, bool isAdministrator)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == userId);
            var car = GetCarById(carId);

            if(user == null)
            {
                return false;
            }

            if(car == null)
            {
                return false;
            }

            if(!isAdministrator || user.Id != car.CreatorId)
            {
                return false;
            }

            try
            {
                context.RemoveRange(context.ReservedCars.Where(x => x.CarId == car.Id).ToList());
                context.Cars.Remove(car);
                context.SaveChanges();
            }
            catch (Exception)
            {

                return false;
            }

            return true;
        }

        public bool ChekCarAvailable(ReservedCar reservedCar)
        {
            var car = context.Cars.FirstOrDefault(x => x.Id == reservedCar.CarId);
            var reservations = context.ReservedCars.Where(x => x.CarId == reservedCar.CarId).ToList();

            if(car == null)
            {
                return false;
            }
            if(reservations.Count == 0)
            {
                return true;
            }

            if(!reservations.Any(x=> HasReservation(x.StartDate, x.EndDate, reservedCar.StartDate, reservedCar.EndDate)))
            {
                return true;
            }
            return false;
        }

        private bool HasReservation(DateTime reservationStartDate, DateTime reservationEndDate, DateTime? startDate, DateTime? endDate)
        {
            if((startDate.Value.Ticks >= reservationStartDate.Ticks && startDate.Value.Ticks <= reservationEndDate.Ticks)
                || endDate.Value.Ticks >= reservationStartDate.Ticks && endDate.Value.Ticks <= reservationEndDate.Ticks)
            {
                return true;
            }
            if ((reservationStartDate.Ticks >= startDate.Value.Ticks && reservationStartDate.Ticks <= endDate.Value.Ticks)
                || reservationEndDate.Ticks >= startDate.Value.Ticks && reservationEndDate.Ticks <= endDate.Value.Ticks)
            {
                return true;
            }
            return false;
        }

        private static int CalcMaxPage(int totalCars, int carsPerPage)
        {
            var maxPage = (int)Math.Ceiling((double)totalCars / carsPerPage);

            return maxPage;
        }

        private static int GetCurrPage(int currPage, ref int maxPage)
        {
            if (currPage > maxPage)
            {
                if (maxPage == 0)
                {
                    maxPage = 1;
                }
                currPage = maxPage;
            }
            if (currPage == 0)
            {
                currPage = 1;
            }

            return currPage;
        }

        public List<CarPhoto> CreatePhotos(IFormFileCollection files)
        {
            List<CarPhoto> photos = new List<CarPhoto>();
            Task.Run(async () =>
            {
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);

                            if (memoryStream.Length < 2097152)
                            {
                                var newphoto = new CarPhoto()
                                {
                                    Bytes = memoryStream.ToArray(),
                                    Description = file.FileName,
                                    FileExtension = Path.GetExtension(file.FileName),
                                    Size = file.Length,
                                };
                                photos.Add(newphoto);
                            }
                        }
                    }
                }
            }).GetAwaiter()
               .GetResult();

            return photos;
        }

        private Car GetCarById(string carId)
            => context.Cars.FirstOrDefault(x => x.Id == carId);
    }
}
