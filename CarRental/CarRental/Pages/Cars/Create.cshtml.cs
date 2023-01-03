﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarRental.Data;
using CarRental.Data.Models;
using CarRental.Servces.CarService;
using static CarRental.Infrastructure.ClaimsPrincipleExtension;

namespace CarRental.Pages.Cars
{
    public class CreateModel : PageModel
    {
        private readonly CarRental.Data.CarRentalDbContext _context;
        private readonly ICarService carService;

        public CreateModel(CarRental.Data.CarRentalDbContext context, ICarService carService)
        {
            _context = context;
            this.carService = carService;
        }

        public IActionResult OnGet()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["EngineId"] = new SelectList(_context.Engines, "Id", "Type");
            ViewData["MakeId"] = new SelectList(_context.Makes, "Id", "Name");
            ViewData["TransmissionId"] = new SelectList(_context.Transmissions, "Id", "Type");
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Car Car { get; set; }

        public async Task<IActionResult> OnPostAsync([FromForm] IFormFileCollection files)
        {
            Car.CreatorId = User.GetId();
            Car.Files = files;
            carService.ValidateCar(Car, ModelState);
            if (!ModelState.IsValid)
            {
                ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
                ViewData["EngineId"] = new SelectList(_context.Engines, "Id", "Type");
                ViewData["MakeId"] = new SelectList(_context.Makes, "Id", "Name");
                ViewData["TransmissionId"] = new SelectList(_context.Transmissions, "Id", "Type");
                ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
                return Page();
            }
            Car.Model = carService.GetModelById(int.Parse(Car.Model)).Name;
            Car.Photos = carService.CreatePhotos(Car.Files);

            _context.Cars.Add(Car);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
