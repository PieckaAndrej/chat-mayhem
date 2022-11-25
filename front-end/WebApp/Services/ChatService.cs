using RestSharp;
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

        public async Task<RestResponse<string>> CheckViewerAnswer(string answer)
        {
            var request = new RestRequest("api/Chat").AddJsonBody(answer);
            return await _client.ExecutePostAsync<string>(request);
        }
    }
}
