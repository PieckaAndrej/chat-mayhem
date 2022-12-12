using Data.ModelLayer;
using WebApp.Services;

namespace WebApp.Models
{
    public class Lobby
    {
        public string GroupName { get; set; }
        public List<Player> Players { get; set; }
        public MessageHandlerService? MessageHandler { get; set; }
        public QuestionPack QuestionPack { get; set; }
        public int currentQuestionIndex;
        public Question<Answer>[] Answers { get; set; }

        public Lobby(string groupName, QuestionPack questionPack, MessageHandlerService messageHandler)
        {
            GroupName = groupName;
            QuestionPack = questionPack;
            MessageHandler = messageHandler;

            Players = new List<Player>();
            Answers = new Question<Answer>[questionPack.Questions?.Count ?? 0];

            for (int i = 0; i < QuestionPack.Questions?.Count; i++)
            {
                Answers[i] = new Question<Answer>() { Prompt = QuestionPack.Questions[i].Prompt };
            }
        }

        public bool NextQuestion()
        {
            bool retVal = false;

            if (QuestionPack?.Questions?.Count - 1 > currentQuestionIndex)
            {
                currentQuestionIndex++;
                Players.ForEach(p => p.WrongAnswers = 0);
                retVal= true;
            }

            return retVal;
        }

        public Question<ViewerAnswer>? GetCurrentQuestion()
        {
            return QuestionPack?.Questions?[currentQuestionIndex];
        }
    }
}
