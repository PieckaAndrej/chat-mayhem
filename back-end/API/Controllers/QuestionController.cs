using API.DTOs;
using API.Services;
using Data.ModelLayer;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private AnswerService _answerService;

        public QuestionController(IConfiguration inConfiguration)
        {
            ServiceInjector.Con = inConfiguration.GetConnectionString("ChatMayhem Connection") ?? "No connection string";

            string? connectionString = inConfiguration.GetConnectionString("ChatMayhem Connection");
            if (connectionString == null)
            {
                Console.WriteLine("Connection string is null");
            }

            _answerService = ServiceInjector.AnswerService;
        }

        [HttpPost]
        public ActionResult<List<Answer>> Post(List<Answer> answers)
        {
            List<Answer> returnAnswers = ServiceInjector.AnswerService.CreateAnswer(answers);

            if (returnAnswers == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return returnAnswers;
        }
    }
}
