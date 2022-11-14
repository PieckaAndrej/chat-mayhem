using API.Controllers;
using Data.DatabaseLayer;
using Data.ModelLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.InteropServices;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Test
{
    public class UnitTest1
    {

        private readonly ITestOutputHelper _extraOutput;
        private Game _testGame;

        public UnitTest1(ITestOutputHelper extraOutput)
        {
            _extraOutput = extraOutput;

            var owner = new Streamer(1, "");
            var playNumber = 30;
            var gameMode = new GameMode(1, "", "");
            var timeLimit = TimeSpan.FromSeconds(15);
            _testGame = new Game(owner, playNumber, gameMode, timeLimit);
        }

        [Fact]
        public void TestCreateGame()
        {
            //Arrange
            var gameAccess = new GameAccess();

            //Act
            Game editedRows = gameAccess.CreateGame(_testGame);

            //Assert
            Assert.Equal(_testGame.Owner, editedRows.Owner);
            Assert.Equal(_testGame.PlayerNumber, editedRows.PlayerNumber);
            Assert.Equal(_testGame.Mode, editedRows.Mode);
            Assert.Equal(_testGame.TimeLimit, editedRows.TimeLimit);
        }

        [Fact]
        public void TestGameController()
        {
            //Arrange
            var gameController = new GameController();

            //Act
            ActionResult<Game> result = gameController.Post(_testGame);
            Game? resultValue = result.Value;
            var okResult = result.Result as OkObjectResult;

            //Arrange
            Assert.Equal(_testGame.Owner, resultValue?.Owner);
            Assert.Equal(_testGame.PlayerNumber, resultValue?.PlayerNumber);
            Assert.Equal(_testGame.Mode, resultValue?.Mode);
            Assert.Equal(_testGame.TimeLimit, resultValue?.TimeLimit);
            Assert.Equal(StatusCodes.Status200OK, okResult?.StatusCode);
        }
    }
}