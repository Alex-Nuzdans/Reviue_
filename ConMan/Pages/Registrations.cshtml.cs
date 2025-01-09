using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConMan.Models;

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
        RegistrationsDb = _context.Registrations.AsNoTracking().ToList();
    }
}