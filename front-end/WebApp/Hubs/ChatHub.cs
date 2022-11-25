using Microsoft.AspNetCore.SignalR;
using WebApp.Services;

namespace WebApp.Hubs
{
    public class ChatHub : Hub
    {
        private ChatService? chatService;

        public async Task SendMessage(string message)
        {
            message = (await chatService.CheckViewerAnswer(message)).Content;
           await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}