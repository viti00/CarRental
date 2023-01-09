using CarRental.Data.Models;
using CarRental.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CarRental.Services.CarService
{
    public interface ICarService
    {
        public List<Model> GetModelsForGivenMake(int makeId);

        public void ValidateCar(Car car, ModelStateDictionary ms);

        public Model GetModelById(int modelId);

        public AllQueryCarsModel GetAllPerPage(IList<Car> cars, AllQueryCarsModel model);

        public List<CarPhoto> CreatePhotos(IFormFileCollection files);

        public bool ChekCarAvailable(ReservedCar reservedCar);

        public List<ReservedCar> GetReservedByDealer(string dealerId);

        public List<ReservedCar> GetMyReservations(string userId);

        public bool DeleteCar(string carId, string userId, bool isAdministrator);

        public void DeletePhotoById(int photoId);

        public void DeleteAllPhotos(string carId);

        public List<CarPhoto> GetAllPhotosForCar(string carId);
    }
}
