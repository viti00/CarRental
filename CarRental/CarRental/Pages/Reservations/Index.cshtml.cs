using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Data.Models;
using CarRental.Infrastructure;
using CarRental.Services.CarService;
using Microsoft.AspNetCore.Authorization;
using ClosedXML.Excel;
using static CarRental.Global.WebConstants;

namespace CarRental.Pages.Reservations
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly CarRental.Data.CarRentalDbContext _context;
        private readonly ICarService carService;

        public IndexModel(CarRental.Data.CarRentalDbContext context, ICarService carService)
        {
            _context = context;
            this.carService = carService;
        }

        public IList<ReservedCar> ReservedCar { get;set; } = default!;

        public void OnGet(string type)
        {
            if(type == "dealer")
            {
                ReservedCar = carService.GetReservedByDealer(User.GetId());
                ViewData["type"] = type;
            }
            else if(type == "tenant")
            {
                ReservedCar = carService.GetMyReservations(User.GetId());
                ViewData["type"] = type;
            }
        }

        public async Task<IActionResult> OnPostDelete(string id)
        {
            var reservation  = await _context.ReservedCars.FirstOrDefaultAsync(x=>x.Id == id);

            if(reservation == null)
            {
                BadRequest();
            }

            _context.ReservedCars.Remove(reservation);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult OnGetExport()
        {
            var toExport = carService.GetReservedByDealer(User.GetId());

            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Резервации");

            worksheet.Cell(1, 1).Value = "Резервация №";
            worksheet.Cell(1, 2).Value = "Кола";
            worksheet.Cell(1, 3).Value = "Начална дата";
            worksheet.Cell(1, 4).Value = "Крайна дата";
            worksheet.Cell(1, 5).Value = "Име на наемател";
            worksheet.Cell(1, 6).Value = "Номер на наемател";
            worksheet.Cell(1, 7).Value = "Имейл на наемател";
            worksheet.Cell(1, 8).Value = "Статус";
            worksheet.Cell(1, 9).Value = "Дата на резервацията";

            for (int i = 0; i < toExport.Count; i++)
            {
                var res = toExport[i];
                worksheet.Cell(i + 2, 1).Value = res.Id;
                worksheet.Cell(i + 2, 2).Value = res.Car.Make + " - " + res.Car.Model + " " + res.Car.Year;
                worksheet.Cell(i + 2, 3).Value = res.StartDate.ToString("dd.MM.yyyy");
                worksheet.Cell(i + 2, 4).Value = res.EndDate.ToString("dd.MM.yyyy");
                worksheet.Cell(i + 2, 5).Value = res.Tenant.FirstName + " " + res.Tenant.LastName;
                worksheet.Cell(i + 2, 6).Value = res.Tenant.PhoneNumber;
                worksheet.Cell(i + 2, 7).Value = res.Tenant.Email;
                worksheet.Cell(i + 2, 8).Value = res.IsActive == true ? "Активна" : "Не активна";
                worksheet.Cell(i + 2, 9).Value = res.LastModified_19118076;
            }

            var fileName = "reservations.xlsx";
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
