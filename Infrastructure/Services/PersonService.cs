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

    public async Task<IEnumerable<Person>> GetAllPeopleAsync()
    {
        var people = await _personRepository.GetAllAsync();
        // TO DO: Sorting Logic
        return people;
    }

    public async Task<Person> GetPersonByIdAsync(Guid id)
    {
        var person = await _personRepository.GetByIdAsync(id);
        // TO DO: Some sort of action
        if (person == null)
        {
            throw new ArgumentException("Person with ID was not found.");
        }
        return person;
    }

    public async Task<Person> AddPersonAsync(PersonDto personDto)
    {
        var person = new Person()
        {
            Id = Guid.NewGuid(),
            FirstName = personDto.FirstName,
            LastName = personDto.LastName,
            Dob = personDto.Dob,
            Address = new Address()
            {
                Line1 = personDto.Address.Line1,
                Line2 = personDto.Address.Line2,
                City = personDto.Address.City,
                State = personDto.Address.State,
                ZipCode = personDto.Address.ZipCode
            }
        };
        // TO DO: Some sort of validation based on email address?
        await _personRepository.AddAsync(person);
        return person;
    }

    public async Task<Person> UpdatePersonAsync(Guid id, PersonDto personDto)
    {
        var person = await _personRepository.GetByIdAsync(id);
        if (person == null)
        {
            throw new ArgumentException("Person with ID does not exist.");
        }
        // TO DO: Validate age based on birthdate.
        // if (person.Age < 0 || person.Age > 150)
        // {
        //     throw new ArgumentException("Age must be between 0 and 150.");
        // }
        
        person.FirstName = personDto.FirstName;
        person.LastName = personDto.LastName;
        person.Dob = personDto.Dob;
        person.Address.Line1 = personDto.Address.Line1;
        person.Address.Line2 = personDto.Address.Line2;
        person.Address.City = personDto.Address.City;
        person.Address.State = personDto.Address.State;
        person.Address.ZipCode = personDto.Address.ZipCode;
        
        return await _personRepository.UpdateAsync(person);
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
}
