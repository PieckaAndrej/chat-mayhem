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

        [HttpPut]
        [Route("answers/{questionId}")]
        public ActionResult<Question> Put(Question question, int questionId)
        {
            Console.WriteLine("called");
            if (question.id != questionId)
            {
                return UnprocessableEntity($"Passed in question's id ({question.id})" +
                    $" and the question id which was searched for ({questionId}) not matching.");
            }

            Question returnQuestion = question;

            if (ServiceInjector.QuestionService.GetQuestionById(question.id) == null)
            {
                return NotFound("No question with matching id: " + question.id);
            }
            List<Answer>? returnAnswers = ServiceInjector.AnswerService.InsertAnswers(question);

            if (returnAnswers == null)
            {
                
            }

            returnQuestion.answers = returnAnswers;

            return returnQuestion;
        }

        [HttpGet]
        public ActionResult<List<Question>> GetPrompts()
        {
            List<Question> questions = ServiceInjector.QuestionService.GetQuestions();

            if (questions == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            else
            {
                return Ok(questions);
            }
        }

        //[HttpPut]
        //[Route("answers/{questionId}")]
        //public ActionResult Putss()
        //{
        //    Console.WriteLine("yo");
        //    return Ok();
        //}
    }
}
