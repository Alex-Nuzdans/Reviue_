namespace ConMan.Models;

public class Status
{
    public int Id { get; set; }
    public string StatusName { get; set; }
    
    public ICollection<Registration> Registrations { get; set; }
}