using Data.ModelLayer;
using WebApp.Services;

namespace WebApp.Models
{
    public class Lobby
    {
        public string GroupName { get; set; }
        public List<string> Players { get; set; }
        public MessageHandlerService? MessageHandler { get; set; }
        public QuestionPack QuestionPack { get; set; }
        public int currentQuestionIndex;
        public Question<Answer>[] Answers { get; set; }

        public Lobby(string groupName, QuestionPack questionPack, MessageHandlerService messageHandler)
        {
            GroupName = groupName;
            QuestionPack = questionPack;
            MessageHandler = messageHandler;

            Players = new List<string>();
            Answers = new Question<Answer>[questionPack.Questions?.Count ?? 0];
        }

        public bool NextQuestion()
        {
            bool retVal = false;

            if (QuestionPack?.Questions?.Count - 1 > currentQuestionIndex)
            {
                currentQuestionIndex++;
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
