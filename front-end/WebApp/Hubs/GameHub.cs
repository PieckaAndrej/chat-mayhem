using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
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

        public async Task CreateGroup(string connectionId, Streamer streamer)
        {
            string guid = Guid.NewGuid().ToString();

            await Groups.AddToGroupAsync(connectionId, guid);
            _streamers.Add(new List<string>() { connectionId }, guid);

            MessageHandlerService messageHandlerService = new MessageHandlerService(streamer);

            _messageHandlers[guid] = messageHandlerService;
        }


        public async Task JoinGroup(string connectionId, string groupName)
        {
            if (_streamers.ContainsValue(groupName))
            {
                await Groups.AddToGroupAsync(connectionId, groupName);
                _streamers.Where(pair => pair.Value == groupName).First().Key.Add(connectionId);
            }
        }
        
        public async Task StartGame(string connectionId)
        {
            string groupName = GetGroupName(connectionId);

            MessageHandlerService handler = _messageHandlers[groupName];

            if (await handler.Connect())
            {
                await handler.Listen();
            } // TODO show error else
        }

        public async Task SendMessage(string connectionId, string message)
        {

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

        private string GetGroupName(string connectionId)
        {
            return _streamers.Where(
                pair => pair.Key.Contains(connectionId)).First().Value;
        }
    }
}