using API.Services;
using Data.ModelLayer;

namespace API.DTOs
{
    public class GameDto
    {
        public int Id { get; set; }
        public string StreamerId { get; set; }
        public int ModeId { get; set; }
        public int TimeLimitSeconds { get; set; }
        public int QuestionPackId { get; set; }  

        public GameDto() { }

        public GameDto(string streamer, int mode, int timeLimit, int questionPackId)
        {
            StreamerId = streamer;
            ModeId = mode;
            TimeLimitSeconds = timeLimit;
            QuestionPackId = questionPackId;
        }

        public GameDto(string streamer, int mode, int timeLimit, int questionPackId, int id)
            : this(streamer, mode, timeLimit, questionPackId)
        {
            Id = id;
        }

        public static GameDto Convert(Game game)
        {
            return new GameDto(game.Streamer.Id, game.Mode.Id, game.TimeLimit, game.QuestionPack.Id, game.Id);
        }

        public static Game Convert(GameDto gameDto)
        {
            Streamer streamer = ServiceInjector.StreamerService.Get(gameDto.StreamerId);
            GameMode mode = ServiceInjector.GameModeService.Get(gameDto.ModeId);
            QuestionPack questionPack = ServiceInjector.QuestionPackService.GetQuestionPackById(gameDto.QuestionPackId);

            return new Game(streamer, mode, gameDto.TimeLimitSeconds, questionPack, gameDto.Id);
        }
    }
}
