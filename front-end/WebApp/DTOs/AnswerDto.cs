namespace WebApp.DTOs
{
    public class AnswerDto
    {
        public int answerCount { get; set; }

        public string text { get; set; }

        public int questionId { get; set; }

        public AnswerDto(int answerCount, string text, int questionId)
        {
            this.answerCount = answerCount;
            this.text = text;
            this.questionId = questionId;
        }
    }
}
