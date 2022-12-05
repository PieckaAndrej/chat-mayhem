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

        public Streamer Get(string streamerId)
        {
            return _streamerAccess.GetStreamerById(streamerId);
        }

        public Streamer Update(Streamer streamer)
        {
            if(Get(streamer.Id) != null)
            {
                return _streamerAccess.UpdateStreamer(streamer);
            }
            return _streamerAccess.CreateStreamer(streamer);
        }

        public async Task<TwitchValidate?> ValidateToken(string token)
        {

            RestClient restClient = new RestClient("https://id.twitch.tv/");
            restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token);

            RestRequest restRequest = new RestRequest("oauth2/validate");

            var response = await restClient.ExecuteGetAsync(restRequest);

            return JsonSerializer.Deserialize<TwitchValidate>(response.Content);
        }

        public async Task<TwitchRefresh?> RefreshToken(string refreshToken)
        {
            string? clientSecret = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json", optional: false)
                                .Build().GetSection("ClientSecret").Value;

            RestClient restClient = new RestClient("https://id.twitch.tv/");

            RestRequest restRequest = new RestRequest("oauth2/token");
            restRequest.AddParameter("client_id", "8hmbxjfogmmj9e14y2ohn2vb0q8zv5");
            restRequest.AddParameter("client_secret", clientSecret);
            restRequest.AddParameter("grant_type", "refresh_token");
            restRequest.AddParameter("refresh_token", refreshToken);

            var response = await restClient.ExecutePostAsync<TwitchRefresh>(restRequest);

            return response.Data;
        }
    }
}
