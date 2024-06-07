using System.ComponentModel.DataAnnotations;

namespace Backend.Interview.Api.Models;

public class UpdatePersonRequest
{
    [Key]
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Dob { get; set; } // DateOfBirth instead of Dob?
    public Address Address { get; set; }
}
