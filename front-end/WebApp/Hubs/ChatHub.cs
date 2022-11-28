using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using WebApp.BusinessLogic;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private ChatLogic _logic = new ChatLogic();

        public async Task SendMessage(string message)
        {
            List<Answer> answerList = new List<Answer>() { new Answer(12, "Shirt"), new Answer(123, "Pizza") };
            Answer? answer = await _logic.CheckAnswer(message, answerList);

            if (answer == null)
            {
                Console.WriteLine("wrong");
            }
            else
            {
                await Clients.All.SendAsync("TurnAnswer", 3);
            }
        }
    }
}