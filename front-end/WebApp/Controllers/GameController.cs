using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using RestSharp;
using System.Drawing.Text;
using System.Security.Claims;
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
        
        public async Task<IActionResult> Index()
        {
            QuestionPackLogic questionPackLogic = new QuestionPackLogic();
            List<QuestionPack>? questionPacks = await questionPackLogic.GetAllQuestionPacks();

            ViewData["QuestionPacks"] = questionPacks;

            return View();
        }

        public async Task<IActionResult> Create(GameDto game)
        {
            Game? response = await _gameLogic.CreateGame(game);

            ClaimsIdentity? identity = (ClaimsIdentity)HttpContext.User.Identity;

            Claim? idClaim = identity.Claims.SingleOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            Claim? accessClaim = identity.Claims.SingleOrDefault(claim => claim.Type == "AccessToken");
            Claim? nameClaim = identity.Claims.SingleOrDefault(claim => claim.Type == ClaimTypes.Name);

            Streamer streamer = new Streamer(nameClaim.Value, accessClaim.Value, idClaim.Value);

            response.Streamer = streamer;

            Response.Cookies.Append("game", JsonSerializer.Serialize(response));

            // maybe see the game first and have a button to play it later
            return RedirectToAction("Play", response);
        }

        public IActionResult Play()
        {
            return RedirectToAction("Index", "Lobby");
        }
    }
}
