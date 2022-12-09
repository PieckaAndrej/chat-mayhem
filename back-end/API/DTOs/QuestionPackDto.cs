using API.Model;
using API.Services;
using Data.ModelLayer;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class QuestionPackDto
    {
        [JsonPropertyName("author")]
        public string Author { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }
        [JsonPropertyName("category")]
        public string Category { get; set; }
        [JsonPropertyName("creationDate")]
        public DateTime CreationDate { get; set; }
        [JsonPropertyName("questions")]
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
