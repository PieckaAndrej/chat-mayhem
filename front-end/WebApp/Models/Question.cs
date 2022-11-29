using System;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApp.Models;

namespace WebApp.Models
{
    public class Question
    {
        public string prompt { get; set; }
        public List<ViewerAnswer> viewerAnswers { get; set; }

        public Question(string prompt, List<ViewerAnswer> viewerAnswers)
        {
            this.prompt = prompt;
            this.viewerAnswers = viewerAnswers;
        }
    }
}
