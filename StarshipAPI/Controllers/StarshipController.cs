using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/starships")]
public class StarshipController : ControllerBase
{
    private readonly StarshipService _starshipService;

    public StarshipController(StarshipService starshipService)
    {
        _starshipService = starshipService;
    }

    // GET: /starships
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Starship>>> GetAllStarships()
    {
        var starships = await _starshipService.GetAllStarships();
        return Ok(starships);
    }

    // GET: /starships/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Starship>> GetStarshipById(int id)
    {
        var starship = await _starshipService.GetStarshipById(id);

        if (starship == null)
        {
            return NotFound();
        }

        return Ok(starship);
    }

    // POST: /starships
    [HttpPost]
    public async Task<ActionResult<Starship>> CreateStarship(Starship starship)
    {
        var newStarship = await _starshipService.CreateStarship(starship);
        return CreatedAtAction(nameof(GetStarshipById), new { id = newStarship.Id }, newStarship);
    }

    // PUT: /starships/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStarship(int id, Starship starship)
    {
        var result = await _starshipService.UpdateStarship(id, starship);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: /starships/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStarship(int id)
    {
        var result = await _starshipService.DeleteStarship(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
