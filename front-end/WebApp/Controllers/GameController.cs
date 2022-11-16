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
            return View();
        }

        public async Task<IActionResult> CreateGame([FromBody] Game game)
        {
            var request = new RestRequest("api/Game").AddJsonBody(game);
            var response = await _client.ExecutePostAsync<Game>(request);
            if (!response.IsSuccessful)
            {
                return StatusCode(500, response.Data);
            }
            return StatusCode(201, response.Data);  
        }
    }
}
