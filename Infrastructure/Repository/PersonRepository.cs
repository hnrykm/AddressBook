using Backend.Interview.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Interview.Api.Repositories;

public interface IPersonRepository
{
    Task<List<Person>> GetAllPeopleAsync();
    Task<Person> GetPersonByIdAsync(Guid id);
    Task<Person> AddPersonAsync(Person person);
    Task<Person> UpdatePersonAsync(Person person);
    Task DeletePersonAsync(Guid id);
}

public class PersonRepository : IPersonRepository
{
    private readonly BackendInterviewDbContext _dbContext;

    public PersonRepository(BackendInterviewDbContext _dbContext)
    {
        _dbContext = _dbContext;
    }

    public async Task<List<Person>> GetAllPeopleAsync()
    {
        return await _dbContext.People.ToListAsync();
    }

    public async Task<Person> GetPersonByIdAsync(Guid id)
    {
        return await _dbContext.People.FindAsync(id);
    }

    public async Task<Person> AddPersonAsync(Person person)
    {
        _dbContext.People.Add(person);
        await _dbContext.SaveChangesAsync();
        return person;
    }

    public async Task<Person> UpdatePersonAsync(Person person)
    {
        _dbContext.Entry(person).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return person;
    }

    public async Task DeletePersonAsync(Guid id)
    {
        var person = await _dbContext.People.FindAsync(id);
        _dbContext.Remove(person);
        await _dbContext.SaveChangesAsync();

    }

}
