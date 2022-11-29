using MongoDB.Driver;
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

        public List<Question> GetAnswers(string questionPrompt, string collection)
        {
            List<Question> answers = new List<Question>();

            var client = new MongoClient(con);

            var db = client.GetDatabase("ChatMayhem");

            var coll = db.GetCollection<Question>(collection);

            answers = coll.Find(x => x.prompt == questionPrompt).ToList();

            return answers;
        }
    }
}
