using Data.DatabaseLayer;
using Data.ModelLayer;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Test
{
    public class GameModeTest
    {
        private readonly ITestOutputHelper _extraOutput;
        private readonly GameMode _testGameMode;
        private readonly IConfiguration _testConfiguration;

        public GameModeTest(ITestOutputHelper extraOutput)
        {
            _extraOutput = extraOutput;

            _testGameMode = new GameMode("Fun game", "No cheating");

            _testConfiguration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.Test.json", optional: false)
                .Build();
        }

        [Fact]
        public void TestGetGame()
        {
            //Arrange
            _testGameMode.Id = 1;
            var gameModeAccess = new GameModeAccess(_testConfiguration.GetConnectionString("ChatMayhem Connection") ?? "");

            //Act
            var resultStreamer = gameModeAccess.GetGameModeById(_testGameMode.Id);

            //Assert
            Assert.Equal(_testGameMode.Id, resultStreamer?.Id);
            Assert.Equal(_testGameMode.Rules, resultStreamer?.Rules);
            Assert.Equal(_testGameMode.Description, resultStreamer?.Description);
        }
    }
}
