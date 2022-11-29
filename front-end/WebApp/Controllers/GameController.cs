using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using RestSharp;
using System.Drawing.Text;
using System.Text.Json;
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

            TempData["game"] = JsonSerializer.Serialize(response);

            // maybe see the game first and have a button to play it later
            return RedirectToAction("Play", response);
        }

        public IActionResult Play()
        {
            return RedirectToAction("Index", "Lobby");
        }
    }
}
