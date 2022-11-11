using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelLayer
{
    public class Game
    {
        public int Id { get; set; }
        public Streamer owner { get; set; }
        public int playerNumber { get; set; }
        public object mode { get; set; }
        public TimeSpan timeLimit { get; set; }

        public Game(Streamer owner, int playerNumber, object mode, TimeSpan timeLimit)
        {
            this.owner = owner;
            this.playerNumber = playerNumber;
            this.mode = mode;
            this.timeLimit = timeLimit;
        }

        public Game(int id, Streamer owner, int playerNumber, object mode, TimeSpan timeLimit)
            : this(owner, playerNumber, mode, timeLimit)
        {
            this.Id = id;
        }


    }
}
