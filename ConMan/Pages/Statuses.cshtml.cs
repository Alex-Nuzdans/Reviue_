using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConMan.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConMan.Pages;

public class StatusesModel : PageModel
{
    private ApplicationContext _context;
    public List<Status> StatusesDb { get; private set; } = new();

    public StatusesModel(ApplicationContext db)
    {
        _context = db;
    }
    
    public void OnGet()
    {
        StatusesDb = _context.Statuses
            .OrderBy(s => s.Id)
            .AsNoTracking()
            .ToList();
    }
    
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var status = await _context.Statuses.FindAsync(id);

        if (status == null)
        {
            TempData["ErrorMessage"] = "Статус не найден.";
            return RedirectToPage();
        }

        try
        {
            _context.Statuses.Remove(status);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            TempData["ErrorMessage"] = "Не удалось удалить статус. Возможно, он используется в других записях.";
            return RedirectToPage();
        }

        return RedirectToPage();
    }
}