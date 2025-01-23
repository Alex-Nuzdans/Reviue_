using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConMan.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConMan.Pages;

public class RegistrationsModel : PageModel
{
    private ApplicationContext _context;
    public List<Registration> RegistrationsDb { get; private set; } = new();

    public RegistrationsModel(ApplicationContext db)
    {
        _context = db;
    }
    
    public void OnGet()
    {
        RegistrationsDb = _context.Registrations
            .Include(r => r.Status)
            .Include(r => r.Event)
            .Include(r => r.Participant)
            .OrderBy(r => r.Id)
            .AsNoTracking()
            .ToList();
    }
    
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var registration = await _context.Registrations.FindAsync(id);

        if (registration == null)
        {
            TempData["ErrorMessage"] = "Статус не найден.";
            return RedirectToPage();
        }

        try
        {
            _context.Registrations.Remove(registration);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            TempData["ErrorMessage"] = "Не удалось удалить регистрацию. Возможно, она используется в других записях.";
            return RedirectToPage();
        }

        return RedirectToPage();
    }
}