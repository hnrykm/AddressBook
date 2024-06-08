using System.ComponentModel.DataAnnotations;
using Backend.Interview.Api.ApplicationCore.Contracts;
using Backend.Interview.Api.ApplicationCore.DTO;
using Backend.Interview.Api.ApplicationCore.Models;

namespace Backend.Interview.Api.Infrastructure.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<IEnumerable<PersonResponseDto>> GetAllPeopleAsync()
    {
        var people = await _personRepository.GetAllAsync();
        var peopleDateOnly = people.Select(DateOnlyResponse);
        return peopleDateOnly;
    }

    public async Task<PersonResponseDto> GetPersonByIdAsync(string id)
    {
        var person = await _personRepository.GetByIdAsync(id);

        return DateOnlyResponse(person);
    }

    public async Task<PersonResponseDto> AddPersonAsync(Person person)
    {
        int age = CalculateAge(person.Dob);
        if (age < 0 || age > 150)
        {
            throw new ValidationException("Age must be between 0 and 150.");
        }
        var createdPerson = new Person()
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = person.FirstName,
            LastName = person.LastName,
            Dob = person.Dob,
            Address = new Address()
            {
                Line1 = person.Address.Line1,
                Line2 = person.Address.Line2,
                City = person.Address.City,
                State = person.Address.State,
                ZipCode = person.Address.ZipCode
            }
        };
        Console.WriteLine(createdPerson.Id);
        var response = await _personRepository.AddAsync(createdPerson);
        return DateOnlyResponse(response);
    }

    public async Task<Person> UpdatePersonAsync(string id, Person person)
    {
        int age = CalculateAge(person.Dob);
        if (age < 0 || age > 150)
        {
            throw new ValidationException("Age must be between 0 and 150.");
        }
        Console.WriteLine(person.Dob);
        
        var editedPerson = await _personRepository.GetByIdAsync(id);
        editedPerson.Id = person.Id;
        editedPerson.FirstName = person.FirstName;
        editedPerson.LastName = person.LastName;
        editedPerson.Dob = editedPerson.Dob;
        editedPerson.Address.Line1 = person.Address.Line1;
        editedPerson.Address.Line2 = person.Address.Line2;
        editedPerson.Address.City = person.Address.City;
        editedPerson.Address.State = person.Address.State;
        editedPerson.Address.ZipCode = person.Address.ZipCode;
        
        var response = await _personRepository.UpdateAsync(editedPerson);
        return response;
    }

    public async Task DeletePersonAsync(string id)
    {
        var person = await _personRepository.GetByIdAsync(id);
        if (person == null)
        {
            throw new ArgumentException("Person with ID does not exist.");
        }

        await _personRepository.DeleteAsync(person);
    }
    
    private int CalculateAge(DateTime dob)
    {
        DateTime currentDate = DateTime.UtcNow;
        DateTime dobDate = dob;
    
        int age = currentDate.Year - dobDate.Year;
        if (currentDate < dobDate.AddYears(age))
        {
            age--;
        }
        return age;
    }

    // private DateTime ConvertToDateTime(DateOnly dateOnly)
    // {
    //     return DateTime.SpecifyKind(dateOnly.ToDateTime(TimeOnly.MinValue).Date, DateTimeKind.Utc);
    // }

    private PersonResponseDto DateOnlyResponse(Person person)
    {
        return new PersonResponseDto()
        {
            Id = person.Id,
            FirstName = person.FirstName,
            LastName = person.LastName,
            Dob = DateOnly.FromDateTime(person.Dob),
            Address = new Address
            {
                Line1 = person.Address.Line1,
                Line2 = person.Address.Line2,
                City = person.Address.City,
                State = person.Address.State,
                ZipCode = person.Address.ZipCode
            }
        };
    }

}
