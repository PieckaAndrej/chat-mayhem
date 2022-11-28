using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelLayer
{
    public class Answer
    {
        public int answerCount { get; set; }

        public string text { get; set; }

        public int questionId { get; set; }

        public Answer(int answerCount, string text)
        {
            this.answerCount = answerCount;
            this.text = text;
        }

        public Answer(int answerCount, string text, int questionId) : this(answerCount, text)
        {
            this.questionId = questionId;
        }
    }
}
