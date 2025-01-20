using ConMan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ConMan.Pages;

[IgnoreAntiforgeryToken]
public class RegistrationsCreate : PageModel
{
    private ApplicationContext _context;
    private ILogger<RegistrationsCreate> _logger;
    
    [BindProperty] 
    public Registration Registration { get; set; } = new();
    public List<Status> Statuses { get; set; } = new();

    public RegistrationsCreate(ApplicationContext db, ILogger<RegistrationsCreate> logger)
    {
        _context = db;
        _logger = logger;
    }

    public async Task OnGet()
    {
        Statuses = await _context.Statuses.ToListAsync();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            Registration.Event = await _context.Events.FindAsync(Registration.EventId);
            Registration.Participant = await _context.Participants.FindAsync(Registration.ParticipantId);
            Registration.Status = await _context.Statuses.FindAsync(Registration.StatusId);

            if (Registration.Event == null || Registration.Participant == null || Registration.Status == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid event, participant or status.");
                Statuses = await _context.Statuses.ToListAsync();
                return Page();
            }
            
            _context.Registrations.Add(Registration);
            await _context.SaveChangesAsync();
            
            return RedirectToPage("/registrations");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            Statuses = await _context.Statuses.ToListAsync();
            
            return Page();
        }
    }
}