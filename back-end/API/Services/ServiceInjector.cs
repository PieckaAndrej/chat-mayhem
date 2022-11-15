﻿using Data.DatabaseLayer;
using System.Configuration;

namespace API.Services
{
    public class ServiceInjector
    {

        private static string? con
        {
            get
            {
                ConfigurationManager configurationManager = new ConfigurationManager();

                return configurationManager.GetConnectionString("ChatMayhem Connection");
            }
        }

        public static GameService gameService
        {
            get
            {
                return new GameService(new GameAccess(con ?? ""));
            }
        }

        public static StreamerService streamService
        {
            get
            {
                return new StreamerService(new StreamerAccess(con ?? ""));
            }
        }

        public static GameModeService gameModeService 
        {
            get
            {
                return new GameModeService(new GameModeAccess(con ?? ""));
            }
        }
    }
}
