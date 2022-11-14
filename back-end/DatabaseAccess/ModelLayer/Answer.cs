using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelLayer
{
    internal class Answer
    {
        public int userId { get; set; }

        public string text { get; set; }

        public Answer(int userId, string text)
        {
            this.userId = userId;
            this.text = text;
        }
    }
}
