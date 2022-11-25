using RestSharp;
using System.Text.Json;
using WebApp.Hubs;
using WebApp.Models;

namespace WebApp.Services
{
    public class ChatService
    {
        private readonly RestClient _client;

        private ChatHub chatHub;

        public ChatService()
        {
            _client = new RestClient("https://localhost:7200/");
        }

        public async Task<string> CheckViewerAnswer(string answer)
        {
            var request = new RestRequest("api/Chat").AddParameter("sentence", answer);

            var response = await _client.ExecuteGetAsync(request);

            return JsonSerializer.Deserialize<string>(response.Content);
        }
    }
}
