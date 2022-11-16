using Data.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DatabaseLayer
{
    public interface IGameModeAccess
    {
        public GameMode GetGameModeById(int id);
    }
}
