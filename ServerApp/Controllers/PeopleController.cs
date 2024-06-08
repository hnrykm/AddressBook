using System.ComponentModel.DataAnnotations;
using Backend.Interview.Api.ApplicationCore.Contracts;
using Backend.Interview.Api.ApplicationCore.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Interview.Api.ServerApp.Controllers;

[ApiController]
[Route("[controller]")]

public class PeopleController : ControllerBase
{
    private readonly IPersonService _personService;

    public PeopleController(IPersonService personService)
    {
        _personService = personService;
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
            return StatusCode(500, "Internal service error");
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PersonDto>> GetPersonByIdAsync(Guid id)
    {
        try
        {
            var person = await _personService.GetPersonByIdAsync(id);
            return Ok(person);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");        
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddPersonAsync(PersonDto personDto)
    {
        try
        {
            var person = await _personService.AddPersonAsync(personDto);
            return Ok(person);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }

    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePersonAsync(Guid id, PersonDto personDto)
    {
        try
        {
            var person = await _personService.UpdatePersonAsync(id, personDto);
            return Ok(person);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePersonAsync(Guid id)
    {
        try
        {
            await _personService.DeletePersonAsync(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }
    
    
}
