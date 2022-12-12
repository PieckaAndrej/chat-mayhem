using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.ModelLayer
{
    public class QuestionPack
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("author")]
        public string Author { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("tags")]
        public string[] Tags { get; set; }
        [JsonPropertyName("category")]
        public string Category { get; set; }
        [JsonPropertyName("creationDate")]
        public DateTime CreationDate { get; set; }
        [JsonPropertyName("questions")]
        public List<Question> Questions { get; set; }
        [JsonPropertyName("xmin")]
        public int xmin { get; set; }

        public QuestionPack() 
        { 
            Questions = new List<Question>();
        }

        public QuestionPack (string author, string name, string[] tag, string category, DateTime creationDate) : this()
        {
            Author = author;
            Name = name;
            Tags = tag;
            Category = category;
            CreationDate = creationDate;
        }
    }
}
