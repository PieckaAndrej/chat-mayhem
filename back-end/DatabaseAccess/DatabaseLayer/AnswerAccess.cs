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
    public class AnswerAccess : IAnswerAccess
    {
        private readonly string _connectionString;

        public AnswerAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Answer> GetAnswersQuestionById(int? questionId)
        {
            string sql = "SELECT \"answerCount\", \"answerText\" AS text  " +
                "FROM public.\"Answer\" " +
                "WHERE \"questionId\" = @questionId";

            using(var connection = new NpgsqlConnection(_connectionString))
            {
                var answerList = connection.Query<Answer>(sql, new
                {
                    questionId = questionId
                }).AsQueryable().ToList();

                return answerList;
            }
        }

        public int CreateAnswer(Answer answer, int? questionId)
        {
            string sql = "INSERT INTO public.\"Answer\" " +
                "(\"questionId\", \"answerText\", \"answerCount\") " +
                "VALUES (@questionId, @answerText, @answerCount);";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var rowsAffected = connection.Execute(sql, new
                {
                    questionId = questionId,
                    answerText = answer.text.ToLower(),
                    answerCount = answer.answerCount
                });

                return rowsAffected;
            }
        }

        public int UpdatePoints(Answer answer, int oldPoints, int? questionId)
        {
            string sql = "UPDATE public.\"Answer\"" +
                "SET \"answerCount\" = @answerCount " +
                "WHERE \"answerText\" = @answerText AND \"questionId\" = @questionId"; 

            using(var connection = new NpgsqlConnection(_connectionString))
            {
                var rowsAffected = connection.Execute(sql, new
                {
                    answerCount = answer.answerCount + oldPoints,
                    answerText = answer.text.ToLower(),
                    questionId = questionId
                });
                return rowsAffected;
            }
        }
    }
}
