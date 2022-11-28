namespace WebApp.Models
{
    public class Answer
    {
        public int Points { get; set; }
        public string? Text { get; set; }

        public Answer() { }

        public Answer(int points, string text)
        {
            Points = points;
            Text = text;
        }
    }
}
