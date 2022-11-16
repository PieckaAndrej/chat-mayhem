namespace WebApp.Models
{
    public class Games
    {
        public List<Game> games { get; set; }

        public Games()
        {
            games = new List<Game>();
            games.Add(new Game(15, "1", 1, 1));
            games.Add(new Game(15, "1", 1, 1));
            games.Add(new Game(15, "1", 1, 1));
            games.Add(new Game(15, "1", 1, 1));
            games.Add(new Game(15, "1", 1, 1));
        }
    }
}
