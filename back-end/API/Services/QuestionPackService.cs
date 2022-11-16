using Data.DatabaseLayer;
using Data.ModelLayer;

namespace API.Services
{
    public class QuestionPackService
    {
        private IQuestionPackAccess _questionPackAccess;

        public QuestionPackService(IQuestionPackAccess questionPackAccess)
        {
            _questionPackAccess = questionPackAccess;
        }

        public QuestionPack Get(int id)
        {
            return _questionPackAccess.GetQuestionPackById(id);
        }
    }
}