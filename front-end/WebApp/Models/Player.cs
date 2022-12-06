namespace WebApp.Models
{
    public class Player
    {
        public int WrongAnswers { get; set; }
        public string Name { get; set; }
        public string ConnectionId { get; set; }
        public int Points { get; set; }

        public Player(string name, string connectionId)
        {
            Name = name;
            ConnectionId = connectionId;
            WrongAnswers = 0;
            Points = 0;
        }
    }
}
