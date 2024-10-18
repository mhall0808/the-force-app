using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

public class GenericSeeder
{
    private readonly HttpClient _httpClient;
    private readonly TheForceDbContext _dbContext;

    public GenericSeeder(HttpClient httpClient, TheForceDbContext dbContext)
    {
        _httpClient = httpClient;
        _dbContext = dbContext;
    }

    // Seed all entities
    public async Task SeedAllEntitiesAsync()
    {
        await SeedFilmsAsync();
        await SeedPeopleAsync();
        await SeedPlanetsAsync();
        await SeedSpeciesAsync();
        await SeedStarshipsAsync();
        await SeedVehiclesAsync();
    }

    // Generic seed method with pagination, type arguments are explicitly defined now
    private async Task SeedEntityAsync<TEntity>(string? apiUrl, Func<JObject, TEntity> entityMapper, DbSet<TEntity> dbSet) where TEntity : class
    {
        while (!string.IsNullOrEmpty(apiUrl))
        {
            var response = await _httpClient.GetStringAsync(apiUrl);
            var jsonResponse = JObject.Parse(response);

            // Deserialize and map to entities
            var results = jsonResponse["results"].ToObject<List<JObject>>();
            var entities = results.Select(entityMapper).ToList();

            // Add or update entities in the database
            foreach (var entity in entities)
            {
                await dbSet.AddAsync(entity); // You can implement AddOrUpdate logic here if necessary
            }

            // Save the changes to the database
            await _dbContext.SaveChangesAsync();

            // Move to the next page if exists
            apiUrl = jsonResponse["next"]?.ToString();
        }
    }


    // Seeder methods for each entity type
    public async Task SeedFilmsAsync()
    {
        await SeedEntityAsync("https://swapi.dev/api/films/?format=json", MapFilm, _dbContext.Films);
    }

    public async Task SeedPeopleAsync()
    {
        await SeedEntityAsync("https://swapi.dev/api/people/?format=json", MapPerson, _dbContext.People);
    }

    public async Task SeedPlanetsAsync()
    {
        await SeedEntityAsync("https://swapi.dev/api/planets/?format=json", MapPlanet, _dbContext.Planets);
    }

    public async Task SeedSpeciesAsync()
    {
        await SeedEntityAsync("https://swapi.dev/api/species/?format=json", MapSpecies, _dbContext.Species);
    }

    public async Task SeedStarshipsAsync()
    {
        await SeedEntityAsync("https://swapi.dev/api/starships/?format=json", MapStarship, _dbContext.Starships);
    }

    public async Task SeedVehiclesAsync()
    {
        await SeedEntityAsync("https://swapi.dev/api/vehicles/?format=json", MapVehicle, _dbContext.Vehicles);
    }

    private Film MapFilm(JObject filmJson)
    {
        return new Film
        {
            Title = filmJson["title"].ToString(),
            EpisodeId = (int)filmJson["episode_id"],
            Director = filmJson["director"].ToString(),
            Producer = filmJson["producer"].ToString(),
            ReleaseDate = (DateTime)filmJson["release_date"],
            OpeningCrawl = filmJson["opening_crawl"]?.ToString() ?? "", // Handle nulls
            Url = filmJson["url"].ToString(),
            Created = (DateTime?)filmJson["created"],
            Edited = (DateTime?)filmJson["edited"],
            // Now map the lists of URLs as strings
            Characters = filmJson["characters"].ToObject<List<string>>(),
            Planets = filmJson["planets"].ToObject<List<string>>(),
            Starships = filmJson["starships"].ToObject<List<string>>(),
            Vehicles = filmJson["vehicles"].ToObject<List<string>>(),
            Species = filmJson["species"].ToObject<List<string>>()
        };
    }

    // Mapping logic for the Person entity
    private Person MapPerson(JObject personJson)
    {
        return new Person
        {
            Name = personJson["name"]?.ToString(),
            Height = personJson["height"]?.ToString(),
            Mass = personJson["mass"]?.ToString(),
            HairColor = personJson["hair_color"]?.ToString(),
            SkinColor = personJson["skin_color"]?.ToString(),
            EyeColor = personJson["eye_color"]?.ToString(),
            BirthYear = personJson["birth_year"]?.ToString(),
            Gender = personJson["gender"]?.ToString(),
            Homeworld = personJson["homeworld"]?.ToString(), // Store as a string (URL)

            // Map the related entities as lists of URLs
            Films = personJson["films"]?.Select(f => f.ToString()).ToList() ?? new List<string>(),
            Species = personJson["species"]?.Select(s => s.ToString()).ToList() ?? new List<string>(),
            Starships = personJson["starships"]?.Select(st => st.ToString()).ToList() ?? new List<string>(),
            Vehicles = personJson["vehicles"]?.Select(v => v.ToString()).ToList() ?? new List<string>(),

            Created = personJson["created"]?.ToObject<DateTime>(),
            Edited = personJson["edited"]?.ToObject<DateTime>(),
            Url = personJson["url"]?.ToString()
        };
    }

    private Planet MapPlanet(JObject planetJson)
    {
        return new Planet
        {
            Name = planetJson["name"].ToString(),
            RotationPeriod = planetJson["rotation_period"]?.ToString(),
            OrbitalPeriod = planetJson["orbital_period"]?.ToString(),
            Diameter = planetJson["diameter"].ToString(),
            Climate = planetJson["climate"].ToString(),
            Gravity = planetJson["gravity"].ToString(),
            Terrain = planetJson["terrain"].ToString(),
            SurfaceWater = planetJson["surface_water"]?.ToString(),
            Population = planetJson["population"].ToString(),

            // Map related entities as list of URLs
            Residents = planetJson["residents"]?.Select(r => r.ToString()).ToList() ?? new List<string>(),
            Films = planetJson["films"]?.Select(f => f.ToString()).ToList() ?? new List<string>(),

            Created = planetJson["created"] != null
                ? (DateTime?)DateTime.Parse(planetJson["created"].ToString())
                : DateTime.Now,
            Edited = planetJson["edited"] != null
                ? (DateTime?)DateTime.Parse(planetJson["edited"].ToString())
                : DateTime.Now,
            Url = planetJson["url"].ToString()
        };
    }

    private Species MapSpecies(JObject speciesJson)
    {
        return new Species
        {
            Name = speciesJson["name"].ToString(),
            Classification = speciesJson["classification"].ToString(),
            Designation = speciesJson["designation"].ToString(),
            AverageHeight = speciesJson["average_height"]?.ToString(),
            SkinColors = speciesJson["skin_colors"]?.ToString(),
            HairColors = speciesJson["hair_colors"]?.ToString(),
            EyeColors = speciesJson["eye_colors"]?.ToString(),
            AverageLifespan = speciesJson["average_lifespan"]?.ToString(),
            Language = speciesJson["language"]?.ToString(),
            Homeworld = speciesJson["homeworld"]?.ToString(),

            // Map related entities as list of URLs
            People = speciesJson["people"]?.Select(p => p.ToString()).ToList() ?? new List<string>(),
            Films = speciesJson["films"]?.Select(f => f.ToString()).ToList() ?? new List<string>(),

            Created = speciesJson["created"] != null
                ? (DateTime?)DateTime.Parse(speciesJson["created"].ToString())
                : DateTime.Now,
            Edited = speciesJson["edited"] != null
                ? (DateTime?)DateTime.Parse(speciesJson["edited"].ToString())
                : DateTime.Now,
            Url = speciesJson["url"].ToString()
        };
    }

    private Starship MapStarship(JObject starshipJson)
    {
        return new Starship
        {
            Name = starshipJson["name"].ToString(),
            Model = starshipJson["model"].ToString(),
            Manufacturer = starshipJson["manufacturer"].ToString(),
            CostInCredits = starshipJson["cost_in_credits"]?.ToString(),
            Length = starshipJson["length"]?.ToString(),
            MaxAtmospheringSpeed = starshipJson["max_atmosphering_speed"]?.ToString(),
            Crew = starshipJson["crew"]?.ToString(),
            Passengers = starshipJson["passengers"]?.ToString(),
            CargoCapacity = starshipJson["cargo_capacity"]?.ToString(),
            Consumables = starshipJson["consumables"]?.ToString(),
            HyperdriveRating = starshipJson["hyperdrive_rating"]?.ToString(),
            MGLT = starshipJson["MGLT"]?.ToString(),
            StarshipClass = starshipJson["starship_class"]?.ToString(),

            // Mapping related entities (Pilots and Films) as lists of URLs
            Pilots = starshipJson["pilots"]?.Select(p => p.ToString()).ToList() ?? new List<string>(),
            Films = starshipJson["films"]?.Select(f => f.ToString()).ToList() ?? new List<string>(),

            Created = starshipJson["created"] != null
                ? (DateTime?)DateTime.Parse(starshipJson["created"].ToString())
                : DateTime.Now,
            Edited = starshipJson["edited"] != null
                ? (DateTime?)DateTime.Parse(starshipJson["edited"].ToString())
                : DateTime.Now,
            Url = starshipJson["url"].ToString()
        };
    }

    private Vehicle MapVehicle(JObject vehicleJson)
    {
        return new Vehicle
        {
            Name = vehicleJson["name"].ToString(),
            Model = vehicleJson["model"].ToString(),
            Manufacturer = vehicleJson["manufacturer"].ToString(),
            CostInCredits = vehicleJson["cost_in_credits"]?.ToString(),
            Length = vehicleJson["length"]?.ToString(),
            MaxAtmospheringSpeed = vehicleJson["max_atmosphering_speed"]?.ToString(),
            Crew = vehicleJson["crew"]?.ToString(),
            Passengers = vehicleJson["passengers"]?.ToString(),
            CargoCapacity = vehicleJson["cargo_capacity"]?.ToString(),
            Consumables = vehicleJson["consumables"]?.ToString(),
            VehicleClass = vehicleJson["vehicle_class"]?.ToString(),

            // Mapping related entities (Pilots and Films) as lists of URLs
            Pilots = vehicleJson["pilots"]?.Select(p => p.ToString()).ToList() ?? new List<string>(),
            Films = vehicleJson["films"]?.Select(f => f.ToString()).ToList() ?? new List<string>(),

            Created = vehicleJson["created"] != null
                ? (DateTime?)DateTime.Parse(vehicleJson["created"].ToString())
                : DateTime.Now,
            Edited = vehicleJson["edited"] != null
                ? (DateTime?)DateTime.Parse(vehicleJson["edited"].ToString())
                : DateTime.Now,
            Url = vehicleJson["url"].ToString()
        };
    }

}
