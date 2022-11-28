using Microsoft.AspNetCore.SignalR;
using WebApp.Services;

namespace WebApp.Hubs
{
    public class ChatHub : Hub
    {
        private ChatService? chatService = new ChatService();

        public async Task SendMessage(string message)
        {
            //message = await chatService.CheckViewerAnswer(message);
            Console.WriteLine(message);
            await Clients.All.SendAsync("TurnAnswer", message);
        }
    }
}