using CarRental.Services.CarService;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Pages
{
    [ApiController]
    [Route("api/delete")]
    public class ApiDeletePhotoController
    {
        private readonly ICarService carService;

        public ApiDeletePhotoController(ICarService carService)
            => this.carService = carService;

        [HttpGet]
        [Route("{photoId}")]
        public void DeletePhoto(int photoId)
            => carService.DeletePhotoById(photoId);
    }
}
