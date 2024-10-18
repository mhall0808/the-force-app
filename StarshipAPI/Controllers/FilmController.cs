using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class FilmController : ControllerBase
{
    private readonly FilmService _filmService;

    public FilmController(FilmService filmService)
    {
        _filmService = filmService;
    }

    // GET: /api/films
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Film>>> GetAllFilms()
    {
        var films = await _filmService.GetAllFilms();
        return Ok(films);
    }

    // GET: /api/films/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Film>> GetFilmById(int id)
    {
        var film = await _filmService.GetFilmById(id);

        if (film == null)
        {
            return NotFound(new { Message = $"Film with ID {id} not found." });
        }

        return Ok(film);
    }

    // POST: /api/films
    [HttpPost]
    public async Task<ActionResult<Film>> CreateFilm([FromBody] Film film)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var newFilm = await _filmService.CreateFilm(film);
            return CreatedAtAction(nameof(GetFilmById), new { id = newFilm.Id }, newFilm);
        }
        catch (Exception ex)
        {
            // Ideally, you'd log this error and provide a meaningful message.
            return StatusCode(500, new { Message = "An error occurred while creating the film.", Details = ex.Message });
        }
    }

    // PUT: /api/films/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFilm(int id, [FromBody] Film film)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _filmService.UpdateFilm(id, film);
            if (!result)
            {
                return NotFound(new { Message = $"Film with ID {id} not found." });
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while updating the film.", Details = ex.Message });
        }
    }

    // DELETE: /api/films/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFilm(int id)
    {
        try
        {
            var result = await _filmService.DeleteFilm(id);
            if (!result)
            {
                return NotFound(new { Message = $"Film with ID {id} not found." });
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while deleting the film.", Details = ex.Message });
        }
    }

    // Example: GET: /api/films/director/George%20Lucas
    [HttpGet("director/{director}")]
    public async Task<ActionResult<IEnumerable<Film>>> GetFilmsByDirector(string director)
    {
        var films = await _filmService.GetFilmsByDirector(director);
        if (films == null || !films.Any())
        {
            return NotFound(new { Message = $"No films found for director {director}." });
        }

        return Ok(films);
    }
}
