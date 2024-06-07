using System.ComponentModel.DataAnnotations;

namespace Backend.Interview.Api.ApplicationCore.Models;
public class Person
{
    [Key]
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Dob { get; set; } // DateOfBirth instead of Dob?
    public Address Address { get; set; }
}
