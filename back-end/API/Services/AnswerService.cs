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

        public List<Answer>? InsertAnswers(Question question)
        {
            var answers = _answerAccess.GetAnswersQuestionById(question.id);
            foreach (var answer in question.answers)
            {
                if (answers.Select(a => a.text.ToLower()).Contains(answer.text.ToLower()))
                {
                    var x = answers.Select(a => a.text.ToLower());
                    if (_answerAccess.UpdatePoints(answer, answers.Single(a => a.text.ToLower().Equals(answer.text.ToLower())).answerCount, question.id) != 1)
                    {
                        return null;
                    }
                }
                else
                {
                    if (_answerAccess.CreateAnswer(answer, question.id) != 1)
                    {
                        return null;
                    }
                }
                
            }
            return _answerAccess.GetAnswersQuestionById(question.id);
        }
    }
}
