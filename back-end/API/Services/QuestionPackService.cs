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

        public QuestionPack GetQuestionPackById(int id)
        {
            return _questionPackAccess.GetQuestionPackById(id);
        }

        public QuestionPack UpdateQuestionPack(int id, QuestionPack questionPack)
        {
            return _questionPackAccess.UpdateQuestionPack(id, questionPack);
        }

        public QuestionPack CreateQuestionPack(QuestionPack questionPack)
        {
            return _questionPackAccess.CreateQuestionPack(questionPack);
        }

        public bool DeleteQuestionPack(int id)
        {
            return _questionPackAccess.DeleteQuestionPack(id);
        }

        public List<QuestionPack> GetAllQuestionPacks()
        {
            return _questionPackAccess.GetAllQuestionPacks();
        }
    }
}