using Data.ModelLayer;
using RestSharp;
using RestSharp.Authenticators.OAuth2;
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

            RestRequest restRequest = new RestRequest("api/QuestionPack");

            await LoginAccess.GetToken("admin", "admin");

            if (!String.IsNullOrEmpty(LoginAccess.token))
            {
                _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(LoginAccess.token, "Bearer");
                var result = await _client.ExecutePostAsync(request);
                return JsonSerializer.Deserialize<Game>(result.Content);
            }
            else
            {
                return null;
            }
        }
    }
}
