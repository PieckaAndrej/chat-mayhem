namespace WebApp.Models
{
    public class TwitchValidate
    {
        public string? clientId { get; set; }
        public string? login { get; set; }
        public List<string>? scopes { get; set; }
        public string? userId { get; set; }
        public int expiresIn { get; set; }
    }
}
