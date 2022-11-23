using Microsoft.AspNetCore.Mvc;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class ChatController : Controller
    {
        private ChatService? chatService;

        /*public async Task<IActionResult> Index(string answer)
        {
            string viewerAnswer = chatService?.CheckViewerAnswer(answer);

            await Clients.All.SendAsync("ReceiveMessage", user, message);

            return View();
        }*/
    }
}
