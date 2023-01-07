using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarRental.Data.Models;
using CarRental.Services.CarService;
using static CarRental.Global.GlobalVariables;
using CarRental.ViewModels;

namespace CarRental.Pages.Cars
{
    public class IndexModel : PageModel
    {
        private readonly CarRental.Data.CarRentalDbContext _context;
        private readonly ICarService carService;

        public IndexModel(CarRental.Data.CarRentalDbContext context, ICarService carService)
        {
            _context = context;
            this.carService = carService;
        }

        public IList<Car> Car { get;set; } = default!;

        public AllQueryCarsModel QueryModel { get; set; }

        public async Task OnGetAsync(AllQueryCarsModel model)
        {
            model.SearchTerm = SearchTermData;
            if (_context.Cars != null)
            {
                Car = await _context.Cars
                .Include(c => c.Category)
                .Include(c => c.Engine)
                .Include(c => c.Make)
                .Include(c=> c.City)
                .Include(c => c.Transmission)
                .Include(c=> c.Photos)
                .ToListAsync();
            }
            QueryModel = carService.GetAllPerPage(Car, model);
            QueryModel.Sorting = model.Sorting;
            Car = QueryModel.Cars;
        }
    }
}
