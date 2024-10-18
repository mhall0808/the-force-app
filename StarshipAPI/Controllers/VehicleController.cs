using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/vehicles")]
public class VehicleController : ControllerBase
{
    private readonly VehicleService _vehicleService;

    public VehicleController(VehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    // GET: /vehicles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Vehicle>>> GetAllVehicles()
    {
        var vehicles = await _vehicleService.GetAllVehicles();
        return Ok(vehicles);
    }

    // GET: /vehicles/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Vehicle>> GetVehicleById(int id)
    {
        var vehicle = await _vehicleService.GetVehicleById(id);

        if (vehicle == null)
        {
            return NotFound();
        }

        return Ok(vehicle);
    }

    // POST: /vehicles
    [HttpPost]
    public async Task<ActionResult<Vehicle>> CreateVehicle(Vehicle vehicle)
    {
        var newVehicle = await _vehicleService.CreateVehicle(vehicle);
        return CreatedAtAction(nameof(GetVehicleById), new { id = newVehicle.Id }, newVehicle);
    }

    // PUT: /vehicles/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVehicle(int id, Vehicle vehicle)
    {
        var result = await _vehicleService.UpdateVehicle(id, vehicle);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: /vehicles/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVehicle(int id)
    {
        var result = await _vehicleService.DeleteVehicle(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
