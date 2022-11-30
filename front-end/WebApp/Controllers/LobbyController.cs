using Data.ModelLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using NuGet.Protocol;
using System.Text.Json;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Authorize]
    public class LobbyController : Controller
    {
        public IActionResult Index()
        {
            // null warnings lol
            // if you go there without coming from game controller, then you get error
            string json = TempData["game"] as string;
            Game game = JsonSerializer.Deserialize<Game>(json);
            return View(game);
        }

        public IActionResult Game()
        {
            List<Answer> list = new List<Answer>()
            {
                new Answer(3, "hello"),
                new Answer(3, "hello"),
                new Answer(3, "hello"),
                new Answer(3, "hello"),
            };

            Question<Answer> question = new Question<Answer>("Best hi?", list);
            return View(question);
        }

        public IActionResult Question()
        {
            return View();
        }
    }
}
