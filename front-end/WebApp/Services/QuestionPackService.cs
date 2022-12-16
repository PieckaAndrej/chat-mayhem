using RestSharp;
using RestSharp.Authenticators.OAuth2;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApp.Models;

namespace WebApp.Services
{
    public class QuestionPackService
    {
        private readonly RestClient _client;

        public QuestionPackService()
        {
            _client = new RestClient("https://localhost:7200/");
        }

        public async Task<QuestionPack?> GetQuestionPackById(int id)
        {
            RestRequest restRequest = new RestRequest("api/QuestionPack/{id}");
            restRequest.AddUrlSegment("id",id);
            await LoginAccess.GetToken("admin", "admin");

            if (!String.IsNullOrEmpty(LoginAccess.token))
            {
                _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(LoginAccess.token, "Bearer");
                var response = await _client.ExecuteGetAsync<QuestionPack>(restRequest);
                return response.Data;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<QuestionPack>?> GetQuestionPacks()
        {
            RestRequest restRequest = new RestRequest("api/QuestionPack");
            await LoginAccess.GetToken("admin", "admin");

            if (!String.IsNullOrEmpty(LoginAccess.token))
            {
                _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(LoginAccess.token, "Bearer");
                var response = await _client.ExecuteGetAsync<List<QuestionPack>>(restRequest);
                return response.Data;
            }
            else
            {
                return null;
            }
        }

        public async Task<QuestionPack?> InsertQuestionPack(QuestionPack questionPack)
        {
            RestRequest restRequest = new RestRequest("api/QuestionPack");
            restRequest.AddJsonBody(questionPack);
            await LoginAccess.GetToken("admin", "admin");
            var x = JsonSerializer.Serialize(questionPack);

            if (!String.IsNullOrEmpty(LoginAccess.token))
            {
                _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(LoginAccess.token, "Bearer");
                var response = await _client.ExecutePostAsync<QuestionPack>(restRequest);
                return response.Data;
            }
            else
            {
                return null;
            }
        }

        public async Task<QuestionPack?> UpdateQuestionPack(QuestionPack questionPack)
        {
            RestRequest restRequest = new RestRequest("api/QuestionPack/{id}");
            restRequest.AddUrlSegment("id", questionPack.Id);
            var x = JsonSerializer.Serialize(questionPack);
            restRequest.AddJsonBody(questionPack);
            await LoginAccess.GetToken("admin", "admin");

            if (!String.IsNullOrEmpty(LoginAccess.token))
            {
                _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(LoginAccess.token, "Bearer");
                var response = await _client.ExecutePutAsync<QuestionPack>(restRequest);
                return response.Data;
            }
            else
            {
                return null;
            }
        }
    }
}
