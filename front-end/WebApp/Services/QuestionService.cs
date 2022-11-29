using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Linq;
using WebApp.Models;

namespace WebApp.Services
{
    public class QuestionService
    {
        public static string con = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json", optional: false)
                                .Build().GetSection("ConnectionString").Value;

        public bool InsertAnswers(List<Question> questions, string collection)
        {
            bool inserted = true;

            var client = new MongoClient(con);

            var db = client.GetDatabase("ChatMayhem");

            var coll = db.GetCollection<Question>(collection);

            if (questions == null)
            {
                inserted = false;
            }

            coll.InsertMany(questions);

            return inserted;
        }

        public List<Answer>  GetAnswers(string questionPrompt, string collection)
        {

            return BsonSerializer.Deserialize<Question>(new MongoClient(con).GetDatabase("ChatMayhem")
                .GetCollection<Question>(collection).Find(x => x.prompt == questionPrompt)
                .Project("{_id: 0,prompt: 1, viewerAnswers: 1}").ToList().FirstOrDefault()).viewerAnswers
                .GroupBy(answer => answer.answer)
                .Select(group => new Answer(group.Count(), group.Key)).ToList();
        }
    }
}
