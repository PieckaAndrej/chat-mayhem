using Data.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DatabaseLayer
{
    public class QuestionPackAccess : IQuestionPackAccess
    {
        private readonly string _connectionString;

        public QuestionPackAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public QuestionPack GetQuestionPackById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
