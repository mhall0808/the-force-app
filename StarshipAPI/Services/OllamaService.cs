using System.Text;
using Newtonsoft.Json;
using System.IO;

public class OllamaService
{
    private readonly HttpClient _httpClient;

    public OllamaService(IConfiguration configuration)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://ollama:11434/")  // Docker service name, not localhost
        };
    }

    public async Task<string> GenerateCrawlAsync(string prompt)
    {
        var requestBody = new
        {
            prompt = prompt,
            model = "qwen2.5:0.5b"
        };

        var response = await _httpClient.PostAsync("api/generate", new StringContent(
            JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json"));

        response.EnsureSuccessStatusCode();

        var responseStream = await response.Content.ReadAsStreamAsync();

        using (var streamReader = new StreamReader(responseStream))
        {
            string output = string.Empty;
            string line;

            while ((line = await streamReader.ReadLineAsync()) != null)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    // Deserialize each line of NDJSON
                    dynamic result = JsonConvert.DeserializeObject(line);
                    output += result.response.ToString();  // Concatenate or process as needed
                }
            }

            return output;
        }
    }
}
