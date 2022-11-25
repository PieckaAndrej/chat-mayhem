using RestSharp;
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

        public async Task<RestResponse<Game>> CreateGame(Game game)
        {
            var request = new RestRequest("api/Game").AddJsonBody(game);
            return await _client.ExecuteGetAsync<Game>(request);
        }
    }
}
