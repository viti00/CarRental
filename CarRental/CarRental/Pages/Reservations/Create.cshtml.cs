using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarRental.Data;
using CarRental.Data.Models;
using CarRental.Servces.CarService;
using CarRental.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace CarRental.Pages.Reservations
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly CarRental.Data.CarRentalDbContext _context;
        private readonly ICarService carService;

        public CreateModel(CarRental.Data.CarRentalDbContext context, ICarService carService)
        {
            _context = context;
            this.carService = carService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ReservedCar ReservedCar { get; set; }

        public async Task<IActionResult> OnPostAsync(string carId)
        {
            ReservedCar.CarId = carId;

            if (!carService.ChekCarAvailable(ReservedCar))
            {
                ModelState.AddModelError("NotAvailable", "Автомобилът вече е зает за избраните от вас дати");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = _context.Users.FirstOrDefault(x => x.Id == User.GetId());

            if(user == null)
            {
                return BadRequest();
            }

            ReservedCar.Tenant = user;


            _context.ReservedCars.Add(ReservedCar);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Reservations", new { type = "tenant"});
        }
    }
}
