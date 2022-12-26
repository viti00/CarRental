using CarRental.Data.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CarRental.Servces.CarService
{
    public interface ICarService
    {
        public List<Model> GetModelsForGivenMake(int makeId);

        public void ValidateCar(Car car, ModelStateDictionary ms);

        public Model GetModelById(int modelId);
    }
}
