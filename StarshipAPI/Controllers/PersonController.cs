using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly PersonService _personService;

    public PersonController(PersonService personService)
    {
        _personService = personService;
    }

    // GET: /api/people
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Person>>> GetAllPeople()
    {
        var people = await _personService.GetAllPeople();
        return Ok(people);
    }

    // GET: /api/people/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Person>> GetPersonById(int id)
    {
        var person = await _personService.GetPersonById(id);

        if (person == null)
        {
            return NotFound(new { Message = $"Person with ID {id} not found." });
        }

        return Ok(person);
    }

    // POST: /api/people
    [HttpPost]
    public async Task<ActionResult<Person>> CreatePerson([FromBody] Person person)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var newPerson = await _personService.CreatePerson(person);
        return CreatedAtAction(nameof(GetPersonById), new { id = newPerson.Id }, newPerson);
    }

    // PUT: /api/people/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePerson(int id, [FromBody] Person person)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _personService.UpdatePerson(id, person);
        if (!result)
        {
            return NotFound(new { Message = $"Person with ID {id} not found." });
        }

        return NoContent();
    }

    // DELETE: /api/people/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(int id)
    {
        var result = await _personService.DeletePerson(id);
        if (!result)
        {
            return NotFound(new { Message = $"Person with ID {id} not found." });
        }

        return NoContent();
    }
}
