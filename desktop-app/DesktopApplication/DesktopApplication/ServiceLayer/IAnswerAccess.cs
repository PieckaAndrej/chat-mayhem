using Data.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApplication.ServiceLayer
{
    public interface IAnswerAccess
    {
        Task<int> CreateAnswers(List<Answer> answers);
    }
}
