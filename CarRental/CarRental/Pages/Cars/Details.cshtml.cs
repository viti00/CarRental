using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Data.Models;

namespace CarRental.Pages.Cars
{
    public class DetailsModel : PageModel
    {
        private readonly CarRental.Data.CarRentalDbContext _context;

        public DetailsModel(CarRental.Data.CarRentalDbContext context)
        {
            _context = context;
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
    }
}
