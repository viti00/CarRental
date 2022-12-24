using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarRental.Data;
using CarRental.Data.Models;

namespace CarRental.Pages.Cars
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
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name").OrderBy(x=> x.Text);
        ViewData["EngineId"] = new SelectList(_context.Engines, "Id", "Type").OrderBy(x => x.Text);
        ViewData["MakeId"] = new SelectList(_context.Makes, "Id", "Name").OrderBy(x => x.Text);
        ViewData["TransmissionId"] = new SelectList(_context.Transmissions, "Id", "Type").OrderBy(x => x.Text);
            return Page();
        }

        [BindProperty]
        public Car Car { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Cars.Add(Car);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
