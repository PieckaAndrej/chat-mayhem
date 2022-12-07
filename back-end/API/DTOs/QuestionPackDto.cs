using API.Model;
using API.Services;
using Data.ModelLayer;

namespace API.DTOs
{
    public class QuestionPackDto
    {
        public string Author { get; set; }
        public string Name { get; set; }
        public List<string> Tags { get; set; }
        public string Category { get; set; }
        public DateTime CreationDate { get; set; }
        public List<Question> Questions { get; set; }
        public QuestionPackDto(string author, string name, List<string> tag, string category, DateTime creationDate)
        {
            Author = author;
            Name = name;
            Tags = tag;
            Category = category;
            CreationDate = creationDate;
        }
        public static QuestionPackDto Convert(QuestionPack questionPack)
        {
            return new QuestionPackDto(questionPack.Author, questionPack.Name, questionPack.Tags.ToList(), questionPack.Category, questionPack.CreationDate);
        }
        public static QuestionPack Convert(QuestionPackDto questionPack)
        {
            return new QuestionPack(questionPack.Author, questionPack.Name, questionPack.Tags.ToArray(), questionPack.Category, questionPack.CreationDate);
        }

    }
}
