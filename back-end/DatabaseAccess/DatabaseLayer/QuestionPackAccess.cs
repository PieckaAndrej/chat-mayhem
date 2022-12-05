﻿using Dapper;
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
        public QuestionPack CreateQuestionPack(QuestionPack questionPack)
        {
            string sql = "INSERT INTO public.\"QuestionPack\"( id, \"author\", \"name\", \"tag\", \"category\", \"creationDate\") " +
                    "VALUES (@id, @author, @name, @tag, @category, @creationDate) RETURNING id;";
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Execute(sql, new
                {
                    id = questionPack.Id,
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
                connection.Execute(sql, new
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

    }
}
