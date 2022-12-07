using DesktopApplication.ModelLayer;
using RestSharp;
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
        }

        public async Task<QuestionPack?> InsertQuestionPack(QuestionPack questionPack)
        {
            var request = new RestRequest("api/QuestionPack");
            request.AddJsonBody(questionPack);
            var response = await _restClient.ExecutePostAsync<QuestionPack>(request);

            return response.Data;
        }
    }
}
