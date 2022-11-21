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

            RestRequest restRequest = new RestRequest("oauth2/validate");
            restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token);

            var response = restClient.ExecutePost<TwitchValidate>(restRequest);

            return response.Data;
        }
    }
}
