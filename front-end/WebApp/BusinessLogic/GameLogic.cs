using RestSharp;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.BusinessLogic
{
    public class GameLogic
    {
        private readonly GameService _gameService;

        public GameLogic()
        {
            _gameService = new GameService();
        }

        public Game? CreateGame(Game game)
        {
            RestResponse<Game> result = _gameService.CreateGame(game).Result;

            return result.Data;
        }
    }
}
