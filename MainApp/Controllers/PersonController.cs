using Infrastructure.Models;
using Infrastructure.Services.PersonService;
using Microsoft.AspNetCore.Mvc;

namespace MainApp.Controllers;

[ApiController]
[Route("/api/person/")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;
    
    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> CreatePerson([FromBody] Person person)
    {
        if (person == null!)
        {
            return Results.BadRequest("Person is null");
        }
        bool res = await _personService.Create(person);
        if (res == false)
        {
            return Results.BadRequest("Person not created");
        }
        return Results.Ok("Person created!");
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> UpdatePerson([FromBody] Person person)
    {
        if (person == null!)
        {
            return Results.BadRequest("Person is null");
        }
        bool res = await _personService.Update(person);
        if (res == false)
        {
            return Results.BadRequest("Person not updated");
        }
        return Results.Ok("Person updated!");
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> DeletePerson([FromRoute]int id)
    {
        if (id == null)
        {
            return Results.BadRequest("Id is null");
        }
        bool res = await _personService.Delete(id);
        if (res == false)
        {
            return Results.BadRequest("Person not deleted");
        }
        return Results.Ok("Person deleted!");
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetById([FromRoute]int id)
    {
        if (id == null)
        {
            return Results.BadRequest("Id is null");
        }

        Person? res = await _personService.GetById(id);
        if (res == null)
        {
            return Results.NotFound("Person not by id");
        }
        return Results.Ok(res);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetAll()
    {
        IEnumerable<Person?> person = await _personService.GetAll();
        if (person == null!)
        {
            return Results.NotFound("Person not found");
        }
        return Results.Ok(person);
    }
}