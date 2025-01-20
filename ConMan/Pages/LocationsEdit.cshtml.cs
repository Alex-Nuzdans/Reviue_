using ConMan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConMan.Pages
{
    public class LocationsEdit : PageModel
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<RegistrationsEdit> _logger;

        [BindProperty]
        public Location Location { get; set; }


        public LocationsEdit(ApplicationContext db, ILogger<RegistrationsEdit> logger)
        {
            _context = db;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Location = await _context.Locations
                .FirstOrDefaultAsync(l => l.Id == id);

            if (Location == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                _context.Attach(Location).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await LocationExists(Location.Id))
                {
                    TempData["ErrorMessage"] = "Место проведения не найдено.";
                    return RedirectToPage();
                }
                else
                {
                    TempData["ErrorMessage"] = "Произошла непредвиденная ошибка.";
                    return RedirectToPage();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Произошла непредвиденная ошибка. Проверьте, правильно ли вы ввели данные.";
                return RedirectToPage();
            }

            return RedirectToPage("/locations");
        }

        private async Task<bool> LocationExists(int id)
        {
            return await _context.Locations.AnyAsync(l => l.Id == id);
        }
    }
}