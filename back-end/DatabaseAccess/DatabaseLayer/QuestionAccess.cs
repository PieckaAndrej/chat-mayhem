using Dapper;
using Data.ModelLayer;
using Data.DatabaseLayer;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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

            using(var connection = new NpgsqlConnection(_connectionString))
            {
                var question = connection.Query<Question, Answer, Question>(sql, map: (q, a) =>
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

        public List<Question>? GetQuestions()
        {
            string sql = "SELECT * FROM public.\"Question\" ORDER BY id ASC";

            List<Question>? questions = new List<Question>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                questions = connection.Query<Question>(sql).ToList();

                return questions;
            }
        }

        public List<Question>? GetQuestionsByQuestionPackId(int questionPackId)
        {
            string sql = "SELECT id, \"questionPackId\", text FROM public.\"Question\" " +
                "WHERE \"questionPackId\" = @questionPackId";

            List<Question>? questions = new List<Question>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                questions = connection.Query<Question>(sql, new {questionPackId = questionPackId}).ToList();

                return questions;
            }
        }

        //public Question? UpdateQuestion(Question question, int questionPackId)
        //{
        //    string sql = "UPDATE public.\"Question\" SET " +
        //        "\"text\" = @Text, \"questionPackId\" = @QuestionPackId " +
        //        "WHERE id = @Id;";

        //    using (var connection = new NpgsqlConnection(_connectionString))
        //    {
        //        var rowsChnaged = connection.Execute(sql, new
        //        {

        //            Id = question.id,
        //            Text = question.text,
        //            QuestionPackId = questionPackId
        //        });

        //        if (rowsChnaged == 0)
        //        {
        //            question = null;
        //        }

        //        return question;
        //    }
        //}

        public List<Question>? UpdateQuestion(List<Question> questions, int questionPackId)
        {
            string sql = "UPDATE public.\"Question\" SET " +
                "\"text\" = @text, \"questionPackId\" = @questionPackId " +
                "WHERE Id = @id;";
            using (var transaction = new TransactionScope(scopeOption: TransactionScopeOption.Required, asyncFlowOption: TransactionScopeAsyncFlowOption.Enabled, transactionOptions: new TransactionOptions()))
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    int rowsChanged = 0;
                    foreach (var question in questions)
                    {
                        rowsChanged += connection.Execute(sql, param: new
                        {
                            Id = question.id,
                            text = question.text,
                            questionPackId = questionPackId
                        });
                    }

                    if (rowsChanged == 0)
                    {
                        questions = null;
                    }

                    transaction.Complete();
                    return questions;
                }
            }
            
        }

        public Question InsertQuestion(Question question, int questionPackId)
        {
            string sql = "INSERT INTO public.\"Question\" " +
                "(\"questionPackId\", \"text\") " +
                "VALUES (@questionPackId, @text) RETURNING id;";

            using (var transaction = new TransactionScope(scopeOption: TransactionScopeOption.Required, asyncFlowOption: TransactionScopeAsyncFlowOption.Enabled, transactionOptions: new TransactionOptions()))
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    question.id = connection.QuerySingle<int>(sql, param: new
                    {
                        QuestionPackId = questionPackId,
                        Text = question.text
                    });

                    AnswerAccess answerAccess = new AnswerAccess(_connectionString);
                    answerAccess.InsertAnswers(question.answers, question.id);

                    transaction.Complete();
                    return question;
                }
            }
        }

        public List<Question> InsertQuestion(List<Question> questions, int questionPackId)
        {
            string sql = "INSERT INTO public.\"Question\" " +
                "(\"questionPackId\", \"text\") " +
                "VALUES (@QuestionPackId, @text) RETURNING id;";

            using (var transaction = new TransactionScope(scopeOption: TransactionScopeOption.Required, asyncFlowOption: TransactionScopeAsyncFlowOption.Enabled, transactionOptions: new TransactionOptions()))
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    foreach (var question in questions)
                    {
                        question.id = connection.QuerySingle<int>(sql, param: new
                        {
                            QuestionPackId = questionPackId,
                            Text = question.text
                        });
                    }

                    transaction.Complete();
                    return questions;
                }
            }
        }

        public void DeleteQuestion(List<Question> questions)
        {
            string sql = "DELETE FROM public.\"Question\" " +
                "WHERE id = @Id";
            using (var transaction = new TransactionScope(scopeOption: TransactionScopeOption.Required, asyncFlowOption: TransactionScopeAsyncFlowOption.Enabled, transactionOptions: new TransactionOptions()))
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Execute(sql, questions);
                }

                transaction.Complete();
            }
        }
    }
}
