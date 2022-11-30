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
            Console.WriteLine(_connectionString);
        }

        public List<Answer> CreateAnswer(List<Answer> answers)
        {
            List<Answer> returnAnswers = new List<Answer>();

            string sql = "INSERT INTO public.\"Answer\"" +
                "( \"questionId\", \"answerText\", \"answerCount\") " +
                "VALUES (@questionId, @answerText, @answerCount);";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                foreach (var answer in answers)
                {
                    returnAnswers.Add(answer);

                    connection.Execute(sql, new
                    {
                        questionId = answer.questionId,
                        answerText = answer.text,
                        answerCount = answer.answerCount
                    });
                }

                return returnAnswers;
            }
        }
    }
}
