using Data.ModelLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.SignalR;
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

        public async Task CreateGroup(string connectionId, Game game)
        {
            Streamer streamer = game.Streamer;

            if (streamer != null)
            {
                string groupName = Guid.NewGuid().ToString();
                MessageHandlerService messageHandlerService = new MessageHandlerService(streamer);

                Lobby lobby = new Lobby(groupName, game.QuestionPack, messageHandlerService);

                _lobbies.Add(lobby);

                await JoinGroup(connectionId, groupName);
            }
        }

        public Question<Answer> GetCurrentQuestion(string connectionId)
        {
            Lobby? lobby = _lobbies.SingleOrDefault(lobby => lobby.Players.Contains(connectionId));

            return lobby.Answers[lobby.currentQuestionIndex];
        }

        public async Task JoinGroup(string connectionId, string groupName)
        {
            Lobby? lobby = _lobbies.SingleOrDefault(lobby => lobby.GroupName == groupName);

            if (lobby != null)
            {
                await Groups.AddToGroupAsync(connectionId, groupName);
                lobby.Players.Add(connectionId);
            }
        }

        public async Task StartGame(string connectionId)
        {
            Lobby? lobby = _lobbies.SingleOrDefault(
                lobby => lobby.Players.Contains(connectionId));

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
                    int inserted = await QuestionService.InsertAnswers(
                        new ViewerAnswer(username, message), streamerId, currentQuestion);

                    if (inserted == 1)
                    {
                        _ = Clients.Group(lobby.GroupName).SendAsync("Answered");
                    }
                });
            } // TODO show error else
        }

        public void EndListening(string connectionId)
        {
            MessageHandlerService? handler = _lobbies.SingleOrDefault(
                lobby => lobby.Players.Contains(connectionId))?.MessageHandler;

            if (handler == null)
            {
                return;
            }

            handler.StopListening();

            Lobby? lobby = _lobbies.SingleOrDefault(
                lobby => lobby.Players.Contains(connectionId));

            Question<ViewerAnswer>? currentQuestion = lobby.GetCurrentQuestion();

            List<Answer> dbAnswers = QuestionService.GetAnswers(currentQuestion.Prompt, handler.Streamer.UserId);

            Question<Answer> answers = new Question<Answer>(
                currentQuestion.Prompt,
                dbAnswers,
                currentQuestion.QuestionId);

            lobby.Answers[lobby.currentQuestionIndex] = answers;
        }

        public async Task SendAnswered(string connectionId)
        {
            Lobby? lobby = _lobbies.SingleOrDefault(
                lobby => lobby.Players.Contains(connectionId));

            var group = Clients.Group(lobby.GroupName);
            await group.SendAsync("Answered");
        }

        public async Task<int> SendMessage(string connectionId, string message)
        {
            Lobby? lobby = _lobbies.SingleOrDefault(
                lobby => lobby.Players.Contains(connectionId));

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
            }

            return returnIndex;
        }
    }
}