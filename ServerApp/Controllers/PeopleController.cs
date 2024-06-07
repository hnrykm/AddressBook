using Backend.Interview.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Interview.Api.Controllers;

[ApiController]
[Route("[controller]")]

public class PeopleController : ControllerBase
{
    // private readonly IPersonService _personService;
    private readonly BackendInterviewDbContext _dbContext;

    public PeopleController(BackendInterviewDbContext dbContext)
    {
        // _personService = personService;
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPeople()
    {
        return Ok(await _dbContext.People.ToListAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPersonByIdAsync(Guid id)
    {
        var person = await _dbContext.People.FindAsync(id);
        if (person != null)
        {
            return Ok(person);
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> AddPersonAsync(AddPersonRequest addPersonRequest)
    {
        var person = new Person()
        {
            Id = Guid.NewGuid(),
            FirstName = addPersonRequest.FirstName,
            LastName = addPersonRequest.LastName,
            Dob = addPersonRequest.Dob,
            Address = new Address()
            {
                Line1 = addPersonRequest.Address.Line1,
                Line2 = addPersonRequest.Address.Line2,
                City = addPersonRequest.Address.City,
                State = addPersonRequest.Address.State,
                ZipCode = addPersonRequest.Address.ZipCode
            }
        };
        await _dbContext.People.AddAsync(person);
        await _dbContext.SaveChangesAsync();

        return Ok(person);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePersonAsync(Guid id, UpdatePersonRequest updatePersonRequest)
    {
        var person = await _dbContext.People.FindAsync(id);
        if (person != null)
        {
            person.FirstName = updatePersonRequest.FirstName;
            person.LastName = updatePersonRequest.LastName;
            person.Dob = updatePersonRequest.Dob;
            person.Address.Line1 = updatePersonRequest.Address.Line1;
            person.Address.Line2 = updatePersonRequest.Address.Line2;
            person.Address.City = updatePersonRequest.Address.City;
            person.Address.State = updatePersonRequest.Address.State;
            person.Address.ZipCode = updatePersonRequest.Address.ZipCode;
            
            await _dbContext.SaveChangesAsync();
            return Ok(person);
        }

        return NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePersonAsync(Guid id)
    {
        var person = await _dbContext.People.FindAsync(id);
        if (person != null)
        {
            _dbContext.Remove(person);
            await _dbContext.SaveChangesAsync();
            return Ok(person);
        }

        return NotFound();
    }
    
    
}
