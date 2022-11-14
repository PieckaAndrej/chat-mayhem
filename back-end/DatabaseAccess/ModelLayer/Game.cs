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
        public Streamer Owner { get; set; }
        public int PlayerNumber { get; set; }
        public GameMode Mode { get; set; }
        public TimeSpan TimeLimit { get; set; }

        public Game(Streamer owner, int playerNumber, GameMode mode, TimeSpan timeLimit)
        {
            Owner = owner;
            PlayerNumber = playerNumber;
            Mode = mode;
            TimeLimit = timeLimit;
        }

        public Game(int id, Streamer owner, int playerNumber, GameMode mode, TimeSpan timeLimit)
            : this(owner, playerNumber, mode, timeLimit)
        {
            Id = id;
        }
    }
}
