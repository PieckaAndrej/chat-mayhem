using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelLayer
{
    public class QuestionPack
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public string Name { get; set; }

        public string Tag { get; set; }

        public string Category { get; set; }

        public DateTime CreationDate { get; set; }

        public QuestionPack (int id, string author, string name, string tag, string category, DateTime creationDate)
        {
            this.Id = id;
            this.Author = author;
            this.Name = name;
            this.Tag = tag;
            this.Category = category;
            this.CreationDate = creationDate;
        }
    }
}
