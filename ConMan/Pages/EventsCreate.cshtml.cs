using ConMan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConMan.Pages;

[IgnoreAntiforgeryToken]
public class EventsCreate : PageModel
{
    private ApplicationContext _context;
    
    [BindProperty] 
    public Event Event { get; set; } = new();
    public List<Location> Locations { get; set; } = new();

    public EventsCreate(ApplicationContext db)
    {
        _context = db;
    }

    public void OnGet(int id) 
    {
        Event = _context.Events
                .Include(e => e.Location)
                .FirstOrDefault(m => m.Id == id);

        Locations = _context.Locations.ToList();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Event.EnsureUtcDates();
        
        _context.Events.Add(Event);
        await _context.SaveChangesAsync();
        return RedirectToPage("/events");
    }
}