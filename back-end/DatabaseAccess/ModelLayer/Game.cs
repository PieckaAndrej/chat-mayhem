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
        public GameMode Mode { get; set; }
        [JsonConverter(typeof(TimeSpan))]
        public TimeSpan TimeLimit { get; set; }
        public QuestionPack QuestionPack { get; set; }

        public Game() { }
        public Game(Streamer streamer, GameMode mode, TimeSpan timeLimit, QuestionPack questionPack)
        {
            Streamer = streamer;
            Mode = mode;
            TimeLimit = timeLimit;
            QuestionPack = questionPack;
        }

        public Game(Streamer streamer, GameMode mode, TimeSpan timeLimit,
            QuestionPack questionPack, int id)
            : this(streamer, mode, timeLimit, questionPack)
        {
            Id = id;
        }
    }
}
