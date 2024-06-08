using Backend.Interview.Api.ApplicationCore.DTO;

namespace Backend.Interview.Api.ApplicationCore.Contracts;

public interface IPersonService
{
        Task<IEnumerable<PersonResponseDto>> GetAllPeopleAsync();
        Task<PersonResponseDto> GetPersonByIdAsync(Guid id);
        Task<PersonResponseDto> AddPersonAsync(PersonDto personDto);
        Task<PersonResponseDto> UpdatePersonAsync(Guid id, PersonDto personDto);
        Task DeletePersonAsync(Guid id);
}
