using System;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApp.Models;

namespace WebApp.Models
{
    public class Question
    {
        public string prompt { get; set; }
        public List<ViewerAnswer> viewerAnswers { get; set; }

        public Question()
        {

        }

        public bool InsertAnswers(List<Question> questions)
        {
            bool inserted = true;

            var uri = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json", optional: false)
                                .Build().GetSection("ConnectionString").Value;

            var client = new MongoClient(uri);

            var db = client.GetDatabase("ChatMayhem");

            var coll = db.GetCollection<Question>("Viewer'sAnswers");

            if(questions == null)
            {
                inserted = false; 
            }

            coll.InsertMany(questions);

            return inserted;
        }
        
    }
}
