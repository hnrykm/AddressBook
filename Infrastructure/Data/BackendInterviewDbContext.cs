using Backend.Interview.Api.ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Interview.Api.Infrastructure.Data;

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
