using ConMan.Models;
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
        EventsDb = _context.Events.AsNoTracking().ToList();
    }
}