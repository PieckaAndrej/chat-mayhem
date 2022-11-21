using Data.DatabaseLayer;
using System.Configuration;

namespace API.Services
{
    public class ServiceInjector
    {
        private static IConfiguration configuration = new ConfigurationBuilder()
                    .AddJsonFile($"appsettings.json", optional: false)
                    .Build();

        public ServiceInjector(IConfiguration configuration)
        {
            ServiceInjector.configuration = configuration;
        }

        private static string con
        {
            get
            {
                return configuration.GetConnectionString("ChatMayhem Connection") 
                    ?? "No connection string";
            }
        }

        public static GameService gameService
        {
            get
            {
                return new GameService(new GameAccess(con));
            }
        }

        public static StreamerService streamService
        {
            get
            {
                Console.WriteLine(con);
                return new StreamerService(new StreamerAccess(con)); 
            }
        }

        public static GameModeService gameModeService 
        {
            get
            {
                return new GameModeService(new GameModeAccess(con));
            }
        }

        public static QuestionPackService questionPackService
        {
            get
            {
                return new QuestionPackService(new QuestionPackAccess(con));
            }
        }
    }
}
