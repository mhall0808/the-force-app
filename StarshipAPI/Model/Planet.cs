public class Planet
{
    public int Id { get; set; } // Extracted from the URL, e.g., "/api/planets/1/"
    public string? Name { get; set; }
    public string? RotationPeriod { get; set; }
    public string? OrbitalPeriod { get; set; }
    public string? Diameter { get; set; }
    public string? Climate { get; set; }
    public string? Gravity { get; set; }
    public string? Terrain { get; set; }
    public string? SurfaceWater { get; set; }
    public string? Population { get; set; }
    
    // List of URLs for residents and films
    public List<string> Residents { get; set; } = new List<string>();
    public List<string> Films { get; set; } = new List<string>();

    public DateTime? Created { get; set; }
    public DateTime? Edited { get; set; }
    public string? Url { get; set; }
}
