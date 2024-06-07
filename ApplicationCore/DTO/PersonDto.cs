using Backend.Interview.Api.ApplicationCore.Models;

namespace Backend.Interview.Api.ApplicationCore.DTO;

public class PersonDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Dob { get; set; } // DateOfBirth instead of Dob?
    public Address Address { get; set; }
}
