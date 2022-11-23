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

        public Streamer CreateStreamer(Streamer streamer)
        {
            string sql = "INSERT INTO public.\"Streamer\"( \"accessToken\", id, \"refreshToken\") " +
                "VALUES (@accessToken, @id, @refreshToken) RETURNING id;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                streamer.Id = connection.QuerySingle<int>(sql, new
                {
                    accessToken = streamer.AccessToken,
                    id = streamer.Id,
                    refreshToken = streamer.RefreshToken
                });

                return streamer;
            }
        }

        public Streamer GetStreamerById(int id)
        {
            string sql = "SELECT \"accessToken\", id, \"refreshToken\" " +
                "FROM public.\"Streamer\" WHERE id = @Id;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var streamer = connection.QuerySingle<Streamer>(sql, new { Id = id });
                return streamer;
            }
        }
    }
}
