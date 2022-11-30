using Data.DatabaseLayer;
using Data.ModelLayer;

namespace API.Services
{
    public class AnswerService
    {
        private IAnswerAccess _answerAccess;

        public AnswerService(IAnswerAccess answerAccess)
        {
            _answerAccess = answerAccess;
        }

        public List<Answer> CreateAnswer(List<Answer> answers)
        {
            return _answerAccess.CreateAnswer(answers);
        }
    }
}
