using API.DTOs;
using API.Services;
using Data.ModelLayer;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

            streamer = _streamerService.Add(streamer);

            return StreamerDto.Convert(streamer);
        }
    }
}
