using Microsoft.EntityFrameworkCore;

public class VehicleServiceTests
{
    private TheForceDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<TheForceDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new TheForceDbContext(options);
    }

    [Fact]
    public async Task CreateVehicle_ShouldAddVehicle()
    {
        var dbContext = GetInMemoryDbContext();
        var vehicleService = new VehicleService(dbContext);
        var vehicle = new Vehicle
        {
            Name = "Speeder Bike",
            Model = "74-Z speeder bike",
            Manufacturer = "Aratech Repulsor Company"
        };

        var result = await vehicleService.CreateVehicle(vehicle);

        Assert.NotNull(result);
        Assert.Equal("Speeder Bike", result.Name);
        Assert.Equal(1, await dbContext.Vehicles.CountAsync());
    }

    [Fact]
    public async Task GetAllVehicles_ShouldReturnAllVehicles()
    {
        var dbContext = GetInMemoryDbContext();
        var vehicleService = new VehicleService(dbContext);
        dbContext.Vehicles.Add(new Vehicle { Name = "Vehicle 1", Model = "Model 1" });
        dbContext.Vehicles.Add(new Vehicle { Name = "Vehicle 2", Model = "Model 2" });
        await dbContext.SaveChangesAsync();

        var vehicles = await vehicleService.GetAllVehicles();

        Assert.Equal(2, vehicles.Count());
    }

    [Fact]
    public async Task GetVehicleById_ShouldReturnVehicle()
    {
        var dbContext = GetInMemoryDbContext();
        var vehicleService = new VehicleService(dbContext);
        var vehicle = new Vehicle { Name = "AT-AT", Model = "All Terrain Armored Transport" };
        dbContext.Vehicles.Add(vehicle);
        await dbContext.SaveChangesAsync();

        var result = await vehicleService.GetVehicleById(vehicle.Id);

        Assert.NotNull(result);
        Assert.Equal("AT-AT", result.Name);
    }

    [Fact]
    public async Task UpdateVehicle_ShouldModifyVehicle()
    {
        var dbContext = GetInMemoryDbContext();
        var vehicleService = new VehicleService(dbContext);
        var vehicle = new Vehicle { Name = "Old Name", Model = "Old Model" };
        dbContext.Vehicles.Add(vehicle);
        await dbContext.SaveChangesAsync();

        var updatedVehicle = new Vehicle { Name = "AT-ST", Model = "All Terrain Scout Transport" };

        var result = await vehicleService.UpdateVehicle(vehicle.Id, updatedVehicle);

        Assert.True(result);
        var vehicleFromDb = await dbContext.Vehicles.FindAsync(vehicle.Id);
        Assert.Equal("AT-ST", vehicleFromDb.Name);
    }

    [Fact]
    public async Task UpdateVehicle_ShouldReturnFalseForNonExistentId()
    {
        var dbContext = GetInMemoryDbContext();
        var vehicleService = new VehicleService(dbContext);

        var updatedVehicle = new Vehicle { Name = "TIE Fighter", Model = "TIE/LN starfighter" };

        var result = await vehicleService.UpdateVehicle(9999, updatedVehicle);

        Assert.False(result);
    }

    [Fact]
    public async Task DeleteVehicle_ShouldRemoveVehicle()
    {
        var dbContext = GetInMemoryDbContext();
        var vehicleService = new VehicleService(dbContext);
        var vehicle = new Vehicle { Name = "Snowspeeder", Model = "T-47 airspeeder" };
        dbContext.Vehicles.Add(vehicle);
        await dbContext.SaveChangesAsync();

        var result = await vehicleService.DeleteVehicle(vehicle.Id);

        Assert.True(result);
        Assert.Equal(0, await dbContext.Vehicles.CountAsync());
    }

    [Fact]
    public async Task DeleteVehicle_ShouldReturnFalseForNonExistentId()
    {
        var dbContext = GetInMemoryDbContext();
        var vehicleService = new VehicleService(dbContext);

        var result = await vehicleService.DeleteVehicle(9999);

        Assert.False(result);
    }
}
