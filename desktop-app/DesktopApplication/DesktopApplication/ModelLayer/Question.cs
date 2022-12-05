using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DesktopApplication.ModelLayer
{
    public class Question
    {
        [JsonPropertyName("id")]
        public int id { get; set; }

        [JsonPropertyName("text")]
        public string text { get; set; }

        [JsonPropertyName("answers")]
        public List<Answer>? answers { get; set; }

        public Question(int id, string text)
        {
            this.id = id;
            this.text = text;
        }

        [JsonConstructor]
        public Question(int id, string text, List<Answer> answers) : this(id, text)
        {
            this.answers = answers;
        }
    }
}
