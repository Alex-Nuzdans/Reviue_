using System.ComponentModel.DataAnnotations;

namespace ConMan.Models;

public class Registration
{
    public int Id { get; set; }
    
    [Required]
    public int EventId { get; set; }
    public Event Event { get; set; }
    
    [Required]
    public int ParticipantId { get; set; }
    public Participant Participant { get; set; }
    
    [Required]
    public int StatusId { get; set; }
    public Status Status { get; set; }
}