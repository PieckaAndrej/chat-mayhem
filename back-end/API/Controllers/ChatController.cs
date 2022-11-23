using API.DTOs;
using API.Services;
using Data.DatabaseLayer;
using Data.ModelLayer;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController
    {
        private ChatService chatService;

        public ChatController(IConfiguration inConfiguration)
        {
            ServiceInjector.Con = inConfiguration.GetConnectionString("ChatMayhem Connection") ?? "No connection string";

            string? connectionString = inConfiguration.GetConnectionString("ChatMayhem Connection");
            if (connectionString == null)
            {
                Console.WriteLine("Connection string is null");
            }

            chatService = new ChatService(new ChatAccess(connectionString ?? "No connection string"));
        }

        [HttpGet]
        public ActionResult<string> GetAnswer(string answer)
        {
            string viewerAnswer = ServiceInjector.ChatService.GetAnswer(answer);

            if (viewerAnswer.Equals("Invalid answer"))
            {
                return "Wrong";
            }
            else
            {
                return viewerAnswer;
            }
        }

    }
}
