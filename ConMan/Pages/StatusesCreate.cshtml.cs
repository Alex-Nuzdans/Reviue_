using ConMan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConMan.Pages;

[IgnoreAntiforgeryToken]
public class StatusesCreate : PageModel
{
    private ApplicationContext _context;
    
    [BindProperty] 
    public Status Status { get; set; } = new();

    public StatusesCreate(ApplicationContext db)
    {
        _context = db;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        _context.Statuses.Add(Status);
        await _context.SaveChangesAsync();
        return RedirectToPage("/statuses");
    }
}