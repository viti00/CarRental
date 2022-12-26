using CarRental.Data.Models;
using CarRental.Servces.CarService;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Pages
{
    [ApiController]
    [Route("api/models")]
    public class ApiController : ControllerBase
    {
        private readonly ICarService carService;

        public ApiController(ICarService carService)
            => this.carService = carService;

        [HttpGet]
        [Route("{makeId}")]
        public List<Model> GetModels(int makeId)
            => carService.GetModelsForGivenMake(makeId);
    }
}
