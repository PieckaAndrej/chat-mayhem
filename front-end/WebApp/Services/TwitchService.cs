using RestSharp;
using RestSharp.Authenticators.OAuth2;
using System.Security.Policy;
using System.Text.Json;
using WebApp.Models;

namespace WebApp.Services
{
    public class TwitchService
    {
        public async Task<TwitchValidate?> ValidateToken(string token)
        {
            RestClient restClient = new RestClient("https://id.twitch.tv/");
            restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token);

            RestRequest restRequest = new RestRequest("oauth2/validate");

            var response = await restClient.ExecuteGetAsync(restRequest);

            return JsonSerializer.Deserialize<TwitchValidate>(response.Content);
        }

        public async Task<TwitchToken?> GetTwitchToken(string code)
        {
            RestClient restClient = new RestClient("https://id.twitch.tv/");

            string clientSecret = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json", optional: false)
                                .Build().GetSection("ClientSecret").Value;

            Console.WriteLine(clientSecret);

            RestRequest restRequest = new RestRequest("oauth2/token");
            restRequest.AddParameter("code", code);
            restRequest.AddParameter("grant_type", "authorization_code");
            restRequest.AddParameter("client_id", "8hmbxjfogmmj9e14y2ohn2vb0q8zv5");
            restRequest.AddParameter("client_secret", clientSecret);
            restRequest.AddParameter("redirect_uri", "https://localhost:7026/twitch");


            var response = await restClient.ExecutePostAsync(restRequest);

            return JsonSerializer.Deserialize<TwitchToken>(response.Content);
        }
    }
}
