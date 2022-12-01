using Data.ModelLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using NuGet.Protocol;
using System.Text.Json;
using WebApp.Models;
using WebApp.Services;
using WebApp.ViewModel;

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

            var lobbyViewModel = new LobbyViewModel();
            lobbyViewModel.Game = game;
            var question = new Question<Answer>("What is your favourite food?", new List<Answer>() { new Answer(), new Answer(), new Answer(), new Answer(), new Answer(), new Answer(), new Answer(), new Answer()}, 1);
            lobbyViewModel.Question = question;

            return View("Index", lobbyViewModel);
        }

        public IActionResult Game(LobbyViewModel lobbyViewModel)
        {
            List<Answer> list = new List<Answer>()
            {
                new Answer(3, "hello"),
                new Answer(3, "hello"),
                new Answer(3, "hello"),
                new Answer(3, "hello"),
            };

            Question<Answer> question = new Question<Answer>("Best hi?", list, 1);
            lobbyViewModel.Question = question;
            return View("Index", lobbyViewModel);
        }

        public IActionResult Question()
        {
            Console.WriteLine("xd");
            return View("Index");
        }
    }
}
