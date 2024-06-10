using Backend.Interview.Api.ApplicationCore.Models;

namespace Backend.Interview.Api.ApplicationCore.DTO;

public class PersonDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly Dob { get; set; } = DateOnly.MaxValue;
    public Address Address { get; set; } = new Address();
}
