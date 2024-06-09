using System.Text.Json;
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
        
        // SeedData(modelBuilder);
    }

    // private void SeedData(ModelBuilder modelBuilder)
    // {
    //     // Read JSON file
    //     var json = File.ReadAllText("Infrastructure/Data/seed.json");
    //
    //     // Deserialize JSON to list of objects
    //     var seedData = JsonSerializer.Deserialize<Dictionary<string, List<Person>>>(json);
    //
    //     // Add data to DbContext and save changes
    //     using var context = new BackendInterviewDbContext();
    //     // Add seed data to DbSet
    //     context.People.AddRange(seedData["people"]);
    //
    //     // Save changes to database
    //     context.SaveChanges();
    //
    //     Console.WriteLine("Seed data initialized successfully.");
    // }

    public DbSet<Person> People { get; set; }
}
