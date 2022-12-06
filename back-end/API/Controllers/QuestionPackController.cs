using API.DTOs;
using API.Services;
using Data.ModelLayer;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            return _questionPackService.GetAllQuestionPacks();
        }

    }
}