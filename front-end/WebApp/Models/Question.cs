using System;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApp.Models;

namespace WebApp.Models
{
    public class Question<T>
    {
        public string Prompt { get; set; }
        public List<T> ViewerAnswers { get; set; }

        public Question()
        {

        }

        public Question(string prompt, List<T> viewerAnswers)
        {
            Prompt = prompt;
            ViewerAnswers = viewerAnswers;
        }
    }
}
