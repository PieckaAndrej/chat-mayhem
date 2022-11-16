using Data.DatabaseLayer;
using Data.ModelLayer;

namespace API.Services
{
    public class GameModeService
    {
        private IGameModeAccess _gameModeAccess;

        public GameModeService(IGameModeAccess gameModeAccess)
        {
            _gameModeAccess = gameModeAccess;
        }

        public GameMode Get(int id)
        {
            return _gameModeAccess.GetGameModeById(id);
        }
    }
}
