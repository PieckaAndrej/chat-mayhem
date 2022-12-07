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

        readonly IQuestionPackAccess _answerPackAccess;

        public AnswerControl()
        {
            _answerAccess = new QuestionAccess();

            _answerPackAccess = new QuestionPackAccess();
        }

        public async Task<Question?> InsertAnswers(Question question)
        {
            Question returnQuestion = await _answerAccess.InsertAnswers(question);
            return returnQuestion;
        }

        public async Task<List<Question>> GetQuestions()
        {
            return await _answerAccess.GetQuestions();
        }

        public async Task<Question> GetQuestionById(int id)
        {
            return await _answerAccess.GetQuestionById(id);
        }

        public async Task<QuestionPack?> InsertQuestionPack(QuestionPack questionPack)
        {
            return await _answerPackAccess.InsertQuestionPack(questionPack);
        }
    }
}
