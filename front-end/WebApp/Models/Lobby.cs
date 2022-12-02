using Data.ModelLayer;
using WebApp.Services;

namespace WebApp.Models
{
    public class Lobby
    {
        public string GroupName { get; set; }
        public List<string>? Streamers { get; set; }
        public MessageHandlerService? MessageHandler { get; set; }
        public QuestionPack? QuestionPack { get; set; }
    }
}
