using Data.DatabaseLayer;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using System.Configuration;

namespace API.Services
{
    public class ServiceInjector
    {
        private static string _con = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build().GetConnectionString("ChatMayhem Connection")
                    ?? "No connection string";
        public static string Con 
        {
            get
            {
                return _con; 
            }
            set
            {
                _con = value;
            } 
        }

        public static GameService gameService
        {
            get
            {
                return new GameService(new GameAccess(Con));
            }
        }

        public static ChatService chatService
        {
            get
            {
                return new ChatService(new ChatAccess(Con));
            }
        }

        public static StreamerService streamService
        {
            get
            {
                Console.WriteLine(Con);
                return new StreamerService(new StreamerAccess(Con)); 
            }
        }

        public static GameModeService gameModeService 
        {
            get
            {
                return new GameModeService(new GameModeAccess(Con));
            }
        }

        public static QuestionPackService questionPackService
        {
            get
            {
                return new QuestionPackService(new QuestionPackAccess(Con));
            }
        }
    }
}
