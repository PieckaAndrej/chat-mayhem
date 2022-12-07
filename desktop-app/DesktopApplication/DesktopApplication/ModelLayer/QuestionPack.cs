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

        public Array Tags { get; set; }

        public string Category { get; set; }

        public DateTime CreationDate { get; set; }
        public List<Question> Questions { get; set; }

        public QuestionPack (string author, string name, Array tag, string category, DateTime creationDate)
        {
            Author = author;
            Name = name;
            Tags = tag;
            Category = category;
            CreationDate = creationDate;
        }

        public QuestionPack(int id, string author, string name, Array tag, string category, DateTime creationDate) : this(author, name, tag, category, creationDate)
        {
            Id = id;
        }

        public QuestionPack()
        {

        }
    }
}
