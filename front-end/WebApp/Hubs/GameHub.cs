using Data.ModelLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Policy;
using System.Text.Json;
using WebApp.BusinessLogic;
using WebApp.Models;
using WebApp.Services;


namespace WebApp.Hubs
{
    public class GameHub : Hub
    {
        private static AnswerLogic _logic = new AnswerLogic();
        private List<Lobby> _lobbies = new List<Lobby>();

        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }

        //don't know how to pass csharp object from javascript so passing json
        //public async Task CreateGroup(Game game)
        //{
        //    Streamer streamer = game.Streamer;

        //    if (streamer != null)
        //    {
        //        string groupName = Guid.NewGuid().ToString();

        //        await Groups.AddToGroupAsync(connectionId, groupName);
        //        _streamers.Add(new List<string>() { connectionId }, groupName);

        //        packJson = packJson.Replace("&quot;", "\"");
        //        _pack[groupName] = JsonSerializer.Deserialize<QuestionPack>(packJson);

        //        MessageHandlerService messageHandlerService = new MessageHandlerService(streamer);

        //        _messageHandlers[groupName] = messageHandlerService;
        //        await JoinGroup(groupName, connectionId);
        //    }
        //}


        //public async Task JoinGroup(string groupName, string connectionId)
        //{
        //    if (_streamers.ContainsValue(groupName))
        //    {
        //        await Groups.AddToGroupAsync(connectionId, groupName);
        //        _streamers.Where(pair => pair.Value == groupName).First().Key.Add(connectionId);
        //    }
        //}

        //public async Task StartGame(string connectionId)
        //{
        //    string groupName = GetGroupName(connectionId);

        //    MessageHandlerService handler = _messageHandlers[groupName];

        //    if (await handler.Connect())
        //    {
        //        var question = await handler.Listen();
        //        _questions.Add(groupName,question);
        //    } // TODO show error else
        //}

        //public async Task EndListening(string connectionId)
        //{
        //    string groupName = GetGroupName(connectionId);

        //    Console.WriteLine(connectionId);

        //    MessageHandlerService handler = _messageHandlers[groupName];

        //    Console.WriteLine(handler.IsRunning);

        //    handler.StopListening();
        //}

        //public async Task SendMessage(string message, string connectionId)
        //{
        //    string groupName = GetGroupName(connectionId);
        //    Question<Answer> question = _questions[groupName];


        //    Answer? answer = await _logic.CheckAnswer(message, question.ViewerAnswers);

        //    if (answer == null)
        //    {
        //        Console.WriteLine("wrong");
        //    }
        //    else
        //    {
        //        await Clients.Group(groupName).SendAsync("TurnAnswer", question.ViewerAnswers.IndexOf(answer), answer.Text, answer.Points);
        //    }
        //}

        //public string GetGroupName(string connectionId)
        //{
        //    return _streamers.Where(
        //        pair => pair.Key.Contains(connectionId)).First().Value;
        //}
        
        //public string GetConnectionId()
        //{
        //    return Context.ConnectionId;
        //}
    }
}