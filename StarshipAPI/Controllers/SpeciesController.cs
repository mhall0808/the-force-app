using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/species")]
public class SpeciesController : ControllerBase
{
    private readonly SpeciesService _speciesService;

    public SpeciesController(SpeciesService speciesService)
    {
        _speciesService = speciesService;
    }

    // GET: /species
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Species>>> GetAllSpecies()
    {
        var speciesList = await _speciesService.GetAllSpecies();
        return Ok(speciesList);
    }

    // GET: /species/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Species>> GetSpeciesById(int id)
    {
        var species = await _speciesService.GetSpeciesById(id);

        if (species == null)
        {
            return NotFound();
        }

        return Ok(species);
    }

    // POST: /species
    [HttpPost]
    public async Task<ActionResult<Species>> CreateSpecies(Species species)
    {
        var newSpecies = await _speciesService.CreateSpecies(species);
        return CreatedAtAction(nameof(GetSpeciesById), new { id = newSpecies.Id }, newSpecies);
    }

    // PUT: /species/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSpecies(int id, Species species)
    {
        var result = await _speciesService.UpdateSpecies(id, species);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: /species/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSpecies(int id)
    {
        var result = await _speciesService.DeleteSpecies(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
