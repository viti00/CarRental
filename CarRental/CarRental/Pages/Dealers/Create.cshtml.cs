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
using Microsoft.AspNetCore.Authorization;
using static CarRental.WebConstants;

namespace CarRental.Pages.Dealers
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly CarRental.Data.CarRentalDbContext _context;

        public CreateModel(CarRental.Data.CarRentalDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if (User.IsInRole(DealerRoleName))
            {
                return RedirectToAction("Index", "Home");
            }
            if(!User.IsInRole(DealerRoleName) && _context.DealerRequests.Any(x=> x.UserId == User.GetId()))
            {
                return RedirectToAction("Info", "Dealers");
            }
            return Page();
        }

        [BindProperty]
        public DealerRequest DealerRequest { get; set; }
        
        public async Task<IActionResult> OnPostAsync()
        {
            DealerRequest.UserId = User.GetId();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.DealerRequests.Add(DealerRequest);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
