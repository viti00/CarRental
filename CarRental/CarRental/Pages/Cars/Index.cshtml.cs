using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarRental.Data.Models;
using CarRental.ViewModels;

namespace CarRental.Pages.Cars
{
    public class IndexModel : PageModel
    {
        private readonly CarRental.Data.CarRentalDbContext _context;

        public IndexModel(CarRental.Data.CarRentalDbContext context)
        {
            _context = context;
        }

        public IList<Car> Car { get;set; } = default!;

        public async Task OnGetAsync(CarsSearchViewModel searchTerm)
        {
            if (_context.Cars != null)
            {
                Car = await _context.Cars
                .Include(c => c.Category)
                .Include(c => c.Engine)
                .Include(c => c.Make)
                .Include(c => c.Transmission).ToListAsync();
            }
        }
    }
}
