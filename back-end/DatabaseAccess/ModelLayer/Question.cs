using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelLayer
{
    internal class Question
    {
        public int id { get; set; }

        public string text { get; set; }

        public Question(int id, string text)
        {
            this.id = id;
            this.text = text;
        }
    }
}
