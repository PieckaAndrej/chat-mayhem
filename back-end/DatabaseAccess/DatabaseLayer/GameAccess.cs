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
                "VALUES (@timeLimit, @owner, @modelId, @questionPackId);";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                game.Id = connection.QuerySingle<int>(sql, new
                {
                    timeLimit = game.TimeLimit,
                    owner = game.Streamer.Id,
                    modelId = game.Mode.Id,
                });

                return game;
            }
        }

        public int DeleteGame(int id)
        {
            throw new NotImplementedException();
        }

        public Game GetGameById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Game> GetGames()
        {
            throw new NotImplementedException();
        }

        public int UpdateGame(int id, Game game)
        {
            throw new NotImplementedException();
        }
    }
}