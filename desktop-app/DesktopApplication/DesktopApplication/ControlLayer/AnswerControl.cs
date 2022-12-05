using DesktopApplication.ModelLayer;
using DesktopApplication.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApplication.ControlLayer
{
    public class AnswerControl
    {
        readonly IQuestionAccess _answerAccess;

        public AnswerControl()
        {
            _answerAccess = new QuestionAccess();
        }

        public async Task<Question?> InsertAnswers(Question question)
        {
            Question returnQuestion = await _answerAccess.InsertAnswers(question);
            return returnQuestion;
        }

        public async Task<List<Question>> GetQuestions()
        {
            return await _answerAccess.GetQuestions().ConfigureAwait(false);
        }
    }
}
