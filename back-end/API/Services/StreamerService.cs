using API.Model;
using Data.DatabaseLayer;
using Data.ModelLayer;
using RestSharp;
using RestSharp.Authenticators.OAuth2;
using System.Text.Json;

namespace API.Services
{
    public class StreamerService
    {
        private IStreamerAccess _streamerAccess;

        public StreamerService(IStreamerAccess streamerAccess)
        {
            _streamerAccess = streamerAccess;
        }

        public Streamer Get(int id)
        {
            return _streamerAccess.GetStreamerById(id);
        }

        public Streamer Add(Streamer streamer)
        {
            return _streamerAccess.CreateStreamer(streamer);
        }

        public async Task<TwitchValidate?> ValidateToken(string token)
        {

            RestClient restClient = new RestClient("https://id.twitch.tv/");
            restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token);

            RestRequest restRequest = new RestRequest("oauth2/validate");

            var response = await restClient.ExecuteGetAsync(restRequest);

            Console.WriteLine(response.ResponseStatus);

            return JsonSerializer.Deserialize<TwitchValidate>(response.Content);
        }
    }
}
