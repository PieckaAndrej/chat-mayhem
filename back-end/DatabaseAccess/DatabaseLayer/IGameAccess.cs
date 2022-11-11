using Data.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DatabaseLayer
{
    internal interface IGameAccess
    {
        List<Game> GetGames();
        Game GetGameById(int id);
        int CreateGame(Game game);
        int UpdateGame(int id, Game game);
        int DeleteGame(int id);

    }
}
