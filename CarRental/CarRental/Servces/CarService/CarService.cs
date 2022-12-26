using CarRental.Data;
using CarRental.Data.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CarRental.Servces.CarService
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
    }
}
