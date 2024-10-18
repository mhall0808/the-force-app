using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class FilmService
{
    private readonly TheForceDbContext _context;

    public FilmService(TheForceDbContext context)
    {
        _context = context;
    }

    // Create a new Film
    public async Task<Film> CreateFilm(Film film)
    {
        _context.Films.Add(film);
        await _context.SaveChangesAsync();
        return film;
    }

    // Get all Films
    public async Task<IEnumerable<Film>> GetAllFilms()
    {
        return await _context.Films.ToListAsync();
    }

    // Get a specific Film by Id
    public async Task<Film> GetFilmById(int id)
    {
        var film = await _context.Films.FirstOrDefaultAsync(f => f.Id == id);
        return film ?? throw new KeyNotFoundException($"Film with id {id} not found.");
    }

    // Update an existing Film
    public async Task<bool> UpdateFilm(int id, Film updatedFilm)
    {
        var existingFilm = await _context.Films.FindAsync(id);
        if (existingFilm == null)
        {
            return false;
        }

        existingFilm.Title = updatedFilm.Title;
        existingFilm.EpisodeId = updatedFilm.EpisodeId;
        existingFilm.OpeningCrawl = updatedFilm.OpeningCrawl;
        existingFilm.Director = updatedFilm.Director;
        existingFilm.Producer = updatedFilm.Producer;
        existingFilm.ReleaseDate = updatedFilm.ReleaseDate;
        existingFilm.Edited = DateTime.Now;

        // Update related entities as string lists (characters, planets, etc.)
        existingFilm.Characters = updatedFilm.Characters;
        existingFilm.Planets = updatedFilm.Planets;
        existingFilm.Starships = updatedFilm.Starships;
        existingFilm.Vehicles = updatedFilm.Vehicles;
        existingFilm.Species = updatedFilm.Species;

        _context.Films.Update(existingFilm);
        await _context.SaveChangesAsync();

        return true;
    }

    // Delete a Film by Id
    public async Task<bool> DeleteFilm(int id)
    {
        var film = await _context.Films.FindAsync(id);
        if (film == null)
        {
            return false;
        }

        _context.Films.Remove(film);
        await _context.SaveChangesAsync();
        return true;
    }

    // Get Films by Director (Example of a custom query)
    public async Task<IEnumerable<Film>> GetFilmsByDirector(string director)
    {
        return await _context.Films
            .Where(f => f.Director == director)
            .ToListAsync();
    }
}
