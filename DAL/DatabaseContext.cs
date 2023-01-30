using Microsoft.EntityFrameworkCore;

namespace DAL;

public class DatabaseContext : DbContext // todo: через транзакции
{

    public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
    { }
    
    public DbSet<Diary> Diaries { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");
        
        modelBuilder.Entity<Diary>()
            .HasMany(d => d.Notes)
            .WithOne(n => n.Diary)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Diaries)
            .WithOne(d => d.User)
            .OnDelete(DeleteBehavior.Cascade);
    }
}