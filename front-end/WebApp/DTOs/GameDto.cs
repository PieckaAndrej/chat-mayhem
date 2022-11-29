namespace WebApp.DTOs
{
    public class GameDto
    {
        public double TimeLimitSeconds { get; set; }
        public string StreamerId { get; set; }
        public int ModeId { get; set; }
        public int QuestionPackId { get; set; }

        public GameDto() { }

        public GameDto(double timeLimitSeconds, string streamerId, int modeId, int questionPackId)
        {
            TimeLimitSeconds = timeLimitSeconds;
            StreamerId = streamerId;
            ModeId = modeId;
            QuestionPackId = questionPackId;
        }
    }
}
