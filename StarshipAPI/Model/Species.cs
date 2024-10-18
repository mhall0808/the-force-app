public class Species
{
    public int Id { get; set; } // Extracted from the URL, e.g., "/api/species/1/"
    public string? Name { get; set; }
    public string? Classification { get; set; }
    public string? Designation { get; set; }
    public string? AverageHeight { get; set; }
    public string? SkinColors { get; set; }
    public string? HairColors { get; set; }
    public string? EyeColors { get; set; }
    public string? AverageLifespan { get; set; }
    public string? Language { get; set; }
    public string? Homeworld { get; set; }
    
    // Lists of URLs for related entities
    public List<string> People { get; set; } = new List<string>();
    public List<string> Films { get; set; } = new List<string>();

    public DateTime? Created { get; set; }
    public DateTime? Edited { get; set; }
    public string? Url { get; set; }
}
