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
using System.Collections.Generic;

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
            string connectionId = "createGroup";

            Game game = new Game();
            Streamer streamer = new Streamer("test", "test", "test");
            QuestionPack questionPack = new QuestionPack();
            game.Streamer = streamer;
            game.QuestionPack = questionPack;

            // Act
            await _gameHub.CreateGroup(connectionId, game);
            Lobby? lobby = _gameHub.GetLobbyById(connectionId);

            // Assert
            Assert.Single(lobby.Players);
            Assert.Equal(streamer.Name, lobby.MessageHandler.Streamer.Name);
        }

        [Fact]
        public async void TestJoinGroup()
        {
            // Arrange
            string connectionId = "joinGroup";
            Game game = new Game();
            Streamer streamer = new Streamer("test", "test", "test");
            QuestionPack questionPack = new QuestionPack();
            game.Streamer = streamer;
            game.QuestionPack = questionPack;

            // Act
            await _gameHub.CreateGroup(connectionId, game);
            Lobby? lobby = _gameHub.GetLobbyById(connectionId);
            try
            {
                await _gameHub.JoinGroup("2", lobby.GroupName, "second");
            }
            catch (NullReferenceException) { }

            // Assert
            Assert.Equal(2, lobby.Players.Count);
            Assert.Equal("second", lobby.Players.Last().Name);
        }

        [Fact]
        public async void TestGetCurrentQuestion()
        {
            // Arrange 
            string connectionId = "getCurrentQuestion";

            Game game = new Game();
            Streamer streamer = new Streamer("test", "test", "test");
            QuestionPack questionPack = new QuestionPack();

            questionPack.Questions = new List<Question<ViewerAnswer>>() {
                new Question<ViewerAnswer>("test?", null, 0) };

            game.Streamer = streamer;
            game.QuestionPack = questionPack;

            Question<Answer> question = new Question<Answer>("test?", new List<Answer>()
            {
                new Answer(1, "test")
            }, 0);

            // Act
            await _gameHub.CreateGroup(connectionId, game);
            Lobby? lobby = _gameHub.GetLobbyById(connectionId);

            lobby.Answers[lobby.currentQuestionIndex] = question;
            Question<Answer> currentQuestion = _gameHub.GetCurrentQuestion(connectionId);

            // Assert
            Assert.Equal(question, currentQuestion);
        }

        [Fact]
        public async void TestAnswerRight()
        {
            // Arrange 
            string connectionId = "answerRight";

            Game game = new Game();
            Streamer streamer = new Streamer("test", "test", "test");
            QuestionPack questionPack = new QuestionPack();

            questionPack.Questions = new List<Question<ViewerAnswer>>() {
                new Question<ViewerAnswer>("test?", null, 0) };

            game.Streamer = streamer;
            game.QuestionPack = questionPack;

            Question<Answer> question = new Question<Answer>("test?", new List<Answer>()
            {
                new Answer(1, "test"),
                new Answer(2, "right")
            }, 0);

            // Act
            await _gameHub.CreateGroup(connectionId, game);
            Lobby? lobby = _gameHub.GetLobbyById(connectionId);

            lobby.Answers[lobby.currentQuestionIndex] = question;
            int response = await _gameHub.SendMessage(connectionId, "right");

            // Assert
            Assert.Equal(1, response);
            Assert.Equal(2, lobby.Players.First().Points);
            Assert.Equal(0, lobby.Players.First().WrongAnswers);
        }

        [Fact]
        public async void TestAnswerWrong()
        {
            // Arrange 
            string connectionId = "answerWrong";

            Game game = new Game();
            Streamer streamer = new Streamer("test", "test", "test");
            QuestionPack questionPack = new QuestionPack();

            questionPack.Questions = new List<Question<ViewerAnswer>>() {
                new Question<ViewerAnswer>("test?", null, 0) };

            game.Streamer = streamer;
            game.QuestionPack = questionPack;

            Question<Answer> question = new Question<Answer>("test?", new List<Answer>()
            {
                new Answer(1, "test"),
                new Answer(2, "test2")
            }, 0);

            // Act
            await _gameHub.CreateGroup(connectionId, game);
            Lobby? lobby = _gameHub.GetLobbyById(connectionId);

            lobby.Answers[lobby.currentQuestionIndex] = question;
            int response = await _gameHub.SendMessage(connectionId, "wrong");

            // Assert
            Assert.Equal(-1, response);
            Assert.Equal(0, lobby.Players.First().Points);
            Assert.Equal(1, lobby.Players.First().WrongAnswers);
        }

        [Fact]
        public async void TestAnswerWrongLocked()
        {
            // Arrange 
            string connectionId = "answerWrongLocked";

            Game game = new Game();
            Streamer streamer = new Streamer("test", "test", "test");
            QuestionPack questionPack = new QuestionPack();

            questionPack.Questions = new List<Question<ViewerAnswer>>() {
                new Question<ViewerAnswer>("test?", null, 0) };

            game.Streamer = streamer;
            game.QuestionPack = questionPack;

            Question<Answer> question = new Question<Answer>("test?", new List<Answer>()
            {
                new Answer(1, "test"),
                new Answer(2, "right")
            }, 0);

            // Act
            await _gameHub.CreateGroup(connectionId, game);
            Lobby? lobby = _gameHub.GetLobbyById(connectionId);

            lobby.Answers[lobby.currentQuestionIndex] = question;
            int response = await _gameHub.SendMessage(connectionId, "wrong");
            await _gameHub.SendMessage(connectionId, "wrong");
            await _gameHub.SendMessage(connectionId, "wrong");
            int responseRight = await _gameHub.SendMessage(connectionId, "right");

            // Assert
            Assert.Equal(-1, response);
            Assert.Equal(0, lobby.Players.First().Points);
            Assert.Equal(3, lobby.Players.First().WrongAnswers);
            Assert.Equal(-2, responseRight);
        }

        [Fact]
        public async void TestAnswerTwice()
        {
            // Arrange 
            string connectionId = "answerTwice";

            Game game = new Game();
            Streamer streamer = new Streamer("test", "test", "test");
            QuestionPack questionPack = new QuestionPack();

            questionPack.Questions = new List<Question<ViewerAnswer>>() {
                new Question<ViewerAnswer>("test?", null, 0) };

            game.Streamer = streamer;
            game.QuestionPack = questionPack;

            Question<Answer> question = new Question<Answer>("test?", new List<Answer>()
            {
                new Answer(1, "test"),
                new Answer(2, "right")
            }, 0);

            // Act
            await _gameHub.CreateGroup(connectionId, game);
            Lobby? lobby = _gameHub.GetLobbyById(connectionId);

            lobby.Answers[lobby.currentQuestionIndex] = question;
            int response = await _gameHub.SendMessage(connectionId, "right");
            int response2 = await _gameHub.SendMessage(connectionId, "right");

            // Assert
            Assert.Equal(1, response);
            Assert.Equal(-1, response2);
            Assert.Equal(2, lobby.Players.First().Points);
        }


        [Fact]
        public async void TestAnswerTwoPlayers()
        {
            // Arrange 
            string connectionId = "answerTwoPlayers";

            Game game = new Game();
            Streamer streamer = new Streamer("test", "test", "test");
            QuestionPack questionPack = new QuestionPack();

            questionPack.Questions = new List<Question<ViewerAnswer>>() {
                new Question<ViewerAnswer>("test?", null, 0) };

            game.Streamer = streamer;
            game.QuestionPack = questionPack;

            Question<Answer> question = new Question<Answer>("test?", new List<Answer>()
            {
                new Answer(1, "test"),
                new Answer(2, "right")
            }, 0);

            // Act
            await _gameHub.CreateGroup(connectionId, game);
            Lobby? lobby = _gameHub.GetLobbyById(connectionId);
            try
            {
                await _gameHub.JoinGroup(connectionId + "2", lobby.GroupName, "Player2");
            }
            catch (NullReferenceException) { }

            lobby.Answers[lobby.currentQuestionIndex] = question;
            int response = await _gameHub.SendMessage(connectionId, "right");
            int response2 = await _gameHub.SendMessage(connectionId + "2", "test");

            // Assert
            Assert.Equal(1, response);
            Assert.Equal(0, response2);
            Assert.Equal(2, lobby.Players.Single(player => player.ConnectionId == connectionId).Points);
            Assert.Equal(1, lobby.Players.Single(player => player.ConnectionId == connectionId + "2").Points);
        }

        [Fact]
        public async void TestNextQuestion()
        {
            // Arrange 
            string connectionId = "nextQuestion";

            Game game = new Game();
            Streamer streamer = new Streamer("test", "test", "test");
            QuestionPack questionPack = new QuestionPack();

            questionPack.Questions = new List<Question<ViewerAnswer>>() 
            {
                new Question<ViewerAnswer>("test?", null, 0),
                new Question<ViewerAnswer>("test again?", null, 0)
            };

            game.Streamer = streamer;
            game.QuestionPack = questionPack;

            Question<Answer> question = new Question<Answer>("test?", new List<Answer>()
            {
                new Answer(1, "test"),
                new Answer(2, "test2")
            }, 0);

            // Act
            await _gameHub.CreateGroup(connectionId, game);
            Lobby? lobby = _gameHub.GetLobbyById(connectionId);
            lobby.Answers[lobby.currentQuestionIndex] = question;
            int questionIndex = lobby.currentQuestionIndex;
            int response = await _gameHub.SendMessage(connectionId, "wrong");
            bool nextQuestion = await _gameHub.GoToNextQuestion(connectionId);


            // Assert
            Assert.Equal(-1, response);
            Assert.Equal(0, lobby.Players.Single(player => player.ConnectionId == connectionId).Points);
            Assert.True(nextQuestion);
            Assert.Equal(0, lobby.Players.Single(player => player.ConnectionId == connectionId).WrongAnswers);
        }
    }
}