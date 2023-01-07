using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarRental.Data.Models;
using CarRental.Services.CarService;
using static CarRental.Infrastructure.ClaimsPrincipleExtension;
using Microsoft.AspNetCore.Authorization;
using static CarRental.Global.WebConstants;

namespace CarRental.Pages.Cars
{
    [Authorize(Roles = DealerRoleName)]
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["EngineId"] = new SelectList(_context.Engines, "Id", "Type");
            ViewData["MakeId"] = new SelectList(_context.Makes, "Id", "Name");
            ViewData["TransmissionId"] = new SelectList(_context.Transmissions, "Id", "Type");
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Car Car { get; set; }

        public async Task<IActionResult> OnPostAsync([FromForm] IFormFileCollection files)
        {
            Car.CreatorId = User.GetId();
            Car.Files = files;
            carService.ValidateCar(Car, ModelState);
            if(Car.Files.Count > 20)
            {
                ModelState.AddModelError("Files", "Позволеният лимит на снимките е 20!");
            }
            if (!ModelState.IsValid)
            {
                ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
                ViewData["EngineId"] = new SelectList(_context.Engines, "Id", "Type");
                ViewData["MakeId"] = new SelectList(_context.Makes, "Id", "Name");
                ViewData["TransmissionId"] = new SelectList(_context.Transmissions, "Id", "Type");
                ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
                ViewData["Model"] = new SelectList(_context.Models.Where(x => x.MakeId == Car.MakeId), "Id", "Name");
                return Page();
            }
            Car.Model = carService.GetModelById(int.Parse(Car.Model)).Name;
            Car.Photos = carService.CreatePhotos(Car.Files);

            _context.Cars.Add(Car);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
