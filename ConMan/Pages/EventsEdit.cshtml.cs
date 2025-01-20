using ConMan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConMan.Pages
{
    public class EventsEdit : PageModel
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<RegistrationsEdit> _logger;

        [BindProperty]
        public Event Event { get; set; }

        public List<Location> Locations { get; set; } = new();

        public EventsEdit(ApplicationContext db, ILogger<RegistrationsEdit> logger)
        {
            _context = db;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Event = await _context.Events
                .Include(e => e.Location)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Event == null)
            {
                return NotFound();
            }

            Locations = await _context.Locations.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                Event.EnsureUtcDates();
                
                _context.Attach(Event).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EventExists(Event.Id))
                {
                    TempData["ErrorMessage"] = "Мероприятие не найдено.";
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

            return RedirectToPage("/events");
        }

        private async Task<bool> EventExists(int id)
        {
            return await _context.Events.AnyAsync(e => e.Id == id);
        }
    }
}