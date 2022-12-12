using API.DTOs;
using API.Services;
using Data.ModelLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class QuestionPackController : Controller
    {

        private QuestionPackService _questionPackService;

        public QuestionPackController(IConfiguration inConfiguration)
        {
            ServiceInjector.Con = inConfiguration.GetConnectionString("ChatMayhem Connection") ?? "No connection string";

            string? connectionString = inConfiguration.GetConnectionString("ChatMayhem Connection");
            if (connectionString == null)
            {
                Console.WriteLine("Connection string is null");
            }

            _questionPackService = ServiceInjector.QuestionPackService;
        }

        [HttpGet]
        public ActionResult<List<QuestionPack>> GetAll()
        {
            return Ok(_questionPackService.GetAllQuestionPacks());
        }

        [HttpPost]
        public async Task<ActionResult<QuestionPack>> Post(QuestionPack questionPack)
        {
            //var questionPack = QuestionPackDto.Convert(inQuestionPack);
            var returnValue = await _questionPackService.InsertAsync(questionPack);

            if (returnValue == null)
            {
                return new StatusCodeResult(StatusCodes.Status204NoContent);
            }

            return Ok(returnValue);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<QuestionPack>> Put(int id, QuestionPack questionPack)
        {
            if (id != questionPack.Id)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var retQuestion = await _questionPackService.UpdateAsync(questionPack);
            return Ok(retQuestion);
        }

    }
}
