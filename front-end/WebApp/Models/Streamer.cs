namespace WebApp.Models
{
    public class Streamer
    {
        public string Name { get; set; }
        public string AccessToken { get; set; }
        public int UserId { get; set; }

        public Streamer(string name, string accessToken, int userId)
        {
            Name = name;
            AccessToken = accessToken;
            UserId = userId;
        }
    }
}
