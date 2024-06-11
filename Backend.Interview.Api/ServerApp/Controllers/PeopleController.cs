using System.ComponentModel.DataAnnotations;
using Backend.Interview.Api.ApplicationCore.Contracts;
using Backend.Interview.Api.ApplicationCore.DTO;
using Backend.Interview.Api.ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Interview.Api.ServerApp.Controllers;

[ApiController]
[Route("[controller]")]

public class PeopleController : ControllerBase
{
    private readonly IPersonService _personService;
    private readonly ICustomLogger _logger;

    public PeopleController(IPersonService personService, ICustomLogger logger)
    {
        _personService = personService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonResponseDto>>> GetAllPeopleAsync()
    {
        try
        {
            var people = await _personService.GetAllPeopleAsync();
            return Ok(people);

        }
        catch (Exception)
        {
            _logger.WriteLog("500 Internal Server Error | Unexpected error fetching all people.");
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PersonDto>> GetPersonByIdAsync(string id)
    {
        try
        {
            var person = await _personService.GetPersonByIdAsync(id);
            return Ok(person);
        }
        catch (ArgumentException ex)
        {
            _logger.WriteLog($"404 Not Found | {ex.Message}");
            return NotFound(ex.Message);
        }
        catch (Exception)
        {
            _logger.WriteLog($"500 Internal Server Error | Unexpected error fetching person with ID [{id}].");
            return StatusCode(500, "Internal Server Error");        
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddPersonAsync(Person person)
    {
        try
        {
            var newPerson = await _personService.AddPersonAsync(person);
            _logger.WriteLog($"201 Created | New person with ID [{newPerson.Id}] created.");
            return new ObjectResult(newPerson) {StatusCode = StatusCodes.Status201Created};
        }
        catch (ValidationException ex)
        {
            _logger.WriteLog($"400 Bad Request | {ex.Message}");
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            _logger.WriteLog($"500 Internal Server Error | Unexpected error creating new person.");
            return StatusCode(500, "Internal server error");
        }

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePersonAsync(string id, PersonResponseDto person)
    {
        try
        {
            var updatedPerson = await _personService.UpdatePersonAsync(id, person);
            _logger.WriteLog($"200 Ok | Person with ID [{id}] updated.");
            return Ok(updatedPerson);
        }
        catch (ValidationException ex)
        {
            _logger.WriteLog($"400 Bad Request | {ex.Message}");
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            _logger.WriteLog($"404 Not Found | {ex.Message}");
            return NotFound(ex.Message);
        }
        catch (Exception)
        {
            _logger.WriteLog($"500 Internal Server Error | Unexpected error updating person with ID [{id}].");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePersonAsync(string id)
    {
        try
        {
            await _personService.DeletePersonAsync(id);
            _logger.WriteLog($"204 No Content | Person with ID [{id}] deleted.");
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            _logger.WriteLog($"404 Not Found | {ex.Message}");
            return NotFound(ex.Message);
        }
        catch (Exception)
        {
            _logger.WriteLog($"500 Internal Server Error | Unexpected error deleting person with ID [{id}].");
            return StatusCode(500, "Internal server error");
        }
    }

    // [HttpPost("seed")]
    // public async Task<IActionResult> AddPeopleFromSeedDataAsync(string peopleJson)
    // {
    //     try
    //     {
    //             var data = JsonSerializer.Deserialize<List<PersonDobStringDto>>(peopleJson);
    //         foreach (var person in data.people)
    //         {
    //
    //             Console.WriteLine(person.ToString());
    //             var deserializedPerson = new Person()
    //             {
    //                 Id = person.Id,
    //                 FirstName = person.FirstName,
    //                 LastName = person.LastName,
    //                 Dob = ParseDateString(person.Dob),
    //                 Address = new Address()
    //                 {
    //                     Line1 = person.Address.Line1,
    //                     Line2 = person.Address.Line2,
    //                     City = person.Address.City,
    //                     State = person.Address.State,
    //                     ZipCode = person.Address.ZipCode
    //                 }
    //             };
    //             await _personService.AddPersonAsync(deserializedPerson);
    //         }
    //
    //         _logger.WriteLog($"Seed data added successfully.");
    //         return Ok("Seed data added successfully.");
    //     }
    //     catch (ValidationException ex)
    //     {
    //         _logger.WriteLog($"400 Bad Request | {ex.Message}");
    //         return BadRequest(ex.Message);
    //     }
    //     catch (Exception)
    //     {
    //         _logger.WriteLog($"500 Internal Server Error | Unexpected error adding seed data.");
    //         return StatusCode(500, "Internal server error");
    //     }
    //     
    //     DateTime ParseDateString(string personDob)
    //     {
    //         return DateTime.TryParseExact(personDob, "MM-dd-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime result) ? result : DateTime.Now;
    //     }
    // }
}
