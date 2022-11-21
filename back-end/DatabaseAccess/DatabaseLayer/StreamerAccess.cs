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
            string sql = "SELECT id, key as \"OAuth\" FROM public.\"Streamer\" WHERE id = @Id;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var streamer = connection.QuerySingle<Streamer>(sql, new { Id = id });
                Console.WriteLine(streamer.OAuth);
                return streamer;
            }
        }
    }
}
