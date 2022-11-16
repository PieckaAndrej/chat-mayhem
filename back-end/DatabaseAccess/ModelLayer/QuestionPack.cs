using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelLayer
{
    internal class QuestionPack
    {
        public int id { get; set; }

        public string author { get; set; }

        public string name { get; set; }

        public string tags { get; set; }

        public string category { get; set; }

        public DateTime creationDate { get; set; }

        public QuestionPack (int id, string author, string name, string tag, string category, DateTime creationDate)
        {
            this.id = id;
            this.author = author;
            this.name = name;
            this.tag = tag;
            this.category = category;
            this.creationDate = creationDate;
        }
    }
}
