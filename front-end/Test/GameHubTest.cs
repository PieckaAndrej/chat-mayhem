using System.Dynamic;
using System;
using Xunit;
using WebApp.Hubs;
using Moq;
using Microsoft.AspNetCore.SignalR;
using WebApp.Models;
using System.Linq;
using Xunit.Sdk;
using System.Reflection;

namespace Test
{
    public class GameHubTest 
    {
        private readonly GameHub _gameHub;

        public GameHubTest()
        {
            var mockClients = new Mock<IHubCallerClients>();
            var mockGroupManager = new Mock<IGroupManager>();

            var hub = new GameHub()
            {
                Clients = mockClients.Object,
                Groups = mockGroupManager.Object
            };
            var all = new Mock<IClientProxy>();

            mockClients.Setup(m => m.All).Returns(all.Object);

            _gameHub = hub;
        }

        [Fact]
        public async void TestCreateGame()
        {
            // Arrange 
            Game game = new Game();
            Streamer streamer = new Streamer("test", "test", "test");
            QuestionPack questionPack = new QuestionPack();
            game.Streamer = streamer;
            game.QuestionPack = questionPack;

            // Act
            await _gameHub.CreateGroup("createGroup", game);
            Lobby? lobby = _gameHub.GetLobbyById("createGroup");

            // Assert
            Assert.Single(lobby.Players);
            Assert.Equal(streamer.Name, lobby.MessageHandler.Streamer.Name);
        }

        [Fact]
        public async void TestJoinGroup()
        {
            // Arrange 
            Game game = new Game();
            Streamer streamer = new Streamer("test", "test", "test");
            QuestionPack questionPack = new QuestionPack();
            game.Streamer = streamer;
            game.QuestionPack = questionPack;

            // Act
            await _gameHub.CreateGroup("joinGroup", game);
            Lobby? lobby = _gameHub.GetLobbyById("joinGroup");
            await _gameHub.JoinGroup("2", lobby.GroupName, "second");

            // Assert
            Assert.Equal(2, lobby.Players.Count);
            Assert.Equal("second", lobby.Players.Last().Name);
        }
    }
}