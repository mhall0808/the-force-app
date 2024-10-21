using Microsoft.EntityFrameworkCore;

public class PlanetServiceTests
{
    private TheForceDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<TheForceDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new TheForceDbContext(options);
    }

    [Fact]
    public async Task CreatePlanet_ShouldAddPlanet()
    {
        var dbContext = GetInMemoryDbContext();
        var planetService = new PlanetService(dbContext);
        var planet = new Planet
        {
            Name = "Tatooine",
            Climate = "Arid",
            Terrain = "Desert"
        };

        var result = await planetService.CreatePlanet(planet);

        Assert.NotNull(result);
        Assert.Equal("Tatooine", result.Name);
        Assert.Equal(1, await dbContext.Planets.CountAsync());
    }

    [Fact]
    public async Task GetAllPlanets_ShouldReturnAllPlanets()
    {
        var dbContext = GetInMemoryDbContext();
        var planetService = new PlanetService(dbContext);
        dbContext.Planets.Add(new Planet { Name = "Planet 1", Climate = "Climate 1" });
        dbContext.Planets.Add(new Planet { Name = "Planet 2", Climate = "Climate 2" });
        await dbContext.SaveChangesAsync();

        var planets = await planetService.GetAllPlanets();

        Assert.Equal(2, planets.Count());
    }

    [Fact]
    public async Task GetPlanetById_ShouldReturnPlanet()
    {
        var dbContext = GetInMemoryDbContext();
        var planetService = new PlanetService(dbContext);
        var planet = new Planet { Name = "Dagobah", Climate = "Swamp" };
        dbContext.Planets.Add(planet);
        await dbContext.SaveChangesAsync();

        var result = await planetService.GetPlanetById(planet.Id);

        Assert.NotNull(result);
        Assert.Equal("Dagobah", result.Name);
    }

    [Fact]
    public async Task UpdatePlanet_ShouldModifyPlanet()
    {
        var dbContext = GetInMemoryDbContext();
        var planetService = new PlanetService(dbContext);
        var planet = new Planet { Name = "Old Name", Climate = "Old Climate" };
        dbContext.Planets.Add(planet);
        await dbContext.SaveChangesAsync();

        var updatedPlanet = new Planet { Name = "Tatooine", Climate = "Arid" };

        var result = await planetService.UpdatePlanet(planet.Id, updatedPlanet);

        Assert.True(result);
        var planetFromDb = await dbContext.Planets.FindAsync(planet.Id);
        Assert.Equal("Tatooine", planetFromDb.Name);
    }

    [Fact]
    public async Task UpdatePlanet_ShouldReturnFalseForNonExistentId()
    {
        var dbContext = GetInMemoryDbContext();
        var planetService = new PlanetService(dbContext);

        var updatedPlanet = new Planet { Name = "Hoth", Climate = "Frozen" };

        var result = await planetService.UpdatePlanet(9999, updatedPlanet);

        Assert.False(result);
    }

    [Fact]
    public async Task DeletePlanet_ShouldRemovePlanet()
    {
        var dbContext = GetInMemoryDbContext();
        var planetService = new PlanetService(dbContext);
        var planet = new Planet { Name = "Endor", Climate = "Temperate" };
        dbContext.Planets.Add(planet);
        await dbContext.SaveChangesAsync();

        var result = await planetService.DeletePlanet(planet.Id);

        Assert.True(result);
        Assert.Equal(0, await dbContext.Planets.CountAsync());
    }

    [Fact]
    public async Task DeletePlanet_ShouldReturnFalseForNonExistentId()
    {
        var dbContext = GetInMemoryDbContext();
        var planetService = new PlanetService(dbContext);

        var result = await planetService.DeletePlanet(9999);

        Assert.False(result);
    }
}
