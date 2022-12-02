using Data.ModelLayer;
using Newtonsoft.Json;
using PersonServiceClientDesktop.Servicelayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace DesktopApplication.ServiceLayer
{
    public class AnswerAccess : IAnswerAccess
    {
        readonly IServiceConnection _answerService;
        readonly String _serviceBaseUrl = "https://localhost:7200/api/Question/";
        public AnswerAccess()
        {
            _answerService = new ServiceConnection(_serviceBaseUrl);
        }

        public async Task<int> CreateAnswers(List<Answer> answers)
        {
            int answersInserted = 0;

            List<string> JsonAnswers = new List<string>();

            foreach (Answer answer in answers)
            {
                answersInserted++;
                
                JsonAnswers.Add(JsonConvert.SerializeObject(answer));
            }

            var stringContent = new StringContent(JsonAnswers.ToString(), UnicodeEncoding.UTF8, "application/json");

            await _answerService.Put(stringContent);

            return answersInserted;
        }
    }
}
