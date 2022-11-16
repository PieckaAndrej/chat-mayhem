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

        public IActionResult Create(int id)
        {
            Games tamkd = new Games();
            return View(tamkd.games[id]);
        }

        public async Task<IActionResult> CreatePost([FromBody] Game game)
        {
            var request = new RestRequest("api/Game").AddJsonBody(game);
            var response = await _client.ExecutePostAsync<Game>(request);
            Console.WriteLine("yo");
            if (!response.IsSuccessful)
            {
                return StatusCode(500, response.Data);
            }
            return StatusCode(201, response.Data);  
        }
    }
}
