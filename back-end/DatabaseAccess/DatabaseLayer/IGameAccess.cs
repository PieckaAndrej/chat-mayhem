using Data.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DatabaseLayer
{
    public interface IGameAccess
    {
        List<Game> GetGames();
        Game? GetGameById(int id);
        Game CreateGame(Game game);
        Game UpdateGame(int id, Game game);
        bool DeleteGame(int id);

    }
}
