using ConMan.Models;
using Microsoft.AspNetCore.Mvc;
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
        ParticipantsDb = _context.Participants
            .OrderBy(p => p.Id)
            .AsNoTracking()
            .ToList();
    }
    
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var participant = await _context.Participants.FindAsync(id);

        if (participant == null)
        {
            TempData["ErrorMessage"] = "Статус не найден.";
            return RedirectToPage();
        }

        try
        {
            _context.Participants.Remove(participant);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            TempData["ErrorMessage"] = "Не удалось удалить участника. Возможно, он используется в других записях.";
            return RedirectToPage();
        }

        return RedirectToPage();
    }
}