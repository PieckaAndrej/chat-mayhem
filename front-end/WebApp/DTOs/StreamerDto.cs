namespace WebApp.DTOs
{
    public class StreamerDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public StreamerDto() { }

        public StreamerDto(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
