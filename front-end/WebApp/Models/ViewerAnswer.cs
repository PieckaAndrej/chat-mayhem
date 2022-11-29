namespace WebApp.Models
{
    public class ViewerAnswer
    {
        public string username { get; set; }
        
        public string answer { get; set; }

        public ViewerAnswer()
        {

        }

        public ViewerAnswer(string username, string answer)
        {
            this.username = username;
            this.answer = answer;
        }
    }
}
