using Data.DatabaseLayer;
using Data.ModelLayer;

namespace API.Services
{
    public class QuestionService
    {
        private IQuestionAccess _questionAccess;

        public QuestionService(IQuestionAccess questionAccess)
        {
            this._questionAccess = questionAccess;
        }

        public Question? GetQuestionById(int? id)
        {
            return _questionAccess.GetQuestionById(id);
        }
    }
}
