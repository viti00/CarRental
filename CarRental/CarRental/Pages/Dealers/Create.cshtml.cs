using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarRental.Data;
using CarRental.Data.Models;
using CarRental.Infrastructure;

namespace CarRental.Pages.Dealers
{
    public class CreateModel : PageModel
    {
        private readonly CarRental.Data.CarRentalDbContext _context;

        public CreateModel(CarRental.Data.CarRentalDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DealerRequest DealerRequest { get; set; }
        
        public async Task<IActionResult> OnPostAsync()
        {
            DealerRequest.UserId = User.GetId();

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(c => c.ErrorMessage)).ToList();
                return Page();
            }

            _context.DealerRequests.Add(DealerRequest);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
