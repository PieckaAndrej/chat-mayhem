using API.Services;
using Data.ModelLayer;

namespace API.DTOs
{
    public class GameDto
    {
        public int Id { get; set; }
        public int StreamerId { get; set; }
        public int PlayerNumber { get; set; }
        public int ModeId { get; set; }
        public double TimeLimitSeconds { get; set; }

        public GameDto() { }

        public GameDto(int streamer, int playerNumber, int mode, double timeLimit)
        {
            StreamerId = streamer;
            PlayerNumber = playerNumber;
            ModeId = mode;
            TimeLimitSeconds = timeLimit;
        }

        public GameDto(int streamer, int playerNumber, int mode, double timeLimit, int id)
            : this(streamer, playerNumber, mode, timeLimit)
        {
            Id = id;
        }

        public static GameDto Convert(Game game)
        {
            return new GameDto(game.Streamer.Id, game.PlayerNumber, game.Mode.Id, game.TimeLimit.TotalSeconds, game.Id);
        }

        public static Game Convert(GameDto gameDto)
        {
            Streamer streamer = ServiceInjector.streamService.Get(gameDto.StreamerId);
            GameMode mode = ServiceInjector.gameModeService.Get(gameDto.ModeId);

            return new Game(streamer, gameDto.PlayerNumber, mode, TimeSpan.FromSeconds(gameDto.TimeLimitSeconds), gameDto.Id);
        }
    }
}
