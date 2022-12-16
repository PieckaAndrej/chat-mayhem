using Data.ModelLayer;
using System.Text.Json.Serialization;
using WebApp.Services;

namespace WebApp.Models
{
    public class Lobby
    {
        public string GroupName { get; set; }
        public List<Player> Players { get; set; }
        [JsonIgnore]
        public MessageHandlerService? MessageHandler { get; set; }
        public int currentQuestionIndex { get; set; }
        public Question<Answer>[] Answers { get; set; }
        public Game Game { get; set; }
        public enum GAME_STATE { LOBBY, LISTENING, ANSWERING, FINISHED }
        public GAME_STATE GameState { get; set; }

        public Lobby(string groupName, Game game, MessageHandlerService messageHandler)
        {
            GroupName = groupName;
            Game = game;
            MessageHandler = messageHandler;
            GameState = GAME_STATE.LOBBY;

            Players = new List<Player>();
            Answers = new Question<Answer>[Game.QuestionPack.Questions?.Count ?? 0];

            for (int i = 0; i < Game.QuestionPack.Questions?.Count; i++)
            {
                Answers[i] = new Question<Answer>() { Prompt = Game.QuestionPack.Questions[i].Prompt };
            }
        }

        public bool NextQuestion()
        {
            bool retVal = false;

            if (Game.QuestionPack?.Questions?.Count - 1 > currentQuestionIndex)
            {
                currentQuestionIndex++;
                Players.ForEach(p => p.WrongAnswers = 0);
                retVal= true;
            }

            return retVal;
        }

        public Question<ViewerAnswer>? GetCurrentQuestion()
        {
            return Game.QuestionPack?.Questions?[currentQuestionIndex];
        }
    }
}
