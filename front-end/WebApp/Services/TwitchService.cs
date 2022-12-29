using RestSharp;
using RestSharp.Authenticators.OAuth2;
using WebApp.Models;

namespace WebApp.Services
{
    public class TwitchService
    {
        private static string _twitchUrl = "https://id.twitch.tv/";
        public static async Task<TwitchValidate?> ValidateToken(string token)
        {
            RestClient restClient = new RestClient(_twitchUrl);
            restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token);

            RestRequest restRequest = new RestRequest("oauth2/validate");

            var response = await restClient.ExecuteGetAsync<TwitchValidate>(restRequest);

            return response.Data;
        }

        public static async Task<TwitchToken?> GetTwitchToken(string code)
        {
            RestClient restClient = new RestClient(_twitchUrl);

            string clientSecret = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json", optional: false)
                                .Build().GetSection("ClientSecret").Value;

            RestRequest restRequest = new RestRequest("oauth2/token");
            restRequest.AddParameter("code", code);
            restRequest.AddParameter("grant_type", "authorization_code");
            restRequest.AddParameter("client_id", "8hmbxjfogmmj9e14y2ohn2vb0q8zv5");
            restRequest.AddParameter("client_secret", clientSecret);
            restRequest.AddParameter("redirect_uri", "https://chatmayhem.tictactoe.dk/twitch");

            var response = await restClient.ExecutePostAsync<TwitchToken>(restRequest);

            return response.Data;
        }

        public static async Task<TwitchUsers?> GetTwitchUsers(string token)
        {
            RestClient restClient = new RestClient("https://api.twitch.tv/");
            restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(
                token, "Bearer");

            RestRequest restRequest = new RestRequest("helix/users");
            restRequest.AddHeader("Client-Id", "8hmbxjfogmmj9e14y2ohn2vb0q8zv5");

            var response = await restClient.ExecuteGetAsync<TwitchUsers>(restRequest);

            return response.Data;
        }
    }
}
