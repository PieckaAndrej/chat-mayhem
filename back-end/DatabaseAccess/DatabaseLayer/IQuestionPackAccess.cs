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
        QuestionPack GetQuestionPackById(int id);
        bool DeleteQuestionPack(int id);
        List<QuestionPack> GetAllQuestionPacks();
        Task<QuestionPack> InsertAsync(QuestionPack questionPack);
        Task<QuestionPack> UpdateAsync(QuestionPack questionPack);
    }
}
