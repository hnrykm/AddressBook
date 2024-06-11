using Backend.Interview.Api.ApplicationCore.Models;

namespace Backend.Interview.Api.ApplicationCore.DTO;

public class PersonDobStringDto
{
    public string Id { get; set; } = String.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Dob { get; set; } = string.Empty;
    public Address Address { get; set; } = new Address();
}
