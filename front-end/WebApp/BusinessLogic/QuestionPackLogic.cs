using WebApp.Models;
using WebApp.Services;

namespace WebApp.BusinessLogic
{
    public class QuestionPackLogic
    {
        private readonly QuestionPackService _questionPackService;

        public QuestionPackLogic()
        {
            _questionPackService = new QuestionPackService();
        }

        public async Task<List<QuestionPack>?> GetAllQuestionPacks()
        {
            return await _questionPackService.GetQuestionPacks();
        }
    }
}
