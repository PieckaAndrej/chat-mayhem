using API.Controllers;
using API.DTOs;
using Dapper;
using Data.DatabaseLayer;
using Data.ModelLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Test
{
    public class GameTest
    {

        private readonly ITestOutputHelper _extraOutput;
        private readonly Game _testGame;
        private readonly IConfiguration _testConfiguration;

        public GameTest(ITestOutputHelper extraOutput)
        {

            _extraOutput = extraOutput;

            var owner = new Streamer("asdfg", "21345", "abcde");
            var gameMode = new GameMode(1, "Fun game", "No cheating");
            var timeLimit = TimeSpan.FromSeconds(15);
            var questionPack = new QuestionPack(1, "me", "best questions", new string[1] {"m"} ,"questions",DateTime.Parse("2022-11-15"));
            _testGame = new Game(owner, gameMode, timeLimit, questionPack);

            _testConfiguration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.Test.json", optional: false)
                .Build();
        }

        [Fact]
        public void TestCreateGame()
        {
            //Arrange
            _testGame.TimeLimit = TimeSpan.FromSeconds(21);
            var gameAccess = new GameAccess(_testConfiguration.GetConnectionString("ChatMayhem Connection") ?? "");

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
            _extraOutput.WriteLine(_testConfiguration.GetConnectionString("ChatMayhem Connection"));

            _testGame.TimeLimit = TimeSpan.FromSeconds(22);
            var gameController = new GameController(_testConfiguration);

            //Act
            ActionResult<Game> result = gameController.Post(GameDto.Convert(_testGame));

            Game resultValue = result.Value;

            //Arrange
            Assert.Equal(_testGame.Streamer, resultValue?.Streamer);
            Assert.Equal(_testGame.Mode, resultValue?.Mode);
            Assert.Equal(_testGame.TimeLimit, resultValue?.TimeLimit);
        }

        [Fact]
        public void TestUpdateGame()
        {
            //Arrange
            var gameAccess = new GameAccess(_testConfiguration.GetConnectionString("ChatMayhem Connection") ?? "");
            int id = 115;
            _testGame.TimeLimit = TimeSpan.FromSeconds(23);

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
            var gameController = new GameController(_testConfiguration);

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
            _testGame.TimeLimit = TimeSpan.FromSeconds(24);
            var gameAccess = new GameAccess(_testConfiguration.GetConnectionString("ChatMayhem Connection") ?? "");

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
            _testGame.TimeLimit = TimeSpan.FromSeconds(25);
            var gameAccess = new GameAccess(_testConfiguration.GetConnectionString("ChatMayhem Connection") ?? "");

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