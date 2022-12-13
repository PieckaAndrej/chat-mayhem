using DesktopApplication.ModelLayer;
using RestSharp;
using RestSharp.Authenticators.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApplication.ServiceLayer
{
    public class QuestionPackAccess : IQuestionPackAccess
    {
        private readonly RestClient _restClient;

        public QuestionPackAccess()
        {
            _restClient = new RestClient("https://localhost:7200/");

            if (!String.IsNullOrEmpty(LoginAccess.token))
            {
                _restClient.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(LoginAccess.token, "Bearer");
            }
        }

        public async Task<QuestionPack?> InsertQuestionPack(QuestionPack questionPack)
        {
            var request = new RestRequest("api/QuestionPack");
            request.AddJsonBody(questionPack);
            
            if (!String.IsNullOrEmpty(LoginAccess.token))
            {
                var response = await _restClient.ExecutePostAsync<QuestionPack>(request);
                return response.Data;
            }
            else
            {
                return null;
            }
        }
    }
}
