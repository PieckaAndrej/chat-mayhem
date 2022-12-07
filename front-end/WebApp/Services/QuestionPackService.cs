using RestSharp;
using WebApp.Models;

namespace WebApp.Services
{
    public class QuestionPackService
    {
        public async Task<List<QuestionPack>?> GetQuestionPacks()
        {
            RestClient restClient = new RestClient("https://localhost:7200/");
            RestRequest restRequest = new RestRequest("api/QuestionPack");

            var response = await restClient.ExecuteGetAsync<List<QuestionPack>>(restRequest);

            return response.Data;
        }

        public async Task<QuestionPack?> InsertQuestionPack(QuestionPack questionPack)
        {
            RestClient restClient = new RestClient("https://localhost:7200/");
            RestRequest restRequest = new RestRequest("api/QuestionPack");

            var response = await restClient.ExecutePostAsync<QuestionPack>(restRequest);

            return response.Data;
        }
    }
}
