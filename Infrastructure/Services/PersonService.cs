using Backend.Interview.Api.Models;
using Backend.Interview.Api.Repositories;

namespace Backend.Interview.Api.Services;

public interface IPersonService
{
    Task<List<Person>> GetAllPeopleAsync();
    Task<Person> GetPersonByIdAsync(Guid id);
    Task<Person> AddPersonAsync(Person person);
    Task<Person> UpdatePersonAsync(Person person);
    Task DeletePersonAsync(Guid id);
}

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<List<Person>> GetAllPeopleAsync()
    {
        // TO DO: Sorting Logic
        return await _personRepository.GetAllPeopleAsync();
    }

    public async Task<Person> GetPersonByIdAsync(Guid id)
    {
        // TO DO: Some sort of action
        return await _personRepository.GetPersonByIdAsync(id);
    }

    public async Task<Person> AddPersonAsync(Person person)
    {
        // TO DO: Some sort of validation based on email address and IsDeleted
        return await _personRepository.AddPersonAsync(person);
    }

    public async Task<Person> UpdatePersonAsync(Person person)
    {
        // TO DO: Validate age based on birthdate.
        // if (person.Age < 0 || person.Age > 150)
        // {
        //     throw new ArgumentException("Age must be between 0 and 150.");
        // }
        return await _personRepository.UpdatePersonAsync(person);
    }

    public async Task DeletePersonAsync(Guid id)
    {
        var person = await _personRepository.GetPersonByIdAsync(id);
        await _personRepository.DeletePersonAsync(id);
    }
}
