using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TheForceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register HttpClient for API calls
builder.Services.AddHttpClient();

// Register GenericSeeder
builder.Services.AddScoped<GenericSeeder>();

// Add CORS support
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Register all six services
builder.Services.AddScoped<FilmService>();
builder.Services.AddScoped<PersonService>();
builder.Services.AddScoped<PlanetService>();
builder.Services.AddScoped<SpeciesService>();
builder.Services.AddScoped<StarshipService>();
builder.Services.AddScoped<VehicleService>();
builder.Services.AddScoped<OllamaService>();

// Add Swagger for API documentation (optional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add controllers
builder.Services.AddControllers();

// Build the application
var app = builder.Build();

// Automatically apply any pending migrations and seed the database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TheForceDbContext>();
    var ollamaService = scope.ServiceProvider.GetRequiredService<OllamaService>();

    try
    {
        Console.WriteLine("Applying database migrations...");
        await dbContext.Database.MigrateAsync();
        Console.WriteLine("Database migrations applied successfully.");

        // Check if the database is already seeded
        if (!dbContext.Films.Any())
        {
            var seeder = scope.ServiceProvider.GetRequiredService<GenericSeeder>();
            Console.WriteLine("Seeding the database...");
            await seeder.SeedAllEntitiesAsync();
            Console.WriteLine("Database seeding completed.");
        }
        else
        {
            Console.WriteLine("Database already seeded. Skipping seeding.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error applying migrations or seeding: {ex.Message}");
    }
}


// Enable CORS (after building the app)
app.UseCors("AllowAllOrigins");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
