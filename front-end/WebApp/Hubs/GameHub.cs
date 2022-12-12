using Data.ModelLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MongoDB.Driver.Core.Connections;
using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Policy;
using System.Text.Json;
using WebApp.BusinessLogic;
using WebApp.Models;
using WebApp.Services;


namespace WebApp.Hubs
{
    public class GameHub : Hub
    {
        private static List<Lobby> _lobbies = new List<Lobby>();

        public List<Lobby> Lobbies { get { return _lobbies; } }

        public async Task CreateGroup(string connectionId, Game game)
        {
            Streamer streamer = game.Streamer;

            if (streamer != null)
            {
                string groupName = Guid.NewGuid().ToString();
                MessageHandlerService messageHandlerService = new MessageHandlerService(streamer);

                Lobby lobby = new Lobby(groupName, game.QuestionPack, messageHandlerService);

                _lobbies.Add(lobby);

                _ = JoinGroup(connectionId, groupName, streamer.Name);
            }
        }

        public Question<Answer> GetCurrentQuestion(string connectionId)
        {
            Lobby? lobby = GetLobbyById(connectionId);

            return lobby.Answers[lobby.currentQuestionIndex];
        }

        public async Task JoinGroup(string connectionId, string groupName, string playerName)
        {
            Lobby? lobby = _lobbies.SingleOrDefault(lobby => lobby.GroupName == groupName);

            if (lobby != null)
            {
                await Groups.AddToGroupAsync(connectionId, groupName);
                Player player = new Player(playerName, connectionId);
                lobby.Players.Add(player);
            }
        }

        public async Task StartListening(string connectionId)
        {
            Lobby? lobby = GetLobbyById(connectionId);

            if (lobby == null)
            {
                return;
            }

            MessageHandlerService? handler = lobby.MessageHandler;

            if (handler == null)
            {
                return;
            }

            Question<ViewerAnswer>? currentQuestion = lobby.GetCurrentQuestion();

            if (currentQuestion == null)
            {
                return;
            }

            if (await handler.Connect())
            {
                _ = handler.Listen(async (username, message, streamerId) =>
                {
                    await QuestionService.InsertAnswers(
                        new ViewerAnswer(username, message), streamerId, currentQuestion);
                });
            } // TODO show error else
        }

        public bool EndListening(string connectionId)
        {
            Lobby? lobby = GetLobbyById(connectionId);
            MessageHandlerService? handler = lobby?.MessageHandler;

            if (handler == null)
            {
                return false;
            }

            Question<ViewerAnswer>? currentQuestion = lobby.GetCurrentQuestion();

            List<Answer> dbAnswers = QuestionService.GetAnswers(currentQuestion.Prompt, handler.Streamer.UserId);

            Question<Answer> answers = new Question<Answer>(
                currentQuestion.Prompt,
                dbAnswers,
                currentQuestion.QuestionId);

            if (answers.Answers.Count == 0)
            {
                return false;
            }

            handler.StopListening();

            lobby.Answers[lobby.currentQuestionIndex] = answers;

            return true;
        }

        public async Task SendAnswered(string connectionId)
        {
            Lobby? lobby = GetLobbyById(connectionId);

            var group = Clients.Group(lobby.GroupName);
            await group.SendAsync("Answered");
        }

        public List<Player> GetPlayers(string connectionId)
        {
            Lobby? lobby = GetLobbyById(connectionId);

            return lobby.Players;
        }

        public async Task SendUpdatedPlayer(string connectionId)
        {
            Lobby? lobby = GetLobbyById(connectionId);

            await Clients.Group(lobby.GroupName).SendAsync("PlayersUpdated", GetPlayers(connectionId));
        }

        public async Task<int> SendMessage(string connectionId, string message)
        {
            Lobby? lobby = GetLobbyById(connectionId);

            if (lobby == null)
            {
                return -2;
            }

            List<Answer> answers = lobby.Answers[lobby.currentQuestionIndex].Answers.ToList();

            Answer? answer = await AnswerLogic.CheckAnswer(message, answers);

            int returnIndex = -1;

            if (answer != null)
            {
                returnIndex = answers.IndexOf(answer);
                lobby.Players.SingleOrDefault(player => player.ConnectionId == connectionId)
                    .Points += answer.Points;
            }
            else
            {
                lobby.Players.SingleOrDefault(player => player.ConnectionId == connectionId)
                    .WrongAnswers++;
            }

            _ = SendUpdatedPlayer(connectionId);

            return returnIndex;
        }

        public bool GoToNextQuestion(string connectionId)
        {
            Lobby? lobby = GetLobbyById(connectionId);

            return lobby.NextQuestion();
        }

        public Lobby? GetLobbyById(string connectionId)
        {
            return _lobbies.SingleOrDefault(
                lobby => lobby.Players.Any(player => player.ConnectionId == connectionId));
        }
    }
}