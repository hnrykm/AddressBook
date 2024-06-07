using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Interview.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

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
}
