using ConMan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConMan.Pages
{
    public class StatusesEdit : PageModel
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<RegistrationsEdit> _logger;

        [BindProperty]
        public Status Status { get; set; }


        public StatusesEdit(ApplicationContext db, ILogger<RegistrationsEdit> logger)
        {
            _context = db;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Status = await _context.Statuses
                .FirstOrDefaultAsync(l => l.Id == id);

            if (Status == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                _context.Attach(Status).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await StatusExists(Status.Id))
                {
                    TempData["ErrorMessage"] = "Статус не найден.";
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

            return RedirectToPage("/statuses");
        }

        private async Task<bool> StatusExists(int id)
        {
            return await _context.Statuses.AnyAsync(l => l.Id == id);
        }
    }
}