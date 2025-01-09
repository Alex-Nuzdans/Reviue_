using ConMan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConMan.Pages;

[IgnoreAntiforgeryToken]
public class RegistrationsCreate : PageModel
{
    private ApplicationContext _context;
    
    [BindProperty] 
    public Registration Registration { get; set; } = new();

    public RegistrationsCreate(ApplicationContext db)
    {
        _context = db;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        _context.Registrations.Add(Registration);
        await _context.SaveChangesAsync();
        return RedirectToPage("/registrations");
    }
}