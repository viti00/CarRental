using CarRental.Data.Models;
using CarRental.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using static CarRental.GlobalVariables;

namespace CarRental.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CarRental.Data.CarRentalDbContext _context;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, Data.CarRentalDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [BindProperty]
        public SearchTerm SearchTerm { get; set; }

        public void OnGet()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["EngineId"] = new SelectList(_context.Engines, "Id", "Type");
            ViewData["MakeId"] = new SelectList(_context.Makes, "Id", "Name");
            ViewData["TransmissionId"] = new SelectList(_context.Transmissions, "Id", "Type");
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
        }

        public IActionResult OnPost()
        {
            SearchTermData = SearchTerm;
            return RedirectToAction("Index", "Cars");
        }
    }
}