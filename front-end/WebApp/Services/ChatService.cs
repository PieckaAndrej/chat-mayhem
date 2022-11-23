using RestSharp;
using WebApp.Models;

namespace WebApp.Services
{
    public class ChatService
    {
        private readonly RestClient _client;

        public ChatService()
        {
            _client = new RestClient("https://localhost:7200/");
        }

        public async Task<string> CheckViewerAnswer(string answer)
        {
            var request = new RestRequest("api/Chat").AddJsonBody(answer);
            var response = await _client.ExecutePostAsync<string>(request);

            return response.Content;
        }
    }
}
