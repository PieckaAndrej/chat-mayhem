namespace WebApp.Models
{
    public class Game
    {
        public double TimeLimitSeconds {  get; set; }
        public string StreamerId {  get; set; }
        public int ModeId {  get; set; }
        public int QuestionPackId { get; set; }

        public Game() { }

        public Game(double timeLimitSeconds, string streamerId, int modeId, int questionPackId)
        {
            TimeLimitSeconds = timeLimitSeconds;
            StreamerId = streamerId;
            ModeId = modeId;
            QuestionPackId = questionPackId;
        }
    }
}
