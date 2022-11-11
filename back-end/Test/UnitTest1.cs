using API.Controllers;
using Data.DatabaseLayer;
using Data.ModelLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Xunit.Abstractions;

namespace Test
{
    public class UnitTest1
    {

        private readonly ITestOutputHelper extraOutput;

        public UnitTest1(ITestOutputHelper extraOutput)
        {
            this.extraOutput = extraOutput;
        }

        [Fact]
        public void Test1()
        {
            Assert.True(false);
        }

        [Fact]
        public void TestCreateGame()
        {
            //Arrange
            var owner = new Streamer(1, "");
            var playNumber = 30;
            var gameMode = new GameMode(1, "", "");
            var timeLimit = TimeSpan.FromSeconds(15);
            GameAccess gameAccess = new GameAccess();
            var game = new Game(owner, playNumber, gameMode, timeLimit);

            //Act
            var editedRows = gameAccess.CreateGame(game);

            //Assert
            Assert.Equal(1, editedRows);
        }

        [Fact]
        public void TestGameController()
        {
            //Arrange
            var gameController = new GameController();
            //var game = new Game(null, 1, null, TimeSpan.FromSeconds(5));

            //Act
            var result = gameController.Post(null);
            extraOutput.WriteLine(result.ToString());

            //Arrange
            //Assert.Equal(Ok(), result);
        }
    }
}