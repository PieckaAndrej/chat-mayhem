using Data.ModelLayer;
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
        readonly IAnswerAccess _answerAccess;

        public AnswerControl()
        {
            _answerAccess = new AnswerAccess();
        }

        public async Task<int> CreateAnswers(List<Answer> answers)
        {
            int answersInserted = await _answerAccess.CreateAnswers(answers);
            return answersInserted;
        }
    }
}
