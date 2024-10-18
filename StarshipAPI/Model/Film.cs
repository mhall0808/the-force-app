public class Film
{
    public int Id { get; set; } // Extracted from the URL, e.g., "/api/films/1/"
    public string? Title { get; set; }
    public int? EpisodeId { get; set; }
    public string? OpeningCrawl { get; set; }
    public string? Director { get; set; }
    public string? Producer { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public DateTime? Created { get; set; }
    public DateTime? Edited { get; set; }
    public string? Url { get; set; }

    // List of related entities' URLs
    public List<string>? Characters { get; set; }
    public List<string>? Planets { get; set; }
    public List<string>? Starships { get; set; }
    public List<string>? Vehicles { get; set; }
    public List<string>? Species { get; set; }
}
