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

        public Answer CreateAnswer(Answer answer)
        {
            return _answerAccess.CreateAnswer(answer);
        }
    }
}
