using Dapper;
using Data.ModelLayer;
using Data.DatabaseLayer;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DatabaseLayer
{
    internal class QuestionAccess : IQuestionAccess
    {
        private readonly string _connectionString;

        public QuestionAccess(string connectionString)
        {
            _connectionString = connectionString;
        }
        public Question InsertQuestion(Question question)
        {
            string sql = "INSERT INTO public.\"Question\"" +
                "(\"questionPackId\", \"text\") " +
                "VALUES (@questionPackId, @text) RETURNING id;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                question.id = connection.QuerySingle<int>(sql, new
                {
                    questionPackId = question.QuestionPack.Id,
                    text = question.text
                });

                return question;
            }
        }
    }
}
