using API.DTOs;
using API.DTOs;
using API.Model;
using API.Services;
using Data.ModelLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class StreamerController : ControllerBase
    {
        private StreamerService _streamerService;

        public StreamerController(IConfiguration inConfiguration)
        {
            ServiceInjector.Con = inConfiguration.GetConnectionString("ChatMayhem Connection") ?? "No connection string";

            string? connectionString = inConfiguration.GetConnectionString("ChatMayhem Connection");
            if (connectionString == null)
            {
                Console.WriteLine("Connection string is null");
            }

            _streamerService = ServiceInjector.StreamerService;
        }

        [HttpPost]
        public async Task<ActionResult<StreamerDto>> Post(StreamerDto inStreamer)
        {
            Streamer? streamer = await StreamerDto.Convert(inStreamer);

            if (streamer == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            streamer = _streamerService.Update(streamer);

            return StreamerDto.Convert(streamer);
        }

        [HttpPost]
        [Route("token")]
        public async Task<ActionResult<string>> RefreshToken(string streamerId, string token)
        {
            if (String.IsNullOrWhiteSpace(streamerId))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            StreamerService streamerService = ServiceInjector.StreamerService;

            Streamer streamer = streamerService.Get(streamerId);

            if (streamer == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            if (streamer.AccessToken != token)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            TwitchRefresh? refresh = await streamerService.RefreshToken(
                streamer.RefreshToken);

            if (refresh?.AccessToken == null)
            {
                return StatusCode(StatusCodes.Status424FailedDependency);
            }

            streamer.AccessToken = refresh.AccessToken;

            streamerService.Update(streamer);

            return refresh.AccessToken;
        }
    }
}
