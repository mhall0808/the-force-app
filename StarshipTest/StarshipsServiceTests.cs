using Microsoft.EntityFrameworkCore;

public class StarshipServiceTests
{
    private TheForceDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<TheForceDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new TheForceDbContext(options);
    }

    [Fact]
    public async Task CreateStarship_ShouldAddStarship()
    {
        var dbContext = GetInMemoryDbContext();
        var starshipService = new StarshipService(dbContext);
        var starship = new Starship
        {
            Name = "Millennium Falcon",
            Model = "YT-1300 light freighter",
            Manufacturer = "Corellian Engineering Corporation"
        };

        var result = await starshipService.CreateStarship(starship);

        Assert.NotNull(result);
        Assert.Equal("Millennium Falcon", result.Name);
        Assert.Equal(1, await dbContext.Starships.CountAsync());
    }

    [Fact]
    public async Task GetAllStarships_ShouldReturnAllStarships()
    {
        var dbContext = GetInMemoryDbContext();
        var starshipService = new StarshipService(dbContext);
        dbContext.Starships.Add(new Starship { Name = "Starship 1", Model = "Model 1" });
        dbContext.Starships.Add(new Starship { Name = "Starship 2", Model = "Model 2" });
        await dbContext.SaveChangesAsync();

        var starships = await starshipService.GetAllStarships();

        Assert.Equal(2, starships.Count());
    }

    [Fact]
    public async Task GetStarshipById_ShouldReturnStarship()
    {
        var dbContext = GetInMemoryDbContext();
        var starshipService = new StarshipService(dbContext);
        var starship = new Starship { Name = "X-Wing", Model = "T-65 X-wing" };
        dbContext.Starships.Add(starship);
        await dbContext.SaveChangesAsync();

        var result = await starshipService.GetStarshipById(starship.Id);

        Assert.NotNull(result);
        Assert.Equal("X-Wing", result.Name);
    }

    [Fact]
    public async Task UpdateStarship_ShouldModifyStarship()
    {
        var dbContext = GetInMemoryDbContext();
        var starshipService = new StarshipService(dbContext);
        var starship = new Starship { Name = "Old Name", Model = "Old Model" };
        dbContext.Starships.Add(starship);
        await dbContext.SaveChangesAsync();

        var updatedStarship = new Starship { Name = "Millennium Falcon", Model = "YT-1300 light freighter" };

        var result = await starshipService.UpdateStarship(starship.Id, updatedStarship);

        Assert.True(result);
        var starshipFromDb = await dbContext.Starships.FindAsync(starship.Id);
        Assert.Equal("Millennium Falcon", starshipFromDb.Name);
    }

    [Fact]
    public async Task UpdateStarship_ShouldReturnFalseForNonExistentId()
    {
        var dbContext = GetInMemoryDbContext();
        var starshipService = new StarshipService(dbContext);

        var updatedStarship = new Starship { Name = "Slave I", Model = "Firespray-31-class patrol and attack craft" };

        var result = await starshipService.UpdateStarship(9999, updatedStarship);

        Assert.False(result);
    }

    [Fact]
    public async Task DeleteStarship_ShouldRemoveStarship()
    {
        var dbContext = GetInMemoryDbContext();
        var starshipService = new StarshipService(dbContext);
        var starship = new Starship { Name = "Imperial Star Destroyer", Model = "Imperial I-class" };
        dbContext.Starships.Add(starship);
        await dbContext.SaveChangesAsync();

        var result = await starshipService.DeleteStarship(starship.Id);

        Assert.True(result);
        Assert.Equal(0, await dbContext.Starships.CountAsync());
    }

    [Fact]
    public async Task DeleteStarship_ShouldReturnFalseForNonExistentId()
    {
        var dbContext = GetInMemoryDbContext();
        var starshipService = new StarshipService(dbContext);

        var result = await starshipService.DeleteStarship(9999);

        Assert.False(result);
    }
}
