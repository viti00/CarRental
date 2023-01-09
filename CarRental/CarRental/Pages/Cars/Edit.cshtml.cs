using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRental.Data.Models;
using CarRental.Services.CarService;
using CarRental.Infrastructure;
using NuGet.Packaging;

namespace CarRental.Pages.Cars
{
    public class EditModel : PageModel
    {
        private readonly CarRental.Data.CarRentalDbContext _context;
        private readonly ICarService carService;

        public EditModel(CarRental.Data.CarRentalDbContext context, ICarService carService)
        {
            _context = context;
            this.carService = carService;
        }

        [BindProperty]
        public Car Car { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car =  await _context.Cars.Include(x=> x.Photos).FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            if(car.CreatorId != User.GetId())
            {
                return BadRequest();
            }
            Car = car;
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["EngineId"] = new SelectList(_context.Engines, "Id", "Type");
            ViewData["MakeId"] = new SelectList(_context.Makes, "Id", "Name");
            ViewData["TransmissionId"] = new SelectList(_context.Transmissions, "Id", "Type");
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            ViewData["Model"] = new SelectList(_context.Models.Where(x => x.MakeId == Car.MakeId), "Name", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync([FromForm] IFormFileCollection files, string id)
        {
            Car.Photos = carService.GetAllPhotosForCar(id);
            Car.Id = id;
            Car.Files = files;
            Car.CreatorId = User.GetId();
            carService.ValidateCar(Car, ModelState);
            if(Car.Photos.Count + Car.Files.Count > 20)
            {
                ModelState.AddModelError("Files", $"Лимитът на снимките е 20! Може да качите още {20 - Car.Photos.Count}");
            }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(c => c.ErrorMessage)).ToList();
                ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
                ViewData["EngineId"] = new SelectList(_context.Engines, "Id", "Type");
                ViewData["MakeId"] = new SelectList(_context.Makes, "Id", "Name");
                ViewData["TransmissionId"] = new SelectList(_context.Transmissions, "Id", "Type");
                ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
                ViewData["Model"] = new SelectList(_context.Models.Where(x => x.MakeId == Car.MakeId), "Name", "Name");
                return Page();
            }
            Car.Photos.AddRange(carService.CreatePhotos(Car.Files));

            _context.Attach(Car).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(Car.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CarExists(string id)
        {
          return _context.Cars.Any(e => e.Id == id);
        }
    }
}
