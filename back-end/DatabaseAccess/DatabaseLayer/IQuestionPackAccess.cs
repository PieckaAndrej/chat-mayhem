using Data.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DatabaseLayer
{
    public interface IQuestionPackAccess
    {
        QuestionPack CreateQuestionPack(QuestionPack questionPack);
        QuestionPack GetQuestionPackById(int id);
        QuestionPack UpdateQuestionPack(int id, QuestionPack questionPack);
        bool DeleteQuestionPack(int id);
        List<QuestionPack> GetAllQuestionPacks();
        Task<QuestionPack> InsertAsync(QuestionPack questionPack);
        Task<QuestionPack> UpdateAsync(QuestionPack questionPack);
    }
}
