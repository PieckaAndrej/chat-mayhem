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
            string sql = "SELECT questionPack.id, questionPack.author, questionPack.name," +
                " questionPack.tag, questionPack.category, questionPack.\"creationDate\", " +
                "question.id, question.text " +
                "FROM public.\"QuestionPack\" questionPack " +
                "INNER JOIN public.\"Question\" question on question.\"questionPackId\" = questionPack.id " +
                "WHERE questionPack.id = @Id;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {

                var questionPack = connection.Query<QuestionPack, Question, QuestionPack>(sql, map: (qp, q) =>
                {
                    qp.Questions = new List<Question>() { q };

                    return qp;
                },
                new { Id = id },
                splitOn: "id"
                ).AsQueryable().FirstOrDefault();

                return questionPack;
            }
        }
    }
}
