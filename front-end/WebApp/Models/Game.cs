using Data.ModelLayer;

namespace WebApp.Models
{
    public class Game
    {
        public int Id { get; set; }
        public int TimeLimit { get; set; }
        public Streamer Streamer { get; set; }
        public GameMode Mode { get; set; }
        public QuestionPack QuestionPack { get; set; }

        public Game() { }

        public Game(Streamer streamer, GameMode mode, int timeLimit, QuestionPack questionPack)
        {
            Streamer = streamer;
            Mode = mode;
            TimeLimit = timeLimit;
            QuestionPack = questionPack;
        }
    }
}
