using Backend.Interview.Api.ApplicationCore.Contracts;
using Backend.Interview.Api.ApplicationCore.Models;
using Backend.Interview.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Interview.Api.Infrastructure.Repository;

public class PersonRepository : IPersonRepository
{
    private readonly BackendInterviewDbContext _dbContext;

    public PersonRepository(BackendInterviewDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        return await _dbContext.People.ToListAsync();
    }

    public async Task<Person> GetByIdAsync(Guid id)
    {
        return await _dbContext.People.FindAsync(id);
    }

    public async Task<Person> AddAsync(Person person)
    {
        await _dbContext.People.AddAsync(person);
        await _dbContext.SaveChangesAsync();
        
        return person;
    }

    public async Task<Person> UpdateAsync(Person person)
    {
        _dbContext.People.Update(person);
        await _dbContext.SaveChangesAsync();
        
        return person;
    }

    public async Task DeleteAsync(Person person)
    {
        _dbContext.Remove(person);
        await _dbContext.SaveChangesAsync();
    }

}
