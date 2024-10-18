using Microsoft.EntityFrameworkCore;

public class SpeciesService
{
    private readonly TheForceDbContext _context;

    public SpeciesService(TheForceDbContext context)
    {
        _context = context;
    }

    // Create a new Species
    public async Task<Species> CreateSpecies(Species species)
    {
        _context.Species.Add(species);
        await _context.SaveChangesAsync();
        return species;
    }

    // Get all Species
    public async Task<IEnumerable<Species>> GetAllSpecies()
    {
        return await _context.Species.ToListAsync();
    }

    // Get a specific Species by Id
    public async Task<Species> GetSpeciesById(int id)
    {
        return await _context.Species.FirstOrDefaultAsync(s => s.Id == id);
    }

    // Update an existing Species
    public async Task<bool> UpdateSpecies(int id, Species updatedSpecies)
    {
        var existingSpecies = await _context.Species.FindAsync(id);
        if (existingSpecies == null)
        {
            return false;
        }

        existingSpecies.Name = updatedSpecies.Name;
        existingSpecies.Classification = updatedSpecies.Classification;
        existingSpecies.Designation = updatedSpecies.Designation;
        existingSpecies.AverageHeight = updatedSpecies.AverageHeight;
        existingSpecies.SkinColors = updatedSpecies.SkinColors;
        existingSpecies.HairColors = updatedSpecies.HairColors;
        existingSpecies.EyeColors = updatedSpecies.EyeColors;
        existingSpecies.AverageLifespan = updatedSpecies.AverageLifespan;
        existingSpecies.Language = updatedSpecies.Language;
        existingSpecies.Homeworld = updatedSpecies.Homeworld;
        existingSpecies.People = updatedSpecies.People;
        existingSpecies.Films = updatedSpecies.Films;
        existingSpecies.Edited = DateTime.Now;

        _context.Species.Update(existingSpecies);
        await _context.SaveChangesAsync();

        return true;
    }

    // Delete a Species by Id
    public async Task<bool> DeleteSpecies(int id)
    {
        var species = await _context.Species.FindAsync(id);
        if (species == null)
        {
            return false;
        }

        _context.Species.Remove(species);
        await _context.SaveChangesAsync();
        return true;
    }
}
