using API.DTOs;
using API.Services;
using Data.DatabaseLayer;
using Data.ModelLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private GameService _gameService;

        public GameController(IConfiguration inConfiguration)
        {
            ServiceInjector.Con = inConfiguration.GetConnectionString("ChatMayhem Connection") ?? "No connection string";

            string? connectionString = inConfiguration.GetConnectionString("ChatMayhem Connection");
            if (connectionString == null)
            {
                Console.WriteLine("Connection string is null");
            } 

            _gameService = new GameService(new GameAccess(connectionString ?? "No connection string"));
        }

        [HttpPost]
        public ActionResult<GameDto> Post(GameDto inGame)
        {
            Game game = GameDto.Convert(inGame);

            game = ServiceInjector.gameService.Add(game);
            GameDto returnGame = GameDto.Convert(game);

            if (returnGame == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return returnGame;
        }

        [HttpPut]
        public ActionResult<GameDto> Put(GameDto inGame, int id)
        {
            Game game = GameDto.Convert(inGame);

            game = ServiceInjector.gameService.UpdateGame(id, game);
            GameDto returnGame = GameDto.Convert(game);

            if (returnGame == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return returnGame;
        }

        [HttpGet]
        public ActionResult<GameDto> Get(int id)
        {
            Game game = ServiceInjector.gameService.GetGameById(id);

            GameDto returnGame = GameDto.Convert(game);

            if (returnGame == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return returnGame;
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            bool deleted = ServiceInjector.gameService.DeleteGame(id);

            if (deleted == true)
            {
                return new StatusCodeResult(200);
            }
            else
            {
                return new StatusCodeResult(204);
            }
        }
    }
}
