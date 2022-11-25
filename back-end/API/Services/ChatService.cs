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

        public List<string> GetAnswers()
        {
            return _chatAccess.GetAnswers();
        }
    }
}
