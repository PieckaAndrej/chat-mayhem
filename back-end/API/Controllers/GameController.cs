using API.Services;
using Data.DatabaseLayer;
using Data.ModelLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private GameService _gameService;

        public GameController(IConfiguration inConfiguration)
        {
            string? connectionString = inConfiguration.GetConnectionString("ChatMayhem Connection");
            if (connectionString == null)
            {
                Console.WriteLine("Connection string is null");
            } 

            _gameService = new GameService(new GameAccess(connectionString ?? ""));
        }

        [HttpPost]
        public ActionResult<Game> Post(Game inGame)
        {
            Game returnGame;

            try
            {
                returnGame = _gameService.Add(inGame);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            if (returnGame == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return returnGame;
        }
    }
}
