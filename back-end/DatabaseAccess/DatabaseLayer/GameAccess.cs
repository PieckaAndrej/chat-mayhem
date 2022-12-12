using Dapper;
using Data.ModelLayer;
using Npgsql;
using System.Data.SqlClient;

namespace Data.DatabaseLayer
{
    public class GameAccess : IGameAccess
    {
        private readonly string _connectionString;

        public GameAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Game CreateGame(Game game)
        {
            string sql = "INSERT INTO public.\"Game\"" +
                "( \"timeLimit\", owner, \"modelId\", \"questionPackId\") " +
                "VALUES (@timeLimit, @owner, @modelId, @questionPackId) RETURNING id;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                game.Id = connection.QuerySingle<int>(sql, new
                {
                    timeLimit = game.TimeLimit,
                    owner = game.Streamer.Id,
                    modelId = game.Mode.Id,
                    questionPackId = game.QuestionPack.Id
                });

                return game;
            }
        }

        public bool DeleteGame(int id)
        {
            string sql = "DELETE FROM public.\"Game\" WHERE Id = @id";

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

        public Game? GetGameById(int id)
        {
            string sql = "SELECT game.owner, game.\"timeLimit\", game.id, " +
                " streamer.\"accessToken\", streamer.id, streamer.\"refreshToken\", " +
                "mode.id, mode.description, mode.rules, " +
                "questionPack.id, questionPack.author, questionPack.\"name\", " +
                "questionPack.category, questionPack.\"creationDate\", " +
                "question.id, question.text, questionPack.tag " +
                "FROM public.\"Game\" game " +
                "INNER JOIN public.\"Streamer\" streamer on streamer.id = game.\"owner\" " +
                "INNER JOIN public.\"Mode\" mode on mode.id = game.\"modelId\" " +
                "INNER JOIN public.\"QuestionPack\" questionPack on questionPack.id = game.\"questionPackId\" " +
                "INNER JOIN public.\"Question\" question on question.\"questionPackId\" = questionPack.id " +
                "WHERE game.id = @Id;";


            QuestionPack questionPack = null;

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var game = connection.Query<Game, Streamer, GameMode, QuestionPack, Question, Game>(sql,map: ((g, s, m, qp, q) =>
                {
                    if (questionPack != null)
                    {
                        qp = questionPack;
                    }
                    if (qp.Questions == null)
                    {
                        qp.Questions = new List<Question>();
                    }
                    g.Streamer = s;
                    g.Mode = m;
                    qp.Questions.Add(q);
                    g.QuestionPack = qp;
                    questionPack = qp;
                    return g;
                }), 
                new { Id = id },
                splitOn: "accessToken, id, id, id"
                ).AsQueryable().FirstOrDefault();

                return game;
            }
        }

        public List<Game> GetGames()
        {
            throw new NotImplementedException();
        }

        public Game UpdateGame(int id, Game game)
        {
            string sql = "UPDATE public.\"Game\" SET " +
                "\"timeLimit\" = @timeLimit, owner = @owner, \"modelId\" = @modelId, \"questionPackId\" = @questionPackId " +
                "WHERE Id = @id;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Execute(sql, new
                {
                    timeLimit = game.TimeLimit,
                    owner = game.Streamer.Id,
                    modelId = game.Mode.Id,
                    questionPackId = game.QuestionPack.Id,
                    Id = id
                });

                return game;
            }
        }
    }
}