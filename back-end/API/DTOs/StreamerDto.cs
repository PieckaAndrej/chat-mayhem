using API.Model;
using API.Services;
using Data.ModelLayer;

namespace API.DTOs
{
    public class StreamerDto
    {
        public string AccessToken { get; set; }
        //TODO maybe remove
        public string RefreshToken { get; set; }

        public StreamerDto() { }

        public StreamerDto(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public static StreamerDto Convert(Streamer streamer)
        {
            return new StreamerDto(streamer.AccessToken, streamer.RefreshToken);
        }

        public static async Task<Streamer?> Convert(StreamerDto streamerDto)
        {
            TwitchValidate? validation = await ServiceInjector.StreamerService.ValidateToken(streamerDto.AccessToken);

            if (String.IsNullOrWhiteSpace(validation?.ClientId))
            {
                return null;
            }

            return new Streamer(streamerDto.AccessToken, validation.UserId, streamerDto.RefreshToken);
        }
    }
}
