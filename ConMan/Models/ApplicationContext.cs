using Microsoft.EntityFrameworkCore;

namespace ConMan.Models;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }
    
    public DbSet<Registration> Registrations { get; set; } = null!;
    public DbSet<Event> Events { get; set; } = null!;
    public DbSet<Participant> Participants { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Настройка связей
        modelBuilder.Entity<Registration>()
            .HasOne(r => r.Event)
            .WithMany(e => e.Registrations)
            .HasForeignKey(r => r.EventId);

        modelBuilder.Entity<Registration>()
            .HasOne(r => r.Participant)
            .WithMany(p => p.Registrations)
            .HasForeignKey(r => r.ParticipantId);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=conman_db;Username=postgres;Password=admin;SslMode=Disable");
        }
    }
}