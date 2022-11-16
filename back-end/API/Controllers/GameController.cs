using API.DTOs;
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
        public ActionResult<GameDto> Post(GameDto inGame)
        {
            Game game = GameDto.Convert(inGame);
            game = _gameService.Add(game);
            GameDto returnGame = GameDto.Convert(game);

            if (returnGame == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return returnGame;
        }
    }
}
