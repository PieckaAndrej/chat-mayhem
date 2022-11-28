using API.DTOs;
using API.Services;
using Data.DatabaseLayer;
using Data.ModelLayer;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private ChatService chatService;

        private readonly RestClient _client;
        private const double ANSWER_THRESHOLD = 0.5;

        public ChatController(IConfiguration inConfiguration)
        {
            ServiceInjector.Con = inConfiguration.GetConnectionString("ChatMayhem Connection") ?? "No connection string";

            string? connectionString = inConfiguration.GetConnectionString("ChatMayhem Connection");
            if (connectionString == null)
            {
                Console.WriteLine("Connection string is null");
            }

            chatService = new ChatService(new ChatAccess(connectionString ?? "No connection string"));

            _client = new RestClient("https://www.api.secretpizza.dk/");
        }

        [HttpGet]
        public async Task<ActionResult<string>> CheckAnswer(string sentence)
        {
            List<string> viewerAnswers = ServiceInjector.ChatService.GetAnswers();

            var request = new RestRequest("sentences");

            request.AddJsonBody(new {sentence = sentence, list = viewerAnswers});

            var response = await _client.ExecuteGetAsync(request);

            var similarityNumbers = JsonSerializer.Deserialize<Dictionary<string, double>>(
                response.Content);

            KeyValuePair<string, double> pair = similarityNumbers.MaxBy(number => number.Value);

            if (pair.Value >= ANSWER_THRESHOLD)
            {
                return pair.Key;
            }
            else
            {
                return NotFound("wrong");
            }
        }

    }
}
