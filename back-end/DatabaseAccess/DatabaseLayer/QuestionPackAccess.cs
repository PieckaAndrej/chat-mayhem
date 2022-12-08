using Dapper;
using Data.ModelLayer;
using Npgsql;
using Npgsql.Internal.TypeHandlers.NumericHandlers;
using Npgsql.PostgresTypes;
using System;
using System.Collections.Generic;
using System.Data;
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
                "\"author\" = @Author, \"name\"=@Name, \"tag\"=@Tag, \"category\"=@Category, " +
                "\"creationDate\" = @CreationDate " +
                "WHERE id = @Id;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                questionPack.Id = connection.Execute(sql, new
                {
                    Id = id,    
                    Author = questionPack.Author,
                    Name = questionPack.Name,
                    Tag = questionPack.Tags,
                    Category = questionPack.Category,
                    CreationDate = questionPack.CreationDate
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
            string sql = "SELECT questionPack.id, questionPack.author, questionPack.name, " +
                "questionPack.category, questionPack.\"creationDate\", questionPack.\"xmin\", questionPack.tag, " +
                "question.id, question.text " +
                "FROM public.\"QuestionPack\" questionPack " +
                "INNER JOIN public.\"Question\" question on question.\"questionPackId\" = questionPack.id;";

            Dictionary<int, QuestionPack> tempQuestionPack = new Dictionary<int, QuestionPack>();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                SqlMapper.AddTypeHandler(new GenericArrayHandler<int>());
                connection.Query<QuestionPack, string[], Question, QuestionPack>(sql, map: (qp, t, q) =>
                {
                    QuestionPack questionPack;

                    qp.Tags = t;

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
                splitOn: "tag, id"
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
                INSERT INTO public.""QuestionPack"" (author, name, tag, category, ""creationDate"" )
                VALUES (@author, @name, @tags, @category, @creationDate )
                RETURNING id, xmin";

            var @params = new DynamicParameters(questionPack)
                .Output(questionPack, q => q.Id);
            @params.Add("xmin", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(Sql, @params);
                questionPack.xmin = Convert.ToInt32(@params.Get<UInt32>("xmin"));
            }
            
            return questionPack;
        }

        public async Task<QuestionPack> UpdateAsync(QuestionPack questionPack)
        {
            QuestionPack retQuestionPack = new QuestionPack();
            const string Sql = @"UPDATE public.""QuestionPack""
	                SET author=@Author, name=@Name, category=@Category, ""creationDate""=@CreationDate, tag=@Tags 
	                WHERE id = @Id AND xmin = @xmin
                    RETURNING xmin, id, author, name, category, ""creationDate"", tag";
            
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = await connection.BeginTransactionAsync())
                {
                    try
                    {
                        SqlMapper.AddTypeHandler(new UintHandler());
                        retQuestionPack = (await connection.QueryAsync<QuestionPack, string[], QuestionPack>(Sql, map: (qp, t) =>
                        {
                            qp.Tags = t;
                            return qp;
                        },
                        splitOn: "tag",
                        param: new
                        {
                            Author = questionPack.Author,
                            Name = questionPack.Name,
                            Category = questionPack.Category,
                            CreationDate = questionPack.CreationDate,
                            Tags = questionPack.Tags,
                            Id = questionPack.Id,
                            xmin = questionPack.xmin
                        } )).AsQueryable().First();
                        await transaction.CommitAsync();
                    }
                    catch
                    {
                        await transaction.RollbackAsync();
                    }
                }
            }
            return retQuestionPack;
        }

    }
    
}
