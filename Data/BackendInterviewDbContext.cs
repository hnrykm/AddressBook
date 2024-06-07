using Backend.Interview.Api.Models;
using Microsoft.EntityFrameworkCore;

public class BackendInterviewDbContext : DbContext
{
    public BackendInterviewDbContext(DbContextOptions<BackendInterviewDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .OwnsOne(person => person.Address);
    }
    public DbSet<Person> People { get; set; }
}
