using API.Services;
using Data.ModelLayer;

namespace API.DTOs
{
    public class GameDto
    {
        public int Id { get; set; }
        public int StreamerId { get; set; }
        public int ModeId { get; set; }
        public double TimeLimitSeconds { get; set; }
        public int QuestionPackId { get; set; }  

        public GameDto() { }

        public GameDto(int streamer, int mode, double timeLimit, int questionPackId)
        {
            StreamerId = streamer;
            ModeId = mode;
            TimeLimitSeconds = timeLimit;
            QuestionPackId = questionPackId;
        }

        public GameDto(int streamer, int mode, double timeLimit, int questionPackId, int id)
            : this(streamer, mode, timeLimit, questionPackId)
        {
            Id = id;
        }

        public static GameDto Convert(Game game)
        {
            return new GameDto(game.Streamer.Id, game.Mode.Id, game.TimeLimit.TotalSeconds, game.QuestionPack.Id, game.Id);
        }

        public static Game Convert(GameDto gameDto)
        {
            Streamer streamer = ServiceInjector.streamService.Get(gameDto.StreamerId);
            GameMode mode = ServiceInjector.gameModeService.Get(gameDto.ModeId);
            QuestionPack questionPack = ServiceInjector.questionPackService.Get(gameDto.QuestionPackId);

            return new Game(streamer, mode, TimeSpan.FromSeconds(gameDto.TimeLimitSeconds), questionPack, gameDto.Id);
        }
    }
}
