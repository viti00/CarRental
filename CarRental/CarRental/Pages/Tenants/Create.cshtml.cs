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
using CarRental.Servces.TenantsService;

namespace CarRental.Pages.Tenants
{
    public class CreateModel : PageModel
    {
        private readonly CarRental.Data.CarRentalDbContext _context;
        private readonly ITenantService tenantService;

        public CreateModel(CarRental.Data.CarRentalDbContext context, ITenantService tenantService)
        {
            _context = context;
            this.tenantService = tenantService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public RentalApproveRequest RentalApproveRequest { get; set; }
        
        public async Task<IActionResult> OnPostAsync([FromForm] IFormFileCollection files)
        {
            RentalApproveRequest.Files = files;
            RentalApproveRequest.UserId = User.GetId();
            if(RentalApproveRequest.Files == null || RentalApproveRequest.Files.Count < 1)
            {
                ModelState.AddModelError("File", "Задължително трябва да качите снимки");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            RentalApproveRequest.Photos = tenantService.CreatePhotos(RentalApproveRequest.Files, RentalApproveRequest.UserId);

            _context.RentalApproveRequests.Add(RentalApproveRequest);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
