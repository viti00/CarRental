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
using ClosedXML.Excel;

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

        public IActionResult OnGetExport()
        {
            var toExport = _context.RentalApproveRequests.Include(x => x.User).ToList();

            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Заявки за наемател");

            worksheet.Cell(1, 1).Value = "Заявка №";
            worksheet.Cell(1, 2).Value = "Име";
            worksheet.Cell(1, 3).Value = "Номер";
            worksheet.Cell(1, 4).Value = "Имейл";
            worksheet.Cell(1, 5).Value = "Книжка №";
            worksheet.Cell(1, 6).Value = "Дата на заявка";
            worksheet.Cell(1, 7).Value = "Статус";

            for (int i = 0; i < toExport.Count; i++)
            {
                var rq = toExport[i];
                worksheet.Cell(i + 2, 1).Value = rq.Id;
                worksheet.Cell(i + 2, 2).Value = rq.FirstName + " " + rq.LastName;
                worksheet.Cell(i + 2, 3).Value = rq.PhoneNumber;
                worksheet.Cell(i + 2, 4).Value = rq.User.Email;
                worksheet.Cell(i + 2, 5).Value = rq.DrivingLicenseNumber;
                worksheet.Cell(i + 2, 6).Value = rq.LastModified_19118076;
                worksheet.Cell(i + 2, 7).Value = rq.Status;
            }

            var fileName = "tenantRequests.xlsx";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);
            workbook.SaveAs(filePath);

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;

            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }
}
