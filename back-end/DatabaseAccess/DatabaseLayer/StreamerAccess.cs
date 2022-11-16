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
    public class StreamerAccess : IStreamerAccess
    {
        private readonly string _connectionString;

        public StreamerAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Streamer GetStreamerById(int id)
        {
            string sql = "SELECT * FROM Streamer WHERE id = 1;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var streamer = connection.QuerySingle<Streamer>(sql);

                return streamer;
            }
        }
    }
}
