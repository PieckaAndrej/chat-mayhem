using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DesktopApplication.ModelLayer
{
    public class QuestionPack
    {
        [Timestamp]
        public uint RowVersion { get; set; }
        public int Id { get; set; }

        public string Author { get; set; }

        public string Name { get; set; }

        public List<string> Tags { get; set; }

        public string Category { get; set; }

        public DateTime CreationDate { get; set; }
        public List<Question> Questions { get; set; }

        public QuestionPack (List<Question> questions, string author, string name, List<string> tag, string category, DateTime creationDate)
        {
            Questions = questions;
            Author = author;
            Name = name;
            Tags = tag;
            Category = category;
            CreationDate = creationDate;
        }

        public QuestionPack(int id, uint rowVersion, List<Question> questions, string author, string name, List<string> tag, string category, DateTime creationDate) : this(questions, author, name, tag, category, creationDate)
        {
            Id = id;
            RowVersion = rowVersion;
        }

        public QuestionPack()
        {

        }
    }
}
