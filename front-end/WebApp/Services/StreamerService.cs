using RestSharp;
using RestSharp.Authenticators.OAuth2;
using System.Text.Json;
using WebApp.DTOs;
using WebApp.Models;

namespace WebApp.Services
{
    public class StreamerService
    {
        private readonly RestClient _client;

        public StreamerService()
        {
            _client = new RestClient("https://localhost:7200/");
        }

        public async Task<StreamerDto?> CreateStreamer(StreamerDto streamer)
        {
            RestRequest restRequest = new RestRequest("api/streamer").AddJsonBody(streamer);
            await LoginAccess.GetToken("admin", "admin");

            if (!String.IsNullOrEmpty(LoginAccess.token))
            {
                _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(LoginAccess.token, "Bearer");
                var response = await _client.ExecutePostAsync<StreamerDto>(restRequest);
                return response.Data;
            }
            else
            {
                return null;
            }
        }

        public static async Task<string?> RefreshToken(string streamerId, string accessToken)
        {
            RestClient restClient = new RestClient("https://localhost:7200/");
            RestRequest restRequest = new RestRequest("api/streamer/token");
            restRequest.AddQueryParameter("streamerId", streamerId);
            restRequest.AddQueryParameter("token", accessToken);
            await LoginAccess.GetToken("admin", "admin");

            if (!String.IsNullOrEmpty(LoginAccess.token))
            {
                restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(LoginAccess.token, "Bearer");
                var response = await restClient.ExecutePostAsync<string>(restRequest);
                return response.Data;
            }
            else
            {
                return null;
            }
        }
    }
}
