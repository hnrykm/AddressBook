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

    public async Task<PersonResponseDto> GetPersonByIdAsync(Guid id)
    {
        var person = await _personRepository.GetByIdAsync(id);

        return DateOnlyResponse(person);
    }

    public async Task<PersonResponseDto> AddPersonAsync(PersonDto personDto)
    {
        int age = CalculateAge(personDto.Dob);
        if (age < 0 || age > 150)
        {
            throw new ValidationException("Age must be between 0 and 150.");
        }
        var person = new Person()
        {
            Id = Guid.NewGuid(),
            FirstName = personDto.FirstName,
            LastName = personDto.LastName,
            Dob = ConvertToDateTime(personDto.Dob),
            Address = new Address()
            {
                Line1 = personDto.Address.Line1,
                Line2 = personDto.Address.Line2,
                City = personDto.Address.City,
                State = personDto.Address.State,
                ZipCode = personDto.Address.ZipCode
            }
        };
        var response = await _personRepository.AddAsync(person);
        return DateOnlyResponse(response);
    }

    public async Task<PersonResponseDto> UpdatePersonAsync(Guid id, PersonDto personDto)
    {
        int age = CalculateAge(personDto.Dob);
        if (age < 0 || age > 150)
        {
            throw new ValidationException("Age must be between 0 and 150.");
        }
        
        var person = await _personRepository.GetByIdAsync(id);
        
        person.FirstName = personDto.FirstName;
        person.LastName = personDto.LastName;
        person.Dob = ConvertToDateTime(personDto.Dob);
        person.Address.Line1 = personDto.Address.Line1;
        person.Address.Line2 = personDto.Address.Line2;
        person.Address.City = personDto.Address.City;
        person.Address.State = personDto.Address.State;
        person.Address.ZipCode = personDto.Address.ZipCode;
        
        var response = await _personRepository.UpdateAsync(person);
        return DateOnlyResponse(response);
    }

    public async Task DeletePersonAsync(Guid id)
    {
        var person = await _personRepository.GetByIdAsync(id);
        if (person == null)
        {
            throw new ArgumentException("Person with ID does not exist.");
        }

        await _personRepository.DeleteAsync(person);
    }
    
    private int CalculateAge(DateOnly dob)
    {
        DateOnly currentDate = DateOnly.FromDateTime(DateTime.UtcNow);
        DateOnly dobDate = dob;
    
        int age = currentDate.Year - dobDate.Year;
        if (currentDate < dobDate.AddYears(age))
        {
            age--;
        }
        return age;
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
