using System.ComponentModel.DataAnnotations;
using Backend.Interview.Api.ApplicationCore.Contracts;
using Backend.Interview.Api.ApplicationCore.DTO;
using Backend.Interview.Api.ApplicationCore.Models;
using Backend.Interview.Api.Infrastructure.Logger;
using Backend.Interview.Api.ServerApp.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit.Abstractions;

namespace Backend.Interview.Test;

public class PeopleControllerTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly PeopleController _sut;
    private readonly Mock<IPersonService> _mockPersonService;
    private readonly Mock<ICustomLogger> _mockCustomLogger;
    private readonly Exception _expectedException;

    private readonly IEnumerable<PersonResponseDto> _samplePeopleList;
    private readonly PersonResponseDto _samplePersonDto;
    private readonly Person _samplePerson;
    
    private readonly string _samplePersonId = "1a0f09f2-4497-4be7-a2a9-0f3a6ab2d4ef";


    public PeopleControllerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _mockPersonService = new Mock<IPersonService>();
        _mockCustomLogger = new Mock<ICustomLogger>();

        _sut = new PeopleController(_mockPersonService.Object, _mockCustomLogger.Object);

        _samplePersonDto = new PersonResponseDto()
        {
            Id = "1a0f09f2-4497-4be7-a2a9-0f3a6ab2d4ef",
            FirstName = "Sheldon",
            LastName = "Cooper",
            Dob = new DateOnly(1980, 2, 26),
            Address = new Address()
            {
                Line1 = "2311 N. Los Robles Ave",
                Line2 = "Apt 4A",
                City = "Pasadena",
                State = "California",
                ZipCode = "91107"
            }
        };

        _samplePerson = new Person()
        {
            Id = "91cb1834-4172-4639-a88b-758b7b9ef516",
            FirstName = "Howard",
            LastName = "Wolowitz",
            Dob = new DateTime(1981, 4, 17),
            Address = new Address()
            {
                Line1 = "1200 Fairview Blvd",
                Line2 = "",
                City = "Pasadena",
                State = "California",
                ZipCode = "91106"
            }
        };
        
        _samplePeopleList = new List<PersonResponseDto>
        {
            new PersonResponseDto
            {
                Id = "f7d1e462-8c63-4c1b-8a7b-5a9d2c1bbd12",
                FirstName = "Leonard",
                LastName = "Hofstadter",
                Dob = new DateOnly(1980, 5, 17),
                Address = new Address()
                {
                    Line1 = "2311 N. Los Robles Ave",
                    Line2 = "Apt 4A",
                    City = "Pasadena",
                    State = "California",
                    ZipCode = "91107"
                }
            },
            new PersonResponseDto
            {
                Id = "bfd7b5ad-6b3c-470e-8e80-2a6a5a820fd4",
                FirstName = "Rajesh",
                LastName = "Koothrappali",
                Dob = new DateOnly(1981, 10, 06),
                Address = new Address()
                {
                    Line1 = "315 S. Marengo Ave",
                    Line2 = "Apt 12C",
                    City = "Pasadena",
                    State = "California",
                    ZipCode = "91101"
                }
            }
        };

        _expectedException = new Exception("Test Exception");
    }
    
    [Fact]
    public async Task GetAllPeopleAsync_ReturnsOkResult_WithListOfPeople()
    {
        // Arrange
        _mockPersonService.Setup(service => service.GetAllPeopleAsync()).ReturnsAsync(_samplePeopleList);

        // Act
        var result = await _sut.GetAllPeopleAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsAssignableFrom<IEnumerable<PersonResponseDto>>(okResult.Value);
        Assert.Equal(_samplePeopleList, returnValue);
    }

    [Fact]
    public async Task GetPersonByIdAsync_ReturnsOkResult_WithPerson_WhenPersonExists()
    {
        // Arrange
        _mockPersonService.Setup(service => service.GetPersonByIdAsync((_samplePersonId))).ReturnsAsync(_samplePersonDto);

        // Act
        var result = await _sut.GetPersonByIdAsync(_samplePersonId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<PersonResponseDto>(okResult.Value);
        Assert.Equal(_samplePersonDto, returnValue);
    }

    [Fact]
    public async Task GetPersonByIdAsync_ReturnsNotFound_WhenPersonDoesNotExist()
    {
        var exceptionMessage = $"Person with ID [{_samplePersonId}] does not exist.";
        
        // Arrange
        _mockPersonService.Setup(service => service.GetPersonByIdAsync(_samplePersonId))
            .ThrowsAsync(new ArgumentException(exceptionMessage));

        // Act
        var result = await _sut.GetPersonByIdAsync(_samplePersonId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal(exceptionMessage, notFoundResult.Value);
    }

    [Fact]
    public async Task AddPersonAsync_ReturnsCreatedResult_WithPerson()
    {
        // Arrange
        _mockPersonService.Setup(service => service.AddPersonAsync(_samplePerson)).ReturnsAsync(_samplePersonDto);

        // Act
        var result = await _sut.AddPersonAsync(_samplePerson);

        // Assert
        var createdResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(createdResult.StatusCode, StatusCodes.Status201Created);
        Assert.Equal(createdResult.Value, _samplePersonDto);
    }

    [Fact]
    public async Task AddPersonAsync_ReturnsBadRequest_WhenDobIsInTheFuture()
    {
        var exceptionMessage = $"Date of birth cannot be before today ({DateOnly.FromDateTime(DateTime.Today)}).";
        
        // Arrange
        _mockPersonService.Setup(service => service.AddPersonAsync(_samplePerson))
            .ThrowsAsync(new ValidationException(exceptionMessage));

        // Act
        var result = await _sut.AddPersonAsync(_samplePerson);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(exceptionMessage, badRequestResult.Value);
    }

    [Fact]
    public async Task UpdatePersonAsync_ReturnsOk_WithUpdatedPerson()
    {
        // Arrange
        _mockPersonService.Setup(service => service.UpdatePersonAsync(_samplePersonId, _samplePersonDto))
            .ReturnsAsync(_samplePersonDto);

        // Act
        var result = await _sut.UpdatePersonAsync(_samplePersonId, _samplePersonDto);

        // Assert
        var updatedResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(_samplePersonDto, updatedResult.Value);
    }
    
    [Fact]
    public async Task UpdatePersonAsync_ReturnsBadRequest_WhenDobIsInTheFuture()
    {
        var exceptionMessage = $"Date of birth cannot be before today ({DateOnly.FromDateTime(DateTime.Today)}).";
        
        // Arrange
        _mockPersonService.Setup(service => service.UpdatePersonAsync(_samplePersonId, _samplePersonDto))
            .ThrowsAsync(new ValidationException(exceptionMessage));

        // Act
        var result = await _sut.UpdatePersonAsync(_samplePersonId, _samplePersonDto);

        // Assert
        var updatedResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(exceptionMessage, updatedResult.Value);
    }
    
    [Fact]
    public async Task UpdatePersonAsync_ReturnsNotFound_WhenPersonDoesNotExist()
    {
        var exceptionMessage = $"Person with ID [{_samplePersonId}] does not exist.";
        
        // Arrange
        _mockPersonService.Setup(service => service.UpdatePersonAsync(_samplePersonId, _samplePersonDto))
            .ThrowsAsync(new ArgumentException(exceptionMessage));

        // Act
        var result = await _sut.UpdatePersonAsync(_samplePersonId, _samplePersonDto);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(exceptionMessage, notFoundResult.Value);
    }

    [Fact]
    public async Task DeletePersonAsync_ReturnsNoContent_WhenPersonIsDeleted()
    {
        // Arrange
        _mockPersonService.Setup(service => service.DeletePersonAsync(_samplePersonId)).Returns((Task.CompletedTask));

        // Act
        var result = await _sut.DeletePersonAsync(_samplePersonId);

        // Assert
        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, noContentResult.StatusCode);
    }
    
    [Fact]
    public async Task DeletePersonAsync_ReturnsNotFound_WhenPersonDoesNotExist()
    {
        var exceptionMessage = $"Person with ID [{_samplePersonId}] does not exist.";
        
        // Arrange
        _mockPersonService.Setup(service => service.DeletePersonAsync(_samplePersonId))
            .ThrowsAsync(new ArgumentException(exceptionMessage));

        // Act
        var result = await _sut.DeletePersonAsync(_samplePersonId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(exceptionMessage, notFoundResult.Value);
    }
    
}
