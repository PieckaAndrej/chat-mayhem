using Dapper;
using Data.ModelLayer;
using Microsoft.Win32.SafeHandles;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Data.DatabaseLayer
{
    public class ChatAccess : IChatAccess
    {
        private readonly string _connectionString;

        public ChatAccess(string connectionString)
        {
            _connectionString = connectionString;
            Console.WriteLine(_connectionString);
        }

        public List<string> GetAnswers()
        {
            string sqlString = "SELECT answer FROM \"Answer\"";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                List<string> viewerAnswers = connection.Query<string>(sqlString).ToList();

                return viewerAnswers;   
            }
        }
    }
}
