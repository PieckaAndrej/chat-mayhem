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

                Lobby lobby = new Lobby(groupName, game, messageHandlerService);

                _lobbies.Add(lobby);

                _ = JoinGroup(connectionId, groupName, streamer.Name);
            }
        }

        public Question<Answer> GetCurrentQuestion(string connectionId)
        {
            Lobby? lobby = GetLobbyById(connectionId);

            return lobby.Answers[lobby.currentQuestionIndex];
        }

        public string? GetGroupName(string connectionId)
        {
            Lobby? lobby = GetLobbyById(connectionId);

            return lobby?.GroupName;
        }

        public async Task<bool> JoinGroup(string connectionId, string groupName, string playerName)
        {
            bool retVal = false;
            Lobby? lobby = _lobbies.SingleOrDefault(lobby => lobby.GroupName == groupName);

            if (lobby != null)
            {
                await Groups.AddToGroupAsync(connectionId, groupName);
                Player player = new Player(playerName, connectionId);
                lobby.Players.Add(player);

                await SendLobbyChanged(connectionId);

                retVal = true;
            }

            return retVal;
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

            if (await handler.Connect((access) => Clients.Caller.SendAsync("Refresh", access)))
            {
                _ = handler.Listen(async (username, message, streamerId) =>
                {
                    await QuestionService.InsertAnswers(
                        new ViewerAnswer(username, message), streamerId, currentQuestion);

                    await SendAnswered(connectionId);
                });
            } // TODO show error else
        }

        public async Task<bool> EndListening(string connectionId)
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

            await SendLobbyChanged(connectionId);

            return true;
        }

        public async Task SendAnswered(string connectionId)
        {
            Lobby? lobby = GetLobbyById(connectionId);

            var group = Clients.Group(lobby.GroupName);
            await group.SendAsync("Answered");
        }

        public async Task<int> SendMessage(string connectionId, string message)
        {
            Lobby? lobby = GetLobbyById(connectionId);

            if (lobby == null)
            {
                return -3;
            }

            if (lobby.Players.Single(player => player.ConnectionId == connectionId)
                .WrongAnswers >= 3)
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

                answer.Answered = true;
            }
            else
            {
                lobby.Players.SingleOrDefault(player => player.ConnectionId == connectionId)
                    .WrongAnswers++;
            }

            _ = SendLobbyChanged(connectionId);

            return returnIndex;
        }

        public async Task<bool> GoToNextQuestion(string connectionId)
        {
            Lobby? lobby = GetLobbyById(connectionId);
            return lobby.NextQuestion();
        }

        public async Task SetGameState(string connectionId, Lobby.GAME_STATE state)
        {
            Lobby? lobby = GetLobbyById(connectionId);

            lobby.GameState = state;

            await SendLobbyChanged(connectionId);
        }

        public async Task SendLobbyChanged(string connectionId)
        {
            Lobby? lobby = GetLobbyById(connectionId);

            await Clients.Group(lobby.GroupName).SendAsync("LobbyUpdated", lobby);
        }

        public Lobby? GetLobbyById(string connectionId)
        {
            return _lobbies.SingleOrDefault(
                lobby => lobby.Players.Any(player => player.ConnectionId == connectionId));
        }
    }
}