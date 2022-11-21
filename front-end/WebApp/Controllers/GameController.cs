using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Drawing.Text;
using WebApp.BusinessLogic;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class GameController : Controller
    {
        private readonly GameLogic _gameLogic;

        public GameController()
        {
            _gameLogic = new GameLogic();
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Game game)
        {
            Game? response = _gameLogic.CreateGame(game);

            return RedirectToAction("Index");
        }
    }
}
