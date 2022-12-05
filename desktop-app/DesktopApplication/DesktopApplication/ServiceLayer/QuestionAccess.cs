using DesktopApplication.ModelLayer;
using Newtonsoft.Json;
using PersonServiceClientDesktop.Servicelayer;
using RestSharp;
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
        }

        public async Task<Question?> InsertAnswers(Question question)
        {
            var request = new RestRequest("api/Question/answers/{id}");
            request.AddUrlSegment("id", question.id);
            request.AddJsonBody(question);
            var response = await _restClient.ExecutePutAsync<Question>(request);

            return response.Data;
        }
    }
}
