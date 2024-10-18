using Microsoft.EntityFrameworkCore;

public class PlanetService
{
    private readonly TheForceDbContext _context;

    public PlanetService(TheForceDbContext context)
    {
        _context = context;
    }

    // Create a new Planet
    public async Task<Planet> CreatePlanet(Planet planet)
    {
        _context.Planets.Add(planet);
        await _context.SaveChangesAsync();
        return planet;
    }

    // Get all Planets
    public async Task<IEnumerable<Planet>> GetAllPlanets()
    {
        return await _context.Planets.ToListAsync();
    }

    // Get a specific Planet by Id
    public async Task<Planet> GetPlanetById(int id)
    {
        return await _context.Planets.FirstOrDefaultAsync(p => p.Id == id);
    }

    // Update an existing Planet
    public async Task<bool> UpdatePlanet(int id, Planet updatedPlanet)
    {
        var existingPlanet = await _context.Planets.FindAsync(id);
        if (existingPlanet == null)
        {
            return false;
        }

        existingPlanet.Name = updatedPlanet.Name;
        existingPlanet.RotationPeriod = updatedPlanet.RotationPeriod;
        existingPlanet.OrbitalPeriod = updatedPlanet.OrbitalPeriod;
        existingPlanet.Diameter = updatedPlanet.Diameter;
        existingPlanet.Climate = updatedPlanet.Climate;
        existingPlanet.Gravity = updatedPlanet.Gravity;
        existingPlanet.Terrain = updatedPlanet.Terrain;
        existingPlanet.SurfaceWater = updatedPlanet.SurfaceWater;
        existingPlanet.Population = updatedPlanet.Population;
        existingPlanet.Residents = updatedPlanet.Residents;
        existingPlanet.Films = updatedPlanet.Films;
        existingPlanet.Edited = DateTime.Now;

        _context.Planets.Update(existingPlanet);
        await _context.SaveChangesAsync();

        return true;
    }

    // Delete a Planet by Id
    public async Task<bool> DeletePlanet(int id)
    {
        var planet = await _context.Planets.FindAsync(id);
        if (planet == null)
        {
            return false;
        }

        _context.Planets.Remove(planet);
        await _context.SaveChangesAsync();
        return true;
    }
}
