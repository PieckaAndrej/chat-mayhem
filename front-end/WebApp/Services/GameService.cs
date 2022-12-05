using Data.ModelLayer;
using RestSharp;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApp.DTOs;
using WebApp.Models;

namespace WebApp.Services
{
    public class GameService
    {
        private readonly RestClient _client;

        public GameService()
        {
            _client = new RestClient("https://localhost:7200/");
        }

        public async Task<Game?> CreateGame(GameDto game)
        {
            var request = new RestRequest("api/Game").AddJsonBody(game);
            var result = await _client.ExecutePostAsync(request);

            return JsonSerializer.Deserialize<Game>(result.Content);
        }
    }
}
