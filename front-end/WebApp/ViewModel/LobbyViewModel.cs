using WebApp.Models;

namespace WebApp.ViewModel
{
    public class LobbyViewModel
    {
        public Game? Game { get; set; }
        public Question<Answer>? Question { get; set; }
    }
}
