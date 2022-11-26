using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private ChatService? chatService;

        /*public async Task<IActionResult> Index(string answer)
        {
            string viewerAnswer = chatService?.CheckViewerAnswer(answer);

            await Clients.All.SendAsync("ReceiveMessage", user, message);

            return View();
        }*/

        public IActionResult Index()
        {
            return View();
        }
    }
}
