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
        if (person.Dob > DateTime.Now)
        {
            throw new ValidationException($"Date of birth cannot be before today ({DateOnly.FromDateTime(DateTime.Today)}).");
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
        var response = await _personRepository.AddAsync(createdPerson);
        return DateOnlyResponse(response);
    }

    public async Task<PersonResponseDto> UpdatePersonAsync(string id, PersonResponseDto person)
    {
        
        var updatedPerson = await _personRepository.GetByIdAsync(id);
        updatedPerson.Id = person.Id;
        updatedPerson.FirstName = person.FirstName;
        updatedPerson.LastName = person.LastName;
        updatedPerson.Dob = ConvertToDateTime(person.Dob);
        updatedPerson.Address.Line1 = person.Address.Line1;
        updatedPerson.Address.Line2 = person.Address.Line2;
        updatedPerson.Address.City = person.Address.City;
        updatedPerson.Address.State = person.Address.State;
        updatedPerson.Address.ZipCode = person.Address.ZipCode;

        if (updatedPerson.Dob > DateTime.Now)
        {
            throw new ValidationException($"Date of birth cannot be before today ({DateOnly.FromDateTime(DateTime.Today)}).");
        }
        
        var response = await _personRepository.UpdateAsync(updatedPerson);
        return DateOnlyResponse(response);
    }

    public async Task DeletePersonAsync(string id)
    {
        var person = await _personRepository.GetByIdAsync(id);
        if (person == null)
        {
            throw new ArgumentException($"Person with ID [{id}] does not exist.");
        }

        await _personRepository.DeleteAsync(person);
    }
    

    private DateTime ConvertToDateTime(DateOnly dateOnly)
    {
        return DateTime.SpecifyKind(dateOnly.ToDateTime(TimeOnly.MinValue).Date, DateTimeKind.Utc);
    }

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
