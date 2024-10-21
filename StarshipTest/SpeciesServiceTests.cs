using Microsoft.EntityFrameworkCore;

public class SpeciesServiceTests
{
    private TheForceDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<TheForceDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new TheForceDbContext(options);
    }

    [Fact]
    public async Task CreateSpecies_ShouldAddSpecies()
    {
        var dbContext = GetInMemoryDbContext();
        var speciesService = new SpeciesService(dbContext);
        var species = new Species
        {
            Name = "Wookiee",
            Classification = "Mammal",
            Language = "Shyriiwook"
        };

        var result = await speciesService.CreateSpecies(species);

        Assert.NotNull(result);
        Assert.Equal("Wookiee", result.Name);
        Assert.Equal(1, await dbContext.Species.CountAsync());
    }

    [Fact]
    public async Task GetAllSpecies_ShouldReturnAllSpecies()
    {
        var dbContext = GetInMemoryDbContext();
        var speciesService = new SpeciesService(dbContext);
        dbContext.Species.Add(new Species { Name = "Species 1", Classification = "Class 1" });
        dbContext.Species.Add(new Species { Name = "Species 2", Classification = "Class 2" });
        await dbContext.SaveChangesAsync();

        var speciesList = await speciesService.GetAllSpecies();

        Assert.Equal(2, speciesList.Count());
    }

    [Fact]
    public async Task GetSpeciesById_ShouldReturnSpecies()
    {
        var dbContext = GetInMemoryDbContext();
        var speciesService = new SpeciesService(dbContext);
        var species = new Species { Name = "Ewok", Classification = "Mammal" };
        dbContext.Species.Add(species);
        await dbContext.SaveChangesAsync();

        var result = await speciesService.GetSpeciesById(species.Id);

        Assert.NotNull(result);
        Assert.Equal("Ewok", result.Name);
    }

    [Fact]
    public async Task UpdateSpecies_ShouldModifySpecies()
    {
        var dbContext = GetInMemoryDbContext();
        var speciesService = new SpeciesService(dbContext);
        var species = new Species { Name = "Old Name", Classification = "Old Class" };
        dbContext.Species.Add(species);
        await dbContext.SaveChangesAsync();

        var updatedSpecies = new Species { Name = "Wookiee", Classification = "Mammal" };

        var result = await speciesService.UpdateSpecies(species.Id, updatedSpecies);

        Assert.True(result);
        var speciesFromDb = await dbContext.Species.FindAsync(species.Id);
        Assert.Equal("Wookiee", speciesFromDb.Name);
    }

    [Fact]
    public async Task UpdateSpecies_ShouldReturnFalseForNonExistentId()
    {
        var dbContext = GetInMemoryDbContext();
        var speciesService = new SpeciesService(dbContext);

        var updatedSpecies = new Species { Name = "Gungan", Classification = "Amphibian" };

        var result = await speciesService.UpdateSpecies(9999, updatedSpecies);

        Assert.False(result);
    }

    [Fact]
    public async Task DeleteSpecies_ShouldRemoveSpecies()
    {
        var dbContext = GetInMemoryDbContext();
        var speciesService = new SpeciesService(dbContext);
        var species = new Species { Name = "Gungan", Classification = "Amphibian" };
        dbContext.Species.Add(species);
        await dbContext.SaveChangesAsync();

        var result = await speciesService.DeleteSpecies(species.Id);

        Assert.True(result);
        Assert.Equal(0, await dbContext.Species.CountAsync());
    }

    [Fact]
    public async Task DeleteSpecies_ShouldReturnFalseForNonExistentId()
    {
        var dbContext = GetInMemoryDbContext();
        var speciesService = new SpeciesService(dbContext);

        var result = await speciesService.DeleteSpecies(9999);

        Assert.False(result);
    }
}
