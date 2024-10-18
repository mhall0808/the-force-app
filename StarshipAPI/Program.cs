using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TheForceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register HttpClient for API calls
builder.Services.AddHttpClient();

// Register GenericSeeder
builder.Services.AddScoped<GenericSeeder>();

// Add Swagger for API documentation (optional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers(); 

var app = builder.Build();

// Automatically apply any pending migrations and seed the database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TheForceDbContext>();

    /* 
        There is an issue with seeding.  It was not waiting until the migrations were complete
        to perform the seed.  Creating a task and awaiting solves the problem.  
        NOTE - running this on my personal machine takes about 30 seconds to boot up.  
    */
    try
    {
        // Apply migrations
        Console.WriteLine("Applying database migrations...");
        await dbContext.Database.MigrateAsync();
        Console.WriteLine("Database migrations applied successfully.");

        // Seed the database once migrations are complete
        var seeder = scope.ServiceProvider.GetRequiredService<GenericSeeder>();
        Console.WriteLine("Seeding the database...");
        await seeder.SeedAllEntitiesAsync();
        Console.WriteLine("Database seeding completed.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error applying migrations or seeding: {ex.Message}");
    }
}

    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

// Use routing and map the controllers
app.UseRouting();
app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});

app.Run();
