using System.ComponentModel.DataAnnotations;

namespace Backend.Interview.Api.ApplicationCore.Models;
public class Person
{
    [Key]
    public Guid Id { get; set; }
    [MaxLength(30)]
    public string FirstName { get; set; } = string.Empty;
    [MaxLength(30)]
    public string LastName { get; set; } = string.Empty;
    public DateTime Dob { get; set; }
    public Address Address { get; set; } = new Address();
}
