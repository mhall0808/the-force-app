using Microsoft.EntityFrameworkCore;

public class VehicleService
{
    private readonly TheForceDbContext _context;

    public VehicleService(TheForceDbContext context)
    {
        _context = context;
    }

    // Create a new Vehicle
    public async Task<Vehicle> CreateVehicle(Vehicle vehicle)
    {
        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync();
        return vehicle;
    }

    // Get all Vehicles
    public async Task<IEnumerable<Vehicle>> GetAllVehicles()
    {
        return await _context.Vehicles.ToListAsync();
    }

    // Get a specific Vehicle by Id
    public async Task<Vehicle> GetVehicleById(int id)
    {
        return await _context.Vehicles.FirstOrDefaultAsync(v => v.Id == id);
    }

    // Update an existing Vehicle
    public async Task<bool> UpdateVehicle(int id, Vehicle updatedVehicle)
    {
        var existingVehicle = await _context.Vehicles.FindAsync(id);
        if (existingVehicle == null)
        {
            return false;
        }

        existingVehicle.Name = updatedVehicle.Name;
        existingVehicle.Model = updatedVehicle.Model;
        existingVehicle.Manufacturer = updatedVehicle.Manufacturer;
        existingVehicle.CostInCredits = updatedVehicle.CostInCredits;
        existingVehicle.Length = updatedVehicle.Length;
        existingVehicle.MaxAtmospheringSpeed = updatedVehicle.MaxAtmospheringSpeed;
        existingVehicle.Crew = updatedVehicle.Crew;
        existingVehicle.Passengers = updatedVehicle.Passengers;
        existingVehicle.CargoCapacity = updatedVehicle.CargoCapacity;
        existingVehicle.Consumables = updatedVehicle.Consumables;
        existingVehicle.VehicleClass = updatedVehicle.VehicleClass;
        existingVehicle.Pilots = updatedVehicle.Pilots;
        existingVehicle.Films = updatedVehicle.Films;
        existingVehicle.Edited = DateTime.Now;

        _context.Vehicles.Update(existingVehicle);
        await _context.SaveChangesAsync();

        return true;
    }

    // Delete a Vehicle by Id
    public async Task<bool> DeleteVehicle(int id)
    {
        var vehicle = await _context.Vehicles.FindAsync(id);
        if (vehicle == null)
        {
            return false;
        }

        _context.Vehicles.Remove(vehicle);
        await _context.SaveChangesAsync();
        return true;
    }
}
