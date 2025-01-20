using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConMan.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConMan.Pages;

public class LocationsModel : PageModel
{
    private ApplicationContext _context;
    public List<Location> LocationsDb { get; private set; } = new();

    public LocationsModel(ApplicationContext db)
    {
        _context = db;
    }
    
    public void OnGet()
    {
        LocationsDb = _context.Locations
            .OrderBy(l => l.Id)
            .AsNoTracking()
            .ToList();
    }
    
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var location = await _context.Locations.FindAsync(id);

        if (location == null)
        {
            TempData["ErrorMessage"] = "Статус не найден.";
            return RedirectToPage();
        }

        try
        {
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            TempData["ErrorMessage"] = "Не удалось удалить место проведения. Возможно, оно используется в других записях.";
            return RedirectToPage();
        }

        return RedirectToPage();
    }
}