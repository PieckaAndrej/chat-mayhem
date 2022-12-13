using DesktopApplication.ModelLayer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace DesktopApplication.ServiceLayer
{
    public class QuestionAccess : IQuestionAccess
    {
        private readonly RestClient _restClient;

        public QuestionAccess()
        {

            _restClient = new RestClient("https://localhost:7200/");

            if (!String.IsNullOrEmpty(LoginAccess.token))
            {
                _restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(LoginAccess.token, "Bearer");
            }
        }

        public async Task<Question?> InsertAnswers(Question question)
        {
            var request = new RestRequest("api/Question/answers/{id}");
            request.AddUrlSegment("id", question.id);
            request.AddJsonBody(question);

            if(!String.IsNullOrEmpty(LoginAccess.token))
            {
                var response = await _restClient.ExecutePutAsync<Question>(request);
                return response.Data;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Question>> GetQuestions()
        {
            var request = new RestRequest("api/Question");

            if (!String.IsNullOrEmpty(LoginAccess.token))
            {
                var response = await _restClient.ExecuteGetAsync<List<Question>>(request);
                return response.Data;
            }
            else
            {
                return null;
            }  
        }

        public async Task<Question> GetQuestionById(int id)
        {
            var request = new RestRequest("api/Question/{id}");
            request.AddUrlSegment("id", id);

            if (!String.IsNullOrEmpty(LoginAccess.token))
            {
                var response = await _restClient.ExecuteGetAsync<Question>(request);
                return response.Data;
            }
            else
            {
                return null;
            }
        }
    }
}
