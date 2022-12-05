using Data.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DatabaseLayer
{
    public interface IQuestionAccess
    {
        //Question InsertQuestion(Question question);
        Question? GetQuestionById(int? id);
        //int UpdateQuestion(Question question);

        List<Question>? GetQuestions();
    }
}
