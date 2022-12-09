using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Test
{
    public class CreateDb
    {
        private static IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", optional: false)
                .Build();
        private CreateDb()
        {
            

        }

        private static CreateDb instance = null;
        public static CreateDb Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CreateDb();
                    create();
                }
                return instance;
            }
        }

        private static void create()
        {
            var s = "SELECT pg_terminate_backend(pg_stat_activity.pid) FROM pg_stat_activity " +
               "WHERE pg_stat_activity.datname = 'Chat Mayhem Test' AND pid <> pg_backend_pid(); " +
               "SELECT pg_terminate_backend(pg_stat_activity.pid) FROM pg_stat_activity " +
               "WHERE pg_stat_activity.datname = 'testData' AND pid <> pg_backend_pid(); " +
               "DROP DATABASE IF EXISTS \"testData\"; " +
               "CREATE DATABASE \"testData\" WITH TEMPLATE \"Chat Mayhem Test\" OWNER postgres;";

            using (var connection = new NpgsqlConnection(configuration.GetConnectionString("ChatMayhem Connection")))
            {
                connection.Open();
                connection.Execute(s);
            }
        }

    }
}
