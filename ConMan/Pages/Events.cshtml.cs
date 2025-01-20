using ConMan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConMan.Pages;

public class EventsModel : PageModel
{
    private ApplicationContext _context;
    public List<Event> EventsDb { get; private set; } = new();

    public EventsModel(ApplicationContext db)
    {
        _context = db;
    }
    
    public void OnGet()
    {
        EventsDb = _context.Events
            .Include(e => e.Location)
            .OrderBy(e => e.Id)
            .AsNoTracking()
            .ToList();
    }
    
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var ev = await _context.Events.FindAsync(id);

        if (ev == null)
        {
            TempData["ErrorMessage"] = "Статус не найден.";
            return RedirectToPage();
        }

        try
        {
            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            TempData["ErrorMessage"] = "Не удалось удалить мероприятие. Возможно, оно используется в других записях.";
            return RedirectToPage();
        }

        return RedirectToPage();
    }
}