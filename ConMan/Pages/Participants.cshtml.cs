using ConMan.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConMan.Pages;

public class ParticipantsModel : PageModel
{
    private ApplicationContext _context;
    public List<Participant> ParticipantsDb { get; private set; } = new();

    public ParticipantsModel(ApplicationContext db)
    {
        _context = db;
    }
    
    public void OnGet()
    {
        ParticipantsDb = _context.Participants.AsNoTracking().ToList();
    }
}