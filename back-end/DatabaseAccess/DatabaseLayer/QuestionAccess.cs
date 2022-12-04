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
    public class QuestionAccess : IQuestionAccess
    {
        private readonly string _connectionString;

        public QuestionAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Question? GetQuestionById(int? id)
        {
            string sql = "SELECT question.\"questionPackId\", question.\"text\", question.id, " +
                    "answer.\"answerCount\", answer.\"answerText\" AS text " +
                    "FROM public.\"Question\" question " +
                    "LEFT JOIN public.\"Answer\" answer ON question.id = answer.\"questionId\" " +
                    "WHERE question.id = @Id";

            Question tempQuestion = null;

            using(var conncetion = new NpgsqlConnection(_connectionString))
            {
                var question = conncetion.Query<Question, Answer, Question>(sql, map: (q, a) =>
                { 
                    if (q.answers == null)
                    {
                        q.answers = new List<Answer>();
                    }
                    if (tempQuestion != null)
                    {
                        q = tempQuestion;
                    }
                    q.answers.Add(a);
                    tempQuestion = q;
                    return q;
                },
                new { Id = id },
                splitOn: "answerCount"
                ).AsQueryable().FirstOrDefault();
                return question;
            }
        }

        //public int UpdateQuestion(Question question)
        //{
        //    string sql = "UPDATE public.\"Question\" SET " +
        //        "\"text\" = @text, \"questionPackId\" = @questionPackId " +
        //        "WHERE Id = @id;";

        //    using (var connection = new NpgsqlConnection(_connectionString))
        //    {
        //        var rowsChnaged = connection.Execute(sql, new
        //        {
                    
        //            Id = question.id
        //        });

        //        return rowsChnaged;
        //    }
        //}


        //public Question InsertQuestion(Question question)
        //{
        //    string sql = "INSERT INTO public.\"Question\"" +
        //        "\"questionPackId\", \"text\" " +
        //        "VALUES (@questionPackId, @text) RETURNING id;";

        //    using (var connection = new NpgsqlConnection(_connectionString))
        //    {
        //        question.id = connection.QuerySingle<int>(sql, new
        //        {
        //            questionPackId = question.QuestionPack.Id,
        //            text = question.text
        //        });

        //        return question;
        //    }
        //}
    }
}
