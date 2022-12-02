using API.Model;
using API.Services;
using Data.ModelLayer;

namespace API.DTOs
{
    public class QuestionPackDto
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Name { get; set; }
        public Array Tags { get; set; }
        public string Category { get; set; }
        public DateTime CreationDate { get; set; }
        public List<Question> Questions { get; set; }
        public QuestionPackDto(int id, string author, string name, Array tag, string category, DateTime creationDate)
        {
            Id = id;
            Author = author;
            Name = name;
            Tags = tag;
            Category = category;
            CreationDate = creationDate;
        }
    }
}
