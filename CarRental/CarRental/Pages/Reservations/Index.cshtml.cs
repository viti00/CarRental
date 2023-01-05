using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Data.Models;
using CarRental.Infrastructure;
using CarRental.Servces.CarService;
using Microsoft.AspNetCore.Authorization;

namespace CarRental.Pages.Reservations
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly CarRental.Data.CarRentalDbContext _context;
        private readonly ICarService carService;

        public IndexModel(CarRental.Data.CarRentalDbContext context, ICarService carService)
        {
            _context = context;
            this.carService = carService;
        }

        public IList<ReservedCar> ReservedCar { get;set; } = default!;

        public void OnGet(string type)
        {
            if(type == "dealer")
            {
                ReservedCar = carService.GetReservedByDealer(User.GetId());
                ViewData["type"] = type;
            }
            else if(type == "tenant")
            {
                ReservedCar = carService.GetMyReservations(User.GetId());
                ViewData["type"] = type;
            }
        }

        public async Task<IActionResult> OnPostDelete(string id)
        {
            var reservation  = await _context.ReservedCars.FirstOrDefaultAsync(x=>x.Id == id);

            if(reservation == null)
            {
                BadRequest();
            }

            _context.ReservedCars.Remove(reservation);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
