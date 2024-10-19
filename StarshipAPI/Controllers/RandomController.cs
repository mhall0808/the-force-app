// Controllers/RandomController.cs

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

[ApiController]
[Route("api/random")]
public class RandomController : ControllerBase
{
    private readonly PersonService _personService;
    private readonly StarshipService _starshipService;
    private readonly PlanetService _planetService;
    private readonly FilmService _filmService;
    private readonly VehicleService _vehicleService;
    private readonly SpeciesService _speciesService;

    public RandomController(
        PersonService personService,
        StarshipService starshipService,
        PlanetService planetService,
        FilmService filmService,
        VehicleService vehicleService,
        SpeciesService speciesService)
    {
        _personService = personService;
        _starshipService = starshipService;
        _planetService = planetService;
        _filmService = filmService;
        _vehicleService = vehicleService;
        _speciesService = speciesService;
    }

    [HttpGet("people")]
    public async Task<IActionResult> GetRandomPerson()
    {
        var people = await _personService.GetAllPeople();
        var randomPerson = people.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        return Ok(randomPerson);
    }

    [HttpGet("starships")]
    public async Task<IActionResult> GetRandomStarship()
    {
        var starships = await _starshipService.GetAllStarships();
        var randomStarship = starships.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        return Ok(randomStarship);
    }

    [HttpGet("planets")]
    public async Task<IActionResult> GetRandomPlanet()
    {
        var planets = await _planetService.GetAllPlanets();
        var randomPlanet = planets.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        return Ok(randomPlanet);
    }

    [HttpGet("films")]
    public async Task<IActionResult> GetRandomFilm()
    {
        var films = await _filmService.GetAllFilms();
        var randomFilm = films.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        return Ok(randomFilm);
    }

    [HttpGet("vehicles")]
    public async Task<IActionResult> GetRandomVehicle()
    {
        var vehicles = await _vehicleService.GetAllVehicles();
        var randomVehicle = vehicles.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        return Ok(randomVehicle);
    }

    [HttpGet("species")]
    public async Task<IActionResult> GetRandomSpecies()
    {
        var speciesList = await _speciesService.GetAllSpecies();
        var randomSpecies = speciesList.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        return Ok(randomSpecies);
    }
}
