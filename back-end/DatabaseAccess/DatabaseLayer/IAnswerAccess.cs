using Data.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DatabaseLayer
{
    public interface IAnswerAccess
    {
        int CreateAnswer(Answer answer, int? questionId);
        int UpdatePoints(Answer answer, int oldPoints, int? questionId);
        List<Answer> GetQuestionsAnswerById(int? questionId);
    }
}
