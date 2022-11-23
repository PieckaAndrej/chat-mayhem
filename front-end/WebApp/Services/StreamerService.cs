using RestSharp;
using RestSharp.Authenticators.OAuth2;
using System.Text.Json;
using WebApp.DTOs;

namespace WebApp.Services
{
    public class StreamerService
    {
        public async Task<StreamerDto?> CreateStreamer(StreamerDto streamer)
        {
            RestClient restClient = new RestClient("https://localhost:7200/");
            RestRequest restRequest = new RestRequest("api/streamer");

            var response = await restClient.ExecutePostAsync(restRequest);

            return JsonSerializer.Deserialize<StreamerDto>(response.Content);
        }
    }
}
