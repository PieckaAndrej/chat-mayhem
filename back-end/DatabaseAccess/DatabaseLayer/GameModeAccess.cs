using Dapper;
using Data.ModelLayer;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DatabaseLayer
{
    public class GameModeAccess : IGameModeAccess
    {
        private readonly string _connectionString;

        public GameModeAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public GameMode GetGameModeById(int id)
        {
            string sql = "SELECT id, rules, description FROM public.\"Mode\" WHERE id = @Id;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var gameMode = connection.QuerySingle<GameMode>(sql, new { Id = id });

                return gameMode;
            }
        }
    }
}
