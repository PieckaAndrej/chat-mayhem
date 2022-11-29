using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Drawing.Text;
using WebApp.BusinessLogic;
using WebApp.DTOs;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
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

        public async Task<IActionResult> Create(GameDto game)
        {
            Game? response = await _gameLogic.CreateGame(game);

            return RedirectToAction("Play", new { game = response });
        }

        public IActionResult Play(Game game)
        {
            Console.WriteLine(game.Streamer.Name);
            return RedirectToAction("Index", "Lobby", new { game = game });
        }
    }
}
