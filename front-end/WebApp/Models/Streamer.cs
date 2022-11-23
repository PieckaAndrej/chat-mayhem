namespace WebApp.Models
{
    public class Streamer
    {
        public string Name { get; set; }
        public string AccessToken { get; set; }
        public string UserId { get; set; }

        public Streamer(string name, string accessToken, string userId)
        {
            Name = name;
            AccessToken = accessToken;
            UserId = userId;
        }
    }
}
