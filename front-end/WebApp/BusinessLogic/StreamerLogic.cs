using WebApp.DTOs;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.BusinessLogic
{
    public class StreamerLogic
    {
        private StreamerService _streamerService;

        public StreamerLogic()
        {
            _streamerService = new StreamerService();
        }

        public async Task<StreamerDto?> CreateStreamer(StreamerDto streamer)
        {
            return await _streamerService.CreateStreamer(streamer);
        }

        public async Task<Streamer?> RegisterCode(string code)
        {
            Streamer? returnStreamer = null;
            TwitchService twitchService = new TwitchService();

            TwitchToken? twitchToken = await twitchService.GetTwitchToken(code);

            if (twitchToken?.AccessToken != null)
            {
                StreamerDto? streamerDto = await CreateStreamer(
                    new StreamerDto(twitchToken.AccessToken ?? "", twitchToken.RefreshToken ?? ""));

                if (streamerDto?.AccessToken != null)
                {
                    TwitchValidate? validate = await twitchService.ValidateToken(
                        streamerDto.AccessToken);

                    if (validate?.Login != null)
                    {
                        returnStreamer = new Streamer(validate.Login,
                            twitchToken.AccessToken ?? "", validate.UserId);
                    }
                }
            }

            return returnStreamer;
        }
    }
}
