using Data.DatabaseLayer;
using Data.ModelLayer;

namespace API.Services
{
    public class QuestionPackService
    {
        private IQuestionPackAccess _questionPackAccess;

        private QuestionService _questionService;

        public QuestionPackService(IQuestionPackAccess questionPackAccess)
        {
            _questionPackAccess = questionPackAccess;
            _questionService = ServiceInjector.QuestionService;
        }

        public QuestionPack GetQuestionPackById(int id)
        {
            return _questionPackAccess.GetQuestionPackById(id);
        }

        public bool DeleteQuestionPack(int id)
        {
            return _questionPackAccess.DeleteQuestionPack(id);
        }

        public List<QuestionPack> GetAllQuestionPacks()
        {
            return _questionPackAccess.GetAllQuestionPacks();
        }

        public async Task<QuestionPack> InsertAsync(QuestionPack questionPack)
        {
            return await _questionPackAccess.InsertAsync(questionPack);
        }

        public async Task<QuestionPack> UpdateAsync(QuestionPack questionPack)
        {
            return await _questionPackAccess.UpdateAsync(questionPack);
        }
    }
}