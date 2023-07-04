using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarRental.Data;
using CarRental.Data.Models;
using CarRental.Services.CarService;
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
            var user = _context.Users.FirstOrDefault(x => x.Id == User.GetId());
            if (!user.CanRent && !_context.RentalApproveRequests.Any(x=> x.UserId == user.Id))
            {
                return RedirectToAction("Create", "Tenants");
            }
            else if(!user.CanRent && _context.RentalApproveRequests.Any(x => x.UserId == user.Id))
            {
                return RedirectToAction("Info", "Tenants");
            }
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

            var log_res = new log_19118076
            {
                Table = "Reservation",
                Action = "Insert",
                ActionDate = DateTime.Now
            };
            _context.log_19118076.Add(log_res);
            _context.ReservedCars.Add(ReservedCar);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Reservations", new { type = "tenant"});
        }
    }
}
