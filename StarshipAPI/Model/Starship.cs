public class Starship
{
    public int Id { get; set; } // Extracted from the URL, e.g., "/api/starships/2/"
    public string? Name { get; set; }
    public string? Model { get; set; }
    public string? Manufacturer { get; set; }
    public string? CostInCredits { get; set; } // Kept as string? due to "unknown" values
    public string? Length { get; set; }
    public string? MaxAtmospheringSpeed { get; set; }
    public string? Crew { get; set; }
    public string? Passengers { get; set; }
    public string? CargoCapacity { get; set; }
    public string? Consumables { get; set; }
    public string? HyperdriveRating { get; set; }
    public string? MGLT { get; set; }
    public string? StarshipClass { get; set; }
    
    // Lists of related entities stored as URLs
    public List<string> Pilots { get; set; } = new List<string>();
    public List<string> Films { get; set; } = new List<string>();

    public DateTime? Created { get; set; }
    public DateTime? Edited { get; set; }
    public string? Url { get; set; }
}
