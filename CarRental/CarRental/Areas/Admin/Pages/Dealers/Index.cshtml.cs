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
using ClosedXML.Excel;

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

        public IActionResult OnGetExport()
        {
            var toExport = _context.DealerRequests.Include(x => x.User).ToList();

            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Заявки за дилър");

            worksheet.Cell(1, 1).Value = "Заявка №";
            worksheet.Cell(1, 2).Value = "Име";
            worksheet.Cell(1, 3).Value = "Номер";
            worksheet.Cell(1, 4).Value = "Имейл";
            worksheet.Cell(1, 5).Value = "Дата на заявка";
            worksheet.Cell(1, 6).Value = "Статус";

            for (int i = 0; i < toExport.Count; i++)
            {
                var rq = toExport[i];
                worksheet.Cell(i + 2, 1).Value = rq.Id;
                worksheet.Cell(i + 2, 2).Value = rq.FirstName + " " + rq.LastName;
                worksheet.Cell(i + 2, 3).Value = rq.PhoneNumber;
                worksheet.Cell(i + 2, 4).Value = rq.User.Email;
                worksheet.Cell(i + 2, 5).Value = rq.LastModified_19118076;
                worksheet.Cell(i + 2, 6).Value = rq.Status;
            }

            var fileName = "dealersRequest.xlsx";
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
