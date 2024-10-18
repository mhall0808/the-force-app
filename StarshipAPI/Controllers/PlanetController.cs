using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("planets")]
public class PlanetController : ControllerBase
{
    private readonly PlanetService _planetService;

    public PlanetController(PlanetService planetService)
    {
        _planetService = planetService;
    }

    // GET: /planets
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Planet>>> GetAllPlanets()
    {
        var planets = await _planetService.GetAllPlanets();
        return Ok(planets);
    }

    // GET: /planets/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Planet>> GetPlanetById(int id)
    {
        var planet = await _planetService.GetPlanetById(id);

        if (planet == null)
        {
            return NotFound();
        }

        return Ok(planet);
    }

    // POST: /planets
    [HttpPost]
    public async Task<ActionResult<Planet>> CreatePlanet(Planet planet)
    {
        var newPlanet = await _planetService.CreatePlanet(planet);
        return CreatedAtAction(nameof(GetPlanetById), new { id = newPlanet.Id }, newPlanet);
    }

    // PUT: /planets/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePlanet(int id, Planet planet)
    {
        var result = await _planetService.UpdatePlanet(id, planet);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: /planets/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlanet(int id)
    {
        var result = await _planetService.DeletePlanet(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
