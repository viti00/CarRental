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
using CarRental.Services.TenantsService;
using Microsoft.AspNetCore.Authorization;

namespace CarRental.Pages.Tenants
{
    [Authorize]
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
            var user = _context.Users.FirstOrDefault(x => x.Id == User.GetId());
            if (user.CanRent)
            {
                return RedirectToPage("./Index");
            }
            if (!user.CanRent && _context.RentalApproveRequests.Any(x => x.UserId == user.Id))
            {
                return RedirectToAction("Info", "Tenants");
            }
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
                ModelState.AddModelError("File", "Задължително трябва да качите снимки!");
            }
            else if(RentalApproveRequest.Files.Count > 2)
            {
                ModelState.AddModelError("File", "Позволеният лимит на снимките е 2!");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            RentalApproveRequest.Photos = tenantService.CreatePhotos(RentalApproveRequest.Files, RentalApproveRequest.UserId);
            var user = _context.Users.FirstOrDefault(x => x.Id == User.GetId());
            if (user.CanRent)
            {
                return RedirectToPage("./Index");
            }

            _context.RentalApproveRequests.Add(RentalApproveRequest);
            await _context.SaveChangesAsync();

            return RedirectToAction("Info", "Tenants");
        }
    }
}
