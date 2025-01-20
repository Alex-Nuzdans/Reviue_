namespace ConMan.Models;

public class Event
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public int LocationId { get; set; }
    public Location Location { get; set; }
    
    public ICollection<Registration> Registrations { get; set; }
    
    public void EnsureUtcDates()
    {
        if (Date.Kind != DateTimeKind.Utc)
        {
            Date = Date.ToUniversalTime().AddHours(5);
        }
    }
}