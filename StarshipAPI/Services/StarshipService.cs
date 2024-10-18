using Microsoft.EntityFrameworkCore;

public class StarshipService
{
    private readonly TheForceDbContext _context;

    public StarshipService(TheForceDbContext context)
    {
        _context = context;
    }

    // Create a new Starship
    public async Task<Starship> CreateStarship(Starship starship)
    {
        _context.Starships.Add(starship);
        await _context.SaveChangesAsync();
        return starship;
    }

    // Get all Starships
    public async Task<IEnumerable<Starship>> GetAllStarships()
    {
        return await _context.Starships.ToListAsync();
    }

    // Get a specific Starship by Id
    public async Task<Starship> GetStarshipById(int id)
    {
        return await _context.Starships.FirstOrDefaultAsync(s => s.Id == id);
    }

    // Update an existing Starship
    public async Task<bool> UpdateStarship(int id, Starship updatedStarship)
    {
        var existingStarship = await _context.Starships.FindAsync(id);
        if (existingStarship == null)
        {
            return false;
        }

        existingStarship.Name = updatedStarship.Name;
        existingStarship.Model = updatedStarship.Model;
        existingStarship.Manufacturer = updatedStarship.Manufacturer;
        existingStarship.CostInCredits = updatedStarship.CostInCredits;
        existingStarship.Length = updatedStarship.Length;
        existingStarship.MaxAtmospheringSpeed = updatedStarship.MaxAtmospheringSpeed;
        existingStarship.Crew = updatedStarship.Crew;
        existingStarship.Passengers = updatedStarship.Passengers;
        existingStarship.CargoCapacity = updatedStarship.CargoCapacity;
        existingStarship.Consumables = updatedStarship.Consumables;
        existingStarship.HyperdriveRating = updatedStarship.HyperdriveRating;
        existingStarship.MGLT = updatedStarship.MGLT;
        existingStarship.StarshipClass = updatedStarship.StarshipClass;
        existingStarship.Pilots = updatedStarship.Pilots;
        existingStarship.Films = updatedStarship.Films;
        existingStarship.Edited = DateTime.Now;

        _context.Starships.Update(existingStarship);
        await _context.SaveChangesAsync();

        return true;
    }

    // Delete a Starship by Id
    public async Task<bool> DeleteStarship(int id)
    {
        var starship = await _context.Starships.FindAsync(id);
        if (starship == null)
        {
            return false;
        }

        _context.Starships.Remove(starship);
        await _context.SaveChangesAsync();
        return true;
    }
}
