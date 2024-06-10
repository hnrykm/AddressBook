using Backend.Interview.Api.ApplicationCore.DTO;
using Backend.Interview.Api.ApplicationCore.Models;

namespace Backend.Interview.Api.ApplicationCore.Contracts;

public interface IPersonService
{
        Task<IEnumerable<PersonResponseDto>> GetAllPeopleAsync();
        Task<PersonResponseDto> GetPersonByIdAsync(string id);
        Task<PersonResponseDto> AddPersonAsync(Person person);
        Task<PersonResponseDto> UpdatePersonAsync(string id, PersonResponseDto person);
        Task DeletePersonAsync(string id);
}
