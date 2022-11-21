using Data.DatabaseLayer;
using Data.ModelLayer;

namespace API.Services
{
    public class GameService
    {
        private IGameAccess _gameAccess;

        public GameService(IGameAccess gameAccess)
        {
            _gameAccess = gameAccess;
        }

        public Game Add(Game game)
        {
            return _gameAccess.CreateGame(game);
        }

        public Game? GetGameById(int id)
        {
            return _gameAccess.GetGameById(id);
        }

        public Game UpdateGame(int id, Game game)
        {
            return _gameAccess.UpdateGame(id, game);
        }

        public Game? DeleteGame(int id)
        {
            return _gameAccess.DeleteGame(id);
        }
    }
}
