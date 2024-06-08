using Backend.Interview.Api.ApplicationCore.DTO;
using Backend.Interview.Api.ApplicationCore.Models;

namespace Backend.Interview.Api.ApplicationCore.Contracts;

public interface IPersonService
{
        Task<IEnumerable<PersonResponseDto>> GetAllPeopleAsync();
        Task<PersonResponseDto> GetPersonByIdAsync(Guid id);
        Task<Person> AddPersonAsync(PersonDto personDto);
        Task<Person> UpdatePersonAsync(Guid id, PersonDto personDto);
        Task DeletePersonAsync(Guid id);
}
