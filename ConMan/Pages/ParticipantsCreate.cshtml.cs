using ConMan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConMan.Pages;

[IgnoreAntiforgeryToken]
public class ParticipantsCreate : PageModel
{
    private ApplicationContext _context;
    
    [BindProperty] 
    public Participant Participant { get; set; } = new();

    public ParticipantsCreate(ApplicationContext db)
    {
        _context = db;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        _context.Participants.Add(Participant);
        await _context.SaveChangesAsync();
        return RedirectToPage("/participants");
    }
}