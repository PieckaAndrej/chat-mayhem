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
    }
}
