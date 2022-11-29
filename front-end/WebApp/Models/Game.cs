using Data.ModelLayer;

namespace WebApp.Models
{
    public class Game
    {
        public TimeSpan TimeLimit { get; set; }
        public Streamer Streamer { get; set; }
        public GameMode Mode { get; set; }
        public QuestionPack QuestionPack { get; set; }

        public Game() { }

        public Game(Streamer streamer, GameMode mode, TimeSpan timeLimit, QuestionPack questionPack)
        {
            Streamer = streamer;
            Mode = mode;
            TimeLimit = timeLimit;
            QuestionPack = questionPack;
        }
    }
}
