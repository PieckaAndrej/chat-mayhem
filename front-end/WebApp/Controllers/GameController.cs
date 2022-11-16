using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Drawing.Text;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class GameController : Controller
    {
        private readonly RestClient _client;

        public GameController()
        {
            _client = new RestClient("https://localhost:7200/");
        }
        
        public IActionResult Index()
        {
            Games tamkd = new Games();
            return View(tamkd.games);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Game game)
        {
            var request = new RestRequest("api/Game").AddJsonBody(game);
            var response = await _client.ExecutePostAsync<Game>(request);
            Console.WriteLine("yo");

            return RedirectToAction("Index");
        }
    }
}
