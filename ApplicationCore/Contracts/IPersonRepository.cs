using Backend.Interview.Api.ApplicationCore.Models;

namespace Backend.Interview.Api.ApplicationCore.Contracts;

public interface IPersonRepository
{
    Task<IEnumerable<Person>> GetAllAsync();
    Task<Person> GetByIdAsync(Guid id);
    Task<Person> AddAsync(Person person);
    Task<Person> UpdateAsync(Person person);
    Task DeleteAsync(Person person);
}
