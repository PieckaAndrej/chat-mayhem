namespace WebApp.Models
{
    public class ViewerAnswer
    {
        public string Username { get; set; }
        public string Answer { get; set; }

        public ViewerAnswer() { }

        public ViewerAnswer(string username, string answer)
        {
            Username = username;
            Answer = answer;
        }
    }
}
