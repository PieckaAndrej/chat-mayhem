using Data.DatabaseLayer;
using Data.ModelLayer;

namespace API.Services
{
    public class ChatService
    {
        private IChatAccess _chatAccess;

        public ChatService(IChatAccess chatAccess)
        {
            _chatAccess = chatAccess;
        }

        public string GetAnswer(string answer)
        {
            return _chatAccess.CheckAnswer(answer);
        }
    }
}
