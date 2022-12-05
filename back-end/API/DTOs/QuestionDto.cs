using Data.ModelLayer;

namespace API.DTOs
{
    public class QuestionDto
    {

        public string text { get; set; }

        public List<Answer> answers { get; set; }
    }
}
