using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using SaveMyNotes.Application.Common.Interfaces;

namespace SaveMyNotes.Infrastructure.Services.AI;

public class AIService : IAIService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private const string ApiUrl = "https://api.groq.com/openai/v1/chat/completions";

    public AIService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _apiKey = configuration["AI:GroqApiKey"] ?? throw new ArgumentNullException("Groq API Key bulunamadı!");
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<List<string>> ExtractTagsAsync(string content)
    {
        if (string.IsNullOrWhiteSpace(content)) return new List<string>();

        try
        {
            var requestBody = new
            {
                model = "llama-3.3-70b-versatile",
                messages = new[]
                {
                    new {
                        role = "system",
                        content = "Sen bir anahtar kelime çıkarıcı robotsun. Sana verilen metni analiz et ve en önemli, konuyu en iyi temsil eden 5 anahtar kelimeyi aralarında sadece virgül olacak şekilde dön. Ekstra açıklama yapma, cümle kurma, sadece kelimeleri yaz."
                    },
                    new { role = "user", content = content }
                },
                temperature = 0.2 // Daha tutarlı sonuçlar için düşük tutuyoruz
            };

            var request = new HttpRequestMessage(HttpMethod.Post, ApiUrl);
            request.Headers.Add("Authorization", $"Bearer {_apiKey}");
            request.Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode) return new List<string>();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(jsonResponse);

            var tagsRaw = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            if (string.IsNullOrEmpty(tagsRaw)) return new List<string>();

            return tagsRaw.Split(',')
                .Select(t => t.Trim().ToLower().Replace(".", ""))
                .Where(t => t.Length > 2)
                .Distinct()
                .Take(5)
                .ToList();
        }
        catch (Exception)
        {
            // API hatası durumunda uygulamanın çökmemesi için boş liste dönüyoruz
            return new List<string>();
        }
    }
}