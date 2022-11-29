using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Linq;
using WebApp.Models;

namespace WebApp.Services
{
    public class QuestionService
    {
        private static readonly string _con = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json", optional: false)
                                .Build().GetSection("ConnectionString").Value;

        private const string DATABASE_NAME = "ChatMayhem";


        public static bool InsertAnswers(List<Question<ViewerAnswer>> questions, string collection)
        {
            bool inserted = true;

            var client = new MongoClient(_con);

            var db = client.GetDatabase(DATABASE_NAME);

            var coll = db.GetCollection<Question<ViewerAnswer>>(collection);

            if (questions == null)
            {
                inserted = false;
            }

            coll.InsertMany(questions);

            return inserted;
        }

        public List<Answer>  GetAnswers(string questionPrompt, string collection)
        {
            return BsonSerializer.Deserialize<Question<ViewerAnswer>>(new MongoClient(_con).GetDatabase("ChatMayhem")
                .GetCollection<Question<ViewerAnswer>>(collection).Find(x => x.Prompt == questionPrompt)
                .Project("{_id: 0,prompt: 1, viewerAnswers: 1}").ToList().FirstOrDefault()).ViewerAnswers
                .GroupBy(answer => answer.Answer)
                .Select(group => new Answer(group.Count(), group.Key)).ToList();
        }
    }
}
