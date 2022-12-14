using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Data.Models;
using CarRental.Services.DealerService;
using Microsoft.AspNetCore.Authorization;
using static CarRental.Global.WebConstants;

namespace CarRental.Areas.Admin.Pages.Dealers
{
    [Area(AdminConstants.AreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public class IndexModel : PageModel
    {
        private readonly CarRental.Data.CarRentalDbContext _context;
        private readonly IDealerService dealerService;

        public IndexModel(CarRental.Data.CarRentalDbContext context, IDealerService dealerService)
        {
            _context = context;
            this.dealerService = dealerService;
        }

        public IList<DealerRequest> DealerRequest { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.DealerRequests != null)
            {
                DealerRequest = await _context.DealerRequests
                .Include(d => d.User).ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostEdit(int id)
        {
            var isSuccess = await dealerService.Approve(id);

            if (!isSuccess)
            {
                BadRequest();
            }

            return RedirectToAction("Index", "Dealers");
        }

        public IActionResult OnPostDelete(int id)
        {
            var isSuccess = dealerService.Reject(id);

            if (!isSuccess)
            {
                BadRequest();
            }

            return RedirectToAction("Index", "Dealers");
        }
    }
}
