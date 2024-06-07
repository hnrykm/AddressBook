using Backend.Interview.Api.Models;
using Microsoft.EntityFrameworkCore;

public class BackendInterviewDbContext : DbContext
{
    public BackendInterviewDbContext(DbContextOptions<BackendInterviewDbContext> options) : base(options)
    {
    }

    public DbSet<Person> People { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .OwnsOne(person => person.Address);
    }
}
