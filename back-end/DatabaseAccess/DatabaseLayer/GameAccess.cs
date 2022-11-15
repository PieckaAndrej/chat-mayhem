using Dapper;
using Data.ModelLayer;
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
            string sql = "INSERT INTO public.'Game'" +
                "('time_limit', 'player_number', owner, 'model_id') " +
                "OUTPUT INSERTED.id " +
                "VALUES (@timeLimit, @playerNumber, @owner, @modelId);";

            using (var connection = new SqlConnection(_connectionString))
            {
                game.Id = connection.QuerySingle<int>(sql, new
                {
                    timeLimit = game.TimeLimit,
                    playerNumber = game.PlayerNumber,
                    steamer = game.Streamer,
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