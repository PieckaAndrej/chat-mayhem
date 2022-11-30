﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using WebApp.BusinessLogic;
using WebApp.Models;
using WebApp.Services;


namespace WebApp.Hubs
{
    [Authorize]
    public class GameHub : Hub
    {
        private static AnswerLogic _logic = new AnswerLogic();
        private static Dictionary<string, Question<Answer>> _questions =
            new Dictionary<string, Question<Answer>>();

        private static Dictionary<string, MessageHandlerService> _messageHandlers =
            new Dictionary<string, MessageHandlerService>();

        private static Dictionary<List<string>, string> _streamers =
            new Dictionary<List<string>, string>();

        //don't know how to pass csharp object from javascript so passing json
        public async Task CreateGroup(string streamerJson)
        {
            string connectionId = Context.ConnectionId;

            //for some reason javascript replaces the quotes with the wierd text so replacing it back :)
            streamerJson = streamerJson.Replace("&quot;", "\"");
            Streamer? streamer = JsonSerializer.Deserialize<Streamer>(streamerJson);

            if (streamer != null)
            {
                string guid = Guid.NewGuid().ToString();

                await Groups.AddToGroupAsync(connectionId, guid);
                _streamers.Add(new List<string>() { connectionId }, guid);

                MessageHandlerService messageHandlerService = new MessageHandlerService(streamer);

                _messageHandlers[guid] = messageHandlerService;
                await JoinGroup(guid);
            }
        }


        public async Task JoinGroup(string groupName)
        {
            string connectionId = Context.ConnectionId;

            if (_streamers.ContainsValue(groupName))
            {
                await Groups.AddToGroupAsync(connectionId, groupName);
                _streamers.Where(pair => pair.Value == groupName).First().Key.Add(connectionId);
            }
        }

        public async Task StartGame()
        {
            string connectionId = Context.ConnectionId;
            string groupName = GetGroupName(connectionId);

            MessageHandlerService handler = _messageHandlers[groupName];

            if (await handler.Connect())
            {
                await handler.Listen();
            } // TODO show error else
        }

        public async Task SendMessage(string message)
        {
            string connectionId = Context.ConnectionId;
            string groupName = GetGroupName(connectionId);
            Question<Answer> question = _questions[groupName];


            Answer? answer = await _logic.CheckAnswer(message, question.ViewerAnswers);

            if (answer == null)
            {
                Console.WriteLine("wrong");
            }
            else
            {
                await Clients.Group(groupName).SendAsync("TurnAnswer", question.ViewerAnswers.IndexOf(answer));
            }
        }

        public string GetGroupName(string connectionId)
        {
            return _streamers.Where(
                pair => pair.Key.Contains(connectionId)).First().Value;
        }
    }
}