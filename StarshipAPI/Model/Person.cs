public class Person
{
    public int Id { get; set; } // Extracted from the URL, e.g., "/api/people/1/"
    public string? Name { get; set; }
    public string? Height { get; set; }
    public string? Mass { get; set; }
    public string? HairColor { get; set; }
    public string? SkinColor { get; set; }
    public string? EyeColor { get; set; }
    public string? BirthYear { get; set; }
    public string? Gender { get; set; }
    public string? Homeworld { get; set; }
    public List<string>? Films { get; set; } = new List<string>(); // List of film URLs
    public List<string>? Species { get; set; } = new List<string>(); // List of species URLs
    public List<string>? Vehicles { get; set; } = new List<string>(); // List of vehicle URLs
    public List<string>? Starships { get; set; } = new List<string>(); // List of starship URLs
    public DateTime? Created { get; set; }
    public DateTime? Edited { get; set; }
    public string? Url { get; set; }
}
