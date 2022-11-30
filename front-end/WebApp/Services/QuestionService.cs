using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using RestSharp;
using System.IO;
using System.Linq;
using WebApp.DTOs;
using WebApp.Models;

namespace WebApp.Services
{
    public class QuestionService
    {
        private static readonly string _con = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json", optional: false)
                                .Build().GetSection("ConnectionString").Value;

        private const string DATABASE_NAME = "ChatMayhem";


        public static bool InsertAnswers(Question<ViewerAnswer> question, string collection)
        {
            bool inserted = true;

            var client = new MongoClient(_con);

            var db = client.GetDatabase(DATABASE_NAME);

            if(!db.ListCollectionNames().ToString().Contains(collection))
            {
                CreateCollection(collection);
            }

            var coll = db.GetCollection<Question<ViewerAnswer>>(collection);

            if (question == null)
            {
                inserted = false;
            }

            coll.InsertOne(question);

            return inserted;
        }

        public static void CreateCollection(string collection)
        {
            var client = new MongoClient(_con);

            var db = client.GetDatabase(DATABASE_NAME);

            db.CreateCollection(collection);
        }

        public static List<Answer> GetAnswers(string questionPrompt, string collection)
        {
            var client = new MongoClient(_con);

            var db = client.GetDatabase("ChatMayhem");

            var coll = db.GetCollection<Question<ViewerAnswer>>(collection);

            List<ViewerAnswer> viewerAnswers = BsonSerializer.Deserialize<Question<ViewerAnswer>>(
                                                   coll.Find(x => x.Prompt == questionPrompt)
                                                   .Project("{_id: 0,prompt: 1, viewerAnswers: 1}")
                                                   .ToList().FirstOrDefault()
                                               ).ViewerAnswers;

            List<Answer> answers = viewerAnswers.GroupBy(viewerAnswer => viewerAnswer.Answer)
                                                .Select(group => new Answer(
                                                    group.Count(),
                                                    group.Key)
                                                ).ToList();

            return answers;
        }

        public async Task<List<AnswerDto>> PostAnswers(string questionPrompt, string collection)
        {
            List<Answer> answers = new List<Answer>();

            List<AnswerDto> answersDto = new List<AnswerDto>();

            Question<ViewerAnswer> question = new Question<ViewerAnswer>();

            answers = GetAnswers(questionPrompt, collection);

            RestClient restClient = new RestClient("https://localhost:7200/");

            var client = new MongoClient(_con);

            var db = client.GetDatabase("ChatMayhem");

            var coll = db.GetCollection<Question<ViewerAnswer>>(collection);

            question = BsonSerializer.Deserialize<Question<ViewerAnswer>>(
                                                   coll.Find(x => x.Prompt == questionPrompt)
                                                   .Project("{_id: 0,prompt: 1, viewerAnswers: 1}")
                                                   .ToList().FirstOrDefault());

            foreach (var answer in answers)
            {
                AnswerDto answerDto = new AnswerDto(answer.Points, answer.Text, question.QuestionId);

                answersDto.Add(answerDto);
            }

            RestRequest restRequest = new RestRequest("api/Question").AddJsonBody(answersDto);

            var response = await restClient.ExecutePostAsync<AnswerDto>(restRequest);

            return answersDto;
        }
    }
}
