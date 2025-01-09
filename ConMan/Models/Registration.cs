namespace ConMan.Models;

public class Registration
{
    public int Id { get; set; }
    
    public int EventId { get; set; }
    public Event Event { get; set; }
    
    public int ParticipantId { get; set; }
    public Participant Participant { get; set; }
    
    public string Status { get; set; }
}