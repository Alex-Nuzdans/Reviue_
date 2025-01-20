using ConMan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConMan.Pages;

[IgnoreAntiforgeryToken]
public class LocationsCreate : PageModel
{
    private ApplicationContext _context;
    
    [BindProperty] 
    public Location Location { get; set; } = new();

    public LocationsCreate(ApplicationContext db)
    {
        _context = db;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        _context.Locations.Add(Location);
        await _context.SaveChangesAsync();
        return RedirectToPage("/locations");
    }
}