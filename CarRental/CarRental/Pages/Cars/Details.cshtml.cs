using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Data.Models;
using CarRental.Servces.CarService;
using CarRental.Infrastructure;
using static CarRental.WebConstants;

namespace CarRental.Pages.Cars
{
    public class DetailsModel : PageModel
    {
        private readonly CarRental.Data.CarRentalDbContext _context;
        private readonly ICarService carService;

        public DetailsModel(CarRental.Data.CarRentalDbContext context, ICarService carService)
        {
            _context = context;
            this.carService = carService;
        }

        public Car Car { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context
                      .Cars
                      .Include(c => c.Category)
                      .Include(c => c.Engine)
                      .Include(c => c.Make)
                      .Include(c => c.City)
                      .Include(c => c.Transmission)
                      .Include(c => c.Photos)
                      .Include(c=> c.Creator)
                      .FirstOrDefaultAsync(m => m.Id == id);
            car.PhotosCollection = car.Photos.Select(x => Convert.ToBase64String(x.Bytes)).ToList();
            if (car == null)
            {
                return NotFound();
            }
            else 
            {
                Car = car;
            }
            return Page();
        }

        public IActionResult OnPostDelete(string carId)
        {
            var isAdministrator = User.IsInRole(AdministratorRoleName);
            if (!carService.DeleteCar(carId, User.GetId(), isAdministrator))
            {
                return BadRequest();
            }

            return RedirectToAction("Index", "Cars");
        }
    }
}
