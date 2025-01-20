using ConMan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConMan.Pages
{
    public class RegistrationsEdit : PageModel
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<RegistrationsEdit> _logger;

        [BindProperty]
        public Registration Registration { get; set; }

        public List<Status> Statuses { get; set; } = new();

        public RegistrationsEdit(ApplicationContext db, ILogger<RegistrationsEdit> logger)
        {
            _context = db;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Registration = await _context.Registrations
                .Include(r => r.Status)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Registration == null)
            {
                return NotFound();
            }

            Statuses = await _context.Statuses.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                _context.Attach(Registration).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RegistrationExists(Registration.Id))
                {
                    TempData["ErrorMessage"] = "Регистрация не найдена.";
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

            return RedirectToPage("/registrations");
        }

        private async Task<bool> RegistrationExists(int id)
        {
            return await _context.Registrations.AnyAsync(e => e.Id == id);
        }
    }
}