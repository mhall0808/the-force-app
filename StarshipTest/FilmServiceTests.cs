// FilmServiceTests.cs

using Microsoft.EntityFrameworkCore;

public class FilmServiceTests
{
    private TheForceDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<TheForceDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new TheForceDbContext(options);
    }

    [Fact]
    public async Task CreateFilm_ShouldAddFilm()
    {
        // Arrange
        var dbContext = GetInMemoryDbContext();
        var filmService = new FilmService(dbContext);
        var film = new Film
        {
            Title = "A New Hope",
            EpisodeId = 4,
            Director = "George Lucas",
            Producer = "Gary Kurtz, Rick McCallum",
            ReleaseDate = DateTime.Parse("1977-05-25")
        };

        // Act
        var result = await filmService.CreateFilm(film);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("A New Hope", result.Title);
        Assert.Equal(1, await dbContext.Films.CountAsync());
    }

    [Fact]
    public async Task GetAllFilms_ShouldReturnAllFilms()
    {
        // Arrange
        var dbContext = GetInMemoryDbContext();
        var filmService = new FilmService(dbContext);
        dbContext.Films.Add(new Film { Title = "Film 1", EpisodeId = 1 });
        dbContext.Films.Add(new Film { Title = "Film 2", EpisodeId = 2 });
        await dbContext.SaveChangesAsync();

        // Act
        var films = await filmService.GetAllFilms();

        // Assert
        Assert.Equal(2, films.Count());
    }

    [Fact]
    public async Task GetFilmById_ShouldReturnFilm()
    {
        // Arrange
        var dbContext = GetInMemoryDbContext();
        var filmService = new FilmService(dbContext);
        var film = new Film { Title = "The Empire Strikes Back", EpisodeId = 5 };
        dbContext.Films.Add(film);
        await dbContext.SaveChangesAsync();

        // Act
        var result = await filmService.GetFilmById(film.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("The Empire Strikes Back", result.Title);
    }

    [Fact]
    public async Task UpdateFilm_ShouldModifyFilm()
    {
        // Arrange
        var dbContext = GetInMemoryDbContext();
        var filmService = new FilmService(dbContext);
        var film = new Film { Title = "Old Title", EpisodeId = 6 };
        dbContext.Films.Add(film);
        await dbContext.SaveChangesAsync();

        var updatedFilm = new Film { Title = "Return of the Jedi", EpisodeId = 6 };

        // Act
        var result = await filmService.UpdateFilm(film.Id, updatedFilm);

        // Assert
        Assert.True(result);
        var filmFromDb = await dbContext.Films.FindAsync(film.Id);
        Assert.Equal("Return of the Jedi", filmFromDb.Title);
    }

    [Fact]
    public async Task DeleteFilm_ShouldRemoveFilm()
    {
        // Arrange
        var dbContext = GetInMemoryDbContext();
        var filmService = new FilmService(dbContext);
        var film = new Film { Title = "Phantom Menace", EpisodeId = 1 };
        dbContext.Films.Add(film);
        await dbContext.SaveChangesAsync();

        // Act
        var result = await filmService.DeleteFilm(film.Id);

        // Assert
        Assert.True(result);
        Assert.Equal(0, await dbContext.Films.CountAsync());
    }
}
