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
    public class ChatController
    {
        private ChatService chatService;

        private readonly RestClient _client;

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

            var request = new RestRequest("sentences").AddParameter("sentence", sentence).AddBody(viewerAnswers);

            var response = await _client.ExecuteGetAsync(request);

            int similarityNumber = JsonSerializer.Deserialize<int>(response.Content);

            if (similarityNumber >= 0.2)
            {
                return sentence;
            }
            else
            {
                return "Wrong";
            }
        }

    }
}
