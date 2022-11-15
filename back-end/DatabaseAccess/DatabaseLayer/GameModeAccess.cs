using Data.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DatabaseLayer
{
    public class GameModeAccess : IGameModeAccess
    {
        private readonly string _connectionString;

        public GameModeAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public GameMode GetGameModeById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
