using CarRental.Services.CarService;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Pages
{
    [ApiController]
    [Route("api/All")]
    public class ApiDeleteAll
    {
        private readonly ICarService carService;

        public ApiDeleteAll(ICarService carService)
            => this.carService = carService;

        [HttpGet]
        [Route("{carId}")]
        public void DeletePhoto(string carId)
            => carService.DeleteAllPhotos(carId);
    }
}
