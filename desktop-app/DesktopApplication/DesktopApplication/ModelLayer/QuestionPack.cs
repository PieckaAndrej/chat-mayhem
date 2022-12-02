using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.ModelLayer
{
    public class QuestionPack
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public string Name { get; set; }

        public Array Tags { get; set; }

        public string Category { get; set; }

        public DateTime CreationDate { get; set; }
        public List<Question> Questions { get; set; }

        public QuestionPack (int id, string author, string name, Array tag, string category, DateTime creationDate)
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
