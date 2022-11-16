using API.Controllers;
using API.DTOs;
using Data.DatabaseLayer;
using Data.ModelLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Test
{
    public class GameTest
    {

        private readonly ITestOutputHelper _extraOutput;
        private Game _testGame;
        private IConfiguration configuration;

        public GameTest(ITestOutputHelper extraOutput) { 
            _extraOutput = extraOutput;

            var owner = new Streamer(1, "1");
            var gameMode = new GameMode(1, "1", "1");
            var timeLimit = TimeSpan.FromSeconds(15);
            var questionPack = new QuestionPack(1, "", "", new string[1] ,"",DateTime.Now);
            _testGame = new Game(owner, gameMode, timeLimit, questionPack);
            configuration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", optional: false)
                .Build();
            _extraOutput.WriteLine(configuration.GetConnectionString("ChatMayhem Connection"));
        }

        [Fact]
        public void TestCreateGame()
        {
            //Arrange
            var gameAccess = new GameAccess(configuration.GetConnectionString("ChatMayhem Connection") ?? "");
            _extraOutput.WriteLine(configuration.GetConnectionString("ChatMayhem Connection"));

            //Act
            Game game = gameAccess.CreateGame(_testGame);
            Game resultGame = gameAccess.GetGameById(game.Id);

            //Assert
            Assert.Equal(_testGame.Streamer, resultGame.Streamer);
            Assert.Equal(_testGame.Mode, resultGame.Mode);
            Assert.Equal(_testGame.TimeLimit, resultGame.TimeLimit);
        }

        [Fact]
        public void TestGameController()
        {
            //Arrange
            var myConfiguration = new Dictionary<string, string>
            {
                {"Key1", "Value1"},
                {"Nested:Key1", "NestedValue1"},
                {"Nested:Key2", "NestedValue2"}
            };

            
            var gameController = new GameController(configuration);

            //Act

            ActionResult<GameDto> result = gameController.Post(GameDto.Convert(_testGame));
            Game? resultValue = GameDto.Convert(result.Value);
            var okResult = result.Result as OkObjectResult;

            //Arrange
            Assert.Equal(_testGame.Streamer, resultValue?.Streamer);
            Assert.Equal(_testGame.Mode, resultValue?.Mode);
            Assert.Equal(_testGame.TimeLimit, resultValue?.TimeLimit);
            Assert.Equal(StatusCodes.Status200OK, okResult?.StatusCode);
        }
    }
}