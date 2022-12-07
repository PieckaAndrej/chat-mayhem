using Dapper;
using Data.ModelLayer;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
        public QuestionPack CreateQuestionPack(QuestionPack questionPack)
        {
            string sql = "INSERT INTO public.\"QuestionPack\"(\"author\", \"name\", \"tag\", \"category\", \"creationDate\") " +
                    "VALUES (@author, @name, @tag, @category, @creationDate) RETURNING id;";
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                questionPack.Id = connection.Execute(sql, new
                {
                    author = questionPack.Author,
                    name = questionPack.Name,
                    tag = questionPack.Tags,
                    category = questionPack.Category,
                    creationDate = questionPack.CreationDate
                });
                return questionPack;
            }
        }



        public QuestionPack GetQuestionPackById(int id)
        {
            string sql = "SELECT questionPack.id, questionPack.author, questionPack.name," +
                " questionPack.tag, questionPack.category, questionPack.\"creationDate\", " +
                "question.id, question.text " +
                "FROM public.\"QuestionPack\" questionPack " +
                "INNER JOIN public.\"Question\" question on question.\"questionPackId\" = questionPack.id " +
                "WHERE questionPack.id = @Id;";

            QuestionPack tempQuestionPack = null;
            using (var connection = new NpgsqlConnection(_connectionString))
            {

                var questionPack = connection.Query<QuestionPack, Question, QuestionPack>(sql, map: (qp, q) =>
                {
                    if (qp.Questions == null)
                    {
                        qp.Questions = new List<Question>();
                    }
                    if (tempQuestionPack != null)
                    {
                        qp = tempQuestionPack;
                    }
                    qp.Questions.Add(q);
                    tempQuestionPack = qp;

                    return qp;
                },
                new { Id = id },
                splitOn: "id"
                ).AsQueryable().FirstOrDefault();

                return questionPack;
            }
        }
        public QuestionPack UpdateQuestionPack(int id, QuestionPack questionPack)
        {
            string sql = "UPDATE public.\"QuestionPack\" SET" +
                "\"author\" = @author, \"name\"=@name, \"tag\"=@tag, \"category\"=@category" +
                "WHERE Id = @id RETURNING id;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                questionPack.Id = connection.Execute(sql, new
                {
                    author = questionPack.Author,
                    name = questionPack.Name,
                    tag = questionPack.Tags,
                    category = questionPack.Category,
                    creationDate = questionPack.CreationDate
                });
                return questionPack;
            }

        }

        public bool DeleteQuestionPack(int id)
        {
            string sql = "DELETE FROM public.\"QuestionPack\" WHERE Id = @id";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                int rowsAffected = connection.Execute(sql, new
                {
                    Id = id
                });

                if(rowsAffected != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<QuestionPack> GetAllQuestionPacks() 
        {
            string sql = "SELECT questionPack.id, questionPack.author, questionPack.name," +
                " questionPack.tag, questionPack.category, questionPack.\"creationDate\", " +
                "question.id, question.text " +
                "FROM public.\"QuestionPack\" questionPack " +
                "INNER JOIN public.\"Question\" question on question.\"questionPackId\" = questionPack.id;";

            Dictionary<int, QuestionPack> tempQuestionPack = new Dictionary<int, QuestionPack>();
            using (var connection = new NpgsqlConnection(_connectionString))
            {

                connection.Query<QuestionPack, Question, QuestionPack>(sql, map: (qp, q) =>
                {
                    QuestionPack questionPack;

                    if (!tempQuestionPack.TryGetValue(qp.Id, out questionPack))
                    {
                        tempQuestionPack.Add(qp.Id, questionPack = qp);
                    }

                    if (questionPack.Questions == null)
                    {
                        questionPack.Questions = new List<Question>();
                    }
                    questionPack.Questions.Add(q);

                    return questionPack;
                },
                splitOn: "id"
                ).AsQueryable();

                return tempQuestionPack.Values.AsList();
            }
        }
        public async Task<QuestionPack> GetAsync(int id, QuestionPack questionPack)
        {
            const string Sql = "SELECT * FROM QuestionPack WHERE Id = @id";
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return connection.QuerySingle(Sql, new { id });
            }     
        }

        public async Task<QuestionPack> InsertAsync(QuestionPack questionPack)
        {
            const string Sql = @"
                INSERT INTO QuestionPack ( id, author, name, tag, category, creationDate )
                VALUES ( @id, @author, @name, @tag, @category, @creationDate )
                SELECT @Id = Id, @xmin = xmin
                FROM QuestionPack WHERE Id = SCOPE_IDENTITY()";

            var @params = new DynamicParameters(questionPack)
                .Output(questionPack, q => q.xmin)
                .Output(questionPack, q => q.Id);

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(Sql, @params);
            }
            
            return questionPack;
        }
        public async Task<QuestionPack> UpdateAsync(QuestionPack questionPack)
        {
            const string Sql = @"
                UPDATE QuestionPack 
                SET
                id = @id,
                author = @author,
                name = @name,
                tag = @tag,
                category = @category,
                creationDate = @creationDate,
                WHERE Id = @Id
                AND xmin = @xmin";
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var rowCount = await connection.ExecuteAsync(Sql, questionPack);
                if (rowCount == 0)
                {
                    throw new Exception("Oh no, someone else edited this record!");
                }
            }
            return questionPack;
        }

    }
    
}
