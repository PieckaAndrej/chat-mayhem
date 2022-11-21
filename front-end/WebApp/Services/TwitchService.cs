using RestSharp;
using RestSharp.Authenticators.OAuth2;
using System.Security.Policy;
using WebApp.Models;

namespace WebApp.Services
{
    public class TwitchService
    {
        public TwitchValidate? ValidateToken(string token)
        {
            RestClient restClient = new RestClient("https://id.twitch.tv/");
            restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token);

            RestRequest restRequest = new RestRequest("oauth2/validate");

            var response = restClient.ExecuteGet<TwitchValidate>(restRequest);

            Console.WriteLine(response.ResponseStatus);

            return response.Data;
        }

        public TwitchToken? GetTwitchToken(string code)
        {
            RestClient restClient = new RestClient("https://id.twitch.tv/");

            RestRequest restRequest = new RestRequest("oauth2/token");
            restRequest.AddParameter("code", code);
            restRequest.AddParameter("grant_type", "authorization_code");
            restRequest.AddParameter("client_id", "8hmbxjfogmmj9e14y2ohn2vb0q8zv5");
            restRequest.AddParameter("client_secret", "");
            restRequest.AddParameter("redirect_uri", "https://localhost:7026/twitch");

            var response = restClient.ExecutePost<TwitchToken>(restRequest);

            Console.WriteLine(response.ResponseStatus);

            return response.Data;
        }
    }
}
