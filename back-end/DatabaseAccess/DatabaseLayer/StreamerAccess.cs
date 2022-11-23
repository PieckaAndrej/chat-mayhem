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
            if (GetStreamerById(streamer.Id) == null)
            {
                string sql = "INSERT INTO public.\"Streamer\"( \"accessToken\", id, \"refreshToken\") " +
                "VALUES (@accessToken, @id, @refreshToken) RETURNING id;";

                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Execute(sql, new
                    {
                        accessToken = streamer.AccessToken,
                        id = streamer.Id,
                        refreshToken = streamer.RefreshToken
                    });

                    return streamer;
                }
            }
            return streamer;
            
        }

        public Streamer GetStreamerById(string id)
        {
            string sql = "SELECT \"accessToken\", id, \"refreshToken\" " +
                "FROM public.\"Streamer\" WHERE id = @Id;";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var streamer = connection.QuerySingleOrDefault<Streamer>(sql, new { Id = id });
                return streamer;
            }
        }
    }
}
