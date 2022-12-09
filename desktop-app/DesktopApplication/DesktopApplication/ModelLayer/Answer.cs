using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DesktopApplication.ModelLayer
{
    public class Answer
    {
        [JsonPropertyName("answerCount")]
        public int answerCount { get; set; }

        [JsonPropertyName("text")]
        public string text { get; set; }

        public Answer(int answerCount, string text)
        {
            this.answerCount = answerCount;
            this.text = text;
        }
    }
}
