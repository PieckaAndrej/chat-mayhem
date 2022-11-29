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

        public static string con = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json", optional: false)
                                .Build().GetSection("ConnectionString").Value;

        public Question()
        {

        }

        public bool InsertAnswers(List<Question> questions)
        {
            bool inserted = true;

            var client = new MongoClient(con);

            var db = client.GetDatabase("ChatMayhem");

            var coll = db.GetCollection<Question>("Viewer'sAnswers");

            if(questions == null)
            {
                inserted = false; 
            }

            coll.InsertMany(questions);

            return inserted;
        }
        

        public List<Question> GetAnswers(string questionPrompt)
        {
            List<Question> answers = new List<Question>();

            var client = new MongoClient(con);

            var db = client.GetDatabase("ChatMayhem");

            var coll = db.GetCollection<Question>("Viewer'sAnswers");

            answers = coll.Find(x => x.prompt == questionPrompt).ToList();

            return answers;
        }
    }
}
