using Microsoft.EntityFrameworkCore;

public class TheForceDbContext : DbContext
{
    public DbSet<Starship> Starships { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<Film> Films { get; set; }
    public DbSet<Planet> Planets { get; set; }
    public DbSet<Species> Species { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }

    public TheForceDbContext(DbContextOptions<TheForceDbContext> options)
        : base(options)
    {
    }

}
