using Dapper;
using Data.ModelLayer;
using Npgsql;
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
            string sql = "SELECT id, author, name, tag, category, \"creationDate\" " +
                "FROM public.\"QuestionPack\" WHERE id = Id;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var questionPack = connection.QuerySingle<QuestionPack>(sql, new { Id = id });

                return questionPack;
            }
        }
    }
}
