using ConMan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConMan.Pages;

[IgnoreAntiforgeryToken]
public class EventsCreate : PageModel
{
    private ApplicationContext _context;
    
    [BindProperty] 
    public Event Event { get; set; } = new();

    public EventsCreate(ApplicationContext db)
    {
        _context = db;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // Преобразование даты в UTC (потому что иначе ему не нравится)
        if (Event.Date.HasValue)
        {
            Event.Date = DateTime.SpecifyKind(Event.Date.Value, DateTimeKind.Utc);
        }
        
        _context.Events.Add(Event);
        await _context.SaveChangesAsync();
        return RedirectToPage("/events");
    }
}