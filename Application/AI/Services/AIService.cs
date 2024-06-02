using Application.AI.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Application.AI.Services
{
    public class AIService : IAIService
    {
        private readonly HttpClient _httpClient;

        public AIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //public async Task<string> GetResponseAsync(string input)
        //{
        //    var openAiApiKey = "";
        //    _httpClient.DefaultRequestHeaders.Clear();
        //    _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {openAiApiKey}");

        //    var requestBody = new
        //    {
        //        model = "gpt-3.5-turbo",
        //        messages = new[]
        //        {
        //            new { role = "user", content = input }
        //        },
        //        max_tokens = 150
        //    };

        //    var response = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestBody);
        //    response.EnsureSuccessStatusCode();

        //    var result = await JsonSerializer.DeserializeAsync<OpenAIResponse>(await response.Content.ReadAsStreamAsync());
        //    return result.Choices.FirstOrDefault()?.Message.Content.Trim();
        //}

        //private class OpenAIResponse
        //{
        //    public IEnumerable<Choice> Choices { get; set; }

        //    public class Choice
        //    {
        //        public Message Message { get; set; }
        //    }
        //}

        //private class Message
        //{
        //    public string Role { get; set; }
        //    public string Content { get; set; }
        //}
    }
}
