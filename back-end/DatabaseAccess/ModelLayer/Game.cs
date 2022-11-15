using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.ModelLayer
{
    public class Game
    {
        public int Id { get; set; }
        public Streamer Streamer { get; set; }
        public int PlayerNumber { get; set; }
        public GameMode Mode { get; set; }
        public TimeSpan TimeLimit { get; set; }

        public Game(Streamer streamer, int playerNumber, GameMode mode, TimeSpan timeLimit)
        {
            Streamer = streamer;
            PlayerNumber = playerNumber;
            Mode = mode;
            TimeLimit = timeLimit;
        }

        public Game(Streamer streamer, int playerNumber, GameMode mode, TimeSpan timeLimit, int id)
            : this(streamer, playerNumber, mode, timeLimit)
        {
            Id = id;
        }
    }
}
