using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Data.Models;
using System.ComponentModel.DataAnnotations;
using CarRental.Services.TenantsService;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using static CarRental.Global.WebConstants;

namespace CarRental.Areas.Admin.Pages.Tenants
{
    [Area(AdminConstants.AreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public class IndexModel : PageModel
    {
        private readonly CarRental.Data.CarRentalDbContext _context;
        private readonly ITenantService tenantService;

        public IndexModel(CarRental.Data.CarRentalDbContext context, ITenantService tenantService)
        {
            _context = context;
            this.tenantService = tenantService;
        }

        public IList<RentalApproveRequest> RentalApproveRequest { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.RentalApproveRequests != null)
            {
                RentalApproveRequest = await _context.RentalApproveRequests
                .Include(r => r.User)
                .Include(r=> r.Photos)
                .ToListAsync();

                foreach (var tenant in RentalApproveRequest)
                {
                    tenant.PhotosCollection = tenant.Photos.Select(x => Convert.ToBase64String(x.Bytes)).ToList();
                }
            }
        }
        public IActionResult OnPostEdit(int id)
        {
            var isSuccess = tenantService.Approve(id);

            if (!isSuccess)
            {
                return BadRequest();
            }

            return RedirectToAction("Index", "Tenants");
        }
        public IActionResult OnPostDelete(int id)
        {
            var isSuccess = tenantService.Reject(id);

            if (!isSuccess)
            {
                return BadRequest();
            }

            return RedirectToAction("Index", "Tenants");
        }
    }
}
