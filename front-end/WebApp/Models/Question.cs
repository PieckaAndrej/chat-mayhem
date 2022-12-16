using System;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WebApp.Models
{
    public class Question<T>
    {
        [JsonPropertyName("text")]
        public string Prompt { get; set; }
        [JsonPropertyName("answers")]
        public List<T> Answers { get; set; }
        [JsonPropertyName("id")]
        public int QuestionId { get; set; }

        public Question() 
        {
            Answers = new List<T>();
        }

        public Question(string prompt, int questionId):this()
        {
            Prompt = prompt;
            QuestionId = questionId;
        }

        public Question(string prompt, List<T> answers, int questionId)
        {
            Prompt = prompt;
            Answers = answers;
            QuestionId = questionId;
        }
    }
}
