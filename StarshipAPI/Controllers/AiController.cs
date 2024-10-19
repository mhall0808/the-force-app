using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/ai")]
public class AiController : ControllerBase
{
    private readonly OllamaService _ollamaService;

    public AiController(OllamaService ollamaService)
    {
        _ollamaService = ollamaService;
    }

    [HttpPost("generate-crawl-paragraph")]
    public async Task<IActionResult> GenerateCrawlParagraph([FromBody] ParagraphRequest request)
    {
        var prompt = GeneratePrompt(request.SelectedData, request.ParagraphNumber);

        var paragraphText = await _ollamaService.GenerateCrawlAsync(prompt);

        return Ok(new { paragraphText });
    }

    private string GeneratePrompt(SelectedData data, int paragraphNumber)
    {
        string paragraphInstructions = "";

        switch (paragraphNumber)
        {
            case 1:
                paragraphInstructions = "Introduce Darth Lord Mark and his treachery across the land.";
                break;
            case 2:
                paragraphInstructions = $"Introduce the GoEngineer rebellion, led by Vitali, Royce, and Francisco. They are on {data.Planet.Name} and protecting {data.Species.Name}.";
                break;
            case 3:
                paragraphInstructions = $"Introduce {data.Person.Name} who will ride in with his {data.Vehicle.Name} to save the day.";
                break;
            case 4:
                paragraphInstructions = $"Darth Lord Mark is aware of this plot and is coming in on his {data.Starship.Name} - can {data.Person.Name} save the day?";
                break;
            default:
                paragraphInstructions = "";
                break;
        }

        return $@"
Please generate a paragraph for a Star Wars-style crawl based on the instructions below.

Instructions:
- Do not include any explanations or extra text.
- Output only the paragraph text.
- The paragraph should be 80 words or less.
- {paragraphInstructions}

Now, generate the paragraph accordingly.
";
    }
}

// Models/ParagraphRequest.cs

public class ParagraphRequest
{
    public SelectedData SelectedData { get; set; }
    public int ParagraphNumber { get; set; }
}

// Models/SelectedData.cs

public class SelectedData
{
    public Person Person { get; set; }
    public Starship Starship { get; set; }
    public Planet Planet { get; set; }
    public Film Film { get; set; }
    public Vehicle Vehicle { get; set; }
    public Species Species { get; set; }
}
