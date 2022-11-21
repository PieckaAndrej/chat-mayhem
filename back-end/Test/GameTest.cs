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
        }

        [Fact]
        public void TestCreateGame()
        {
            //Arrange
            var gameAccess = new GameAccess(configuration.GetConnectionString("ChatMayhem Connection") ?? "");

            //Act
            Game game = gameAccess.CreateGame(_testGame);
            Game? resultGame = gameAccess.GetGameById(game.Id);

            //Assert
            Assert.Equal(_testGame.Streamer, resultGame?.Streamer);
            Assert.Equal(_testGame.Mode, resultGame?.Mode);
            Assert.Equal(_testGame.TimeLimit, resultGame?.TimeLimit);
        }

        [Fact]
        public void TestPostGameController()
        {
            //Arrange
            var gameController = new GameController(configuration);

            //Act
            ActionResult<GameDto> result = gameController.Post(GameDto.Convert(_testGame));
            Game? resultValue = GameDto.Convert(result.Value);

            //Arrange
            Assert.Equal(_testGame.Streamer, resultValue?.Streamer);
            Assert.Equal(_testGame.Mode, resultValue?.Mode);
            Assert.Equal(_testGame.TimeLimit, resultValue?.TimeLimit);
        }

        [Fact]
        public void TestUpdateGame()
        {
            //Arrange
            var gameAccess = new GameAccess(configuration.GetConnectionString("ChatMayhem Connection") ?? "");
            int id = 1;
            _testGame.TimeLimit = TimeSpan.FromSeconds(20);

            //Act
            Game game = gameAccess.UpdateGame(id, _testGame);
            Game? resultGame = gameAccess.GetGameById(id);

            //Assert
            Assert.Equal(_testGame.TimeLimit, resultGame?.TimeLimit);
        }

        /*[Fact]
        public void TestPutGameController()
        {
            //Arrange
            var gameController = new GameController(configuration);

            //Act
            ActionResult<GameDto> result = gameController.Post(GameDto.Convert(_testGame));
            Game? resultValue = GameDto.Convert(result.Value);

            //Arrange
            Assert.Equal(_testGame.Streamer, resultValue?.Streamer);
            Assert.Equal(_testGame.Mode, resultValue?.Mode);
            Assert.Equal(_testGame.TimeLimit, resultValue?.TimeLimit);
        }*/

        [Fact]
        public void TestDeleteGame()
        {
            //Arrange
            var gameAccess = new GameAccess(configuration.GetConnectionString("ChatMayhem Connection") ?? "");

            //Act
            gameAccess.CreateGame(_testGame);
            bool deleted = gameAccess.DeleteGame(_testGame.Id);

            //Assert
            Assert.True(deleted);
        }

        [Fact]
        public void TestGetGame()
        {
            //Arrange
            var gameAccess = new GameAccess(configuration.GetConnectionString("ChatMayhem Connection") ?? "");

            //Act
            gameAccess.CreateGame(_testGame);
            Game? resultGame = gameAccess.GetGameById(_testGame.Id);

            //Assert
            Assert.Equal(_testGame.Streamer, resultGame?.Streamer);
            Assert.Equal(_testGame.Mode, resultGame?.Mode);
            Assert.Equal(_testGame.TimeLimit, resultGame?.TimeLimit);
        }
    }
}