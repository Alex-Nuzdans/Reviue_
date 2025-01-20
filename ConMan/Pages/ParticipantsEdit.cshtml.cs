using ConMan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConMan.Pages
{
    public class ParticipantsEdit : PageModel
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<RegistrationsEdit> _logger;

        [BindProperty]
        public Participant Participant { get; set; }


        public ParticipantsEdit(ApplicationContext db, ILogger<RegistrationsEdit> logger)
        {
            _context = db;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Participant = await _context.Participants
                .FirstOrDefaultAsync(p => p.Id == id);

            if (Participant == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                _context.Attach(Participant).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ParticipantExists(Participant.Id))
                {
                    TempData["ErrorMessage"] = "Участник не найден.";
                    return RedirectToPage();
                }
                else
                {
                    TempData["ErrorMessage"] = "Произошла непредвиденная ошибка.";
                    return RedirectToPage();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Произошла непредвиденная ошибка. Проверьте, правильно ли вы ввели данные.";
                return RedirectToPage();
            }

            return RedirectToPage("/participants");
        }

        private async Task<bool> ParticipantExists(int id)
        {
            return await _context.Participants.AnyAsync(p => p.Id == id);
        }
    }
}