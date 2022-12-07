using DesktopApplication.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApplication.ServiceLayer
{
    public interface IQuestionPackAccess
    {
        Task<QuestionPack?> InsertQuestionPack(QuestionPack questionPack);
    }
}
