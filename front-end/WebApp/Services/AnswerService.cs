using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;
using WebApp.Hubs;
using WebApp.Models;

namespace WebApp.Services
{
    public class AnswerService
    {
        private readonly RestClient _client;

        private GameHub chatHub;

        public AnswerService()
        {
            _client = new RestClient("https://localhost:7200/");
        }

        public static async Task<Dictionary<string, double>?> CheckAnswer(string sentence, List<Answer> answers)
        {
            RestClient client = new RestClient("https://www.api.secretpizza.dk/");
            var request = new RestRequest("sentences");
            List<string?> stringList = answers.Select(ans => ans.Text).ToList();

            request.AddJsonBody(new { sentence = sentence, list = stringList });

            var response = await client.ExecuteGetAsync(request);

            return JsonSerializer.Deserialize<Dictionary<string, double>>(response.Content);
        }
    }
}
