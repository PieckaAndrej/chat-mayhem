using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using RestSharp;
using System.Collections;
using System.Diagnostics.Metrics;
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


        public static async Task<long> InsertAnswers(ViewerAnswer viewerAnswer, string collectionName, Question<ViewerAnswer> question)
        {
            long result = 0;

            var client = new MongoClient(_con);

            var db = client.GetDatabase(DATABASE_NAME);

            if (!db.ListCollectionNames().ToList().Contains(collectionName))
            {
                db.CreateCollection(collectionName);
            }

            var collection = db.GetCollection<Question<ViewerAnswer>>(collectionName);

            if (question == null || viewerAnswer == null)
            {
                return result;
            }

            if (collection.Find(q => q.Prompt.Equals(question.Prompt)).CountDocuments() == 0)
            {
                var q = new Question<ViewerAnswer>(question.Prompt,new List<ViewerAnswer>(), question.QuestionId);
                q.ViewerAnswers.Add(viewerAnswer);
                await collection.InsertOneAsync(q);
                result = 1;
            } 
            else
            {
                MongoDB.Driver.FilterDefinition<Question<ViewerAnswer>> filter;
                MongoDB.Driver.UpdateDefinition<Question<ViewerAnswer>> update;

                if (!GetQuestion(question.Prompt,collection).ViewerAnswers
                                .Select<ViewerAnswer, string>(a => a.Username).ToList()
                                .Contains(viewerAnswer.Username))
                {
                    filter = Builders<Question<ViewerAnswer>>.Filter.Eq(q => q.Prompt, question.Prompt);
                    update = Builders<Question<ViewerAnswer>>.Update.AddToSet(s => s.ViewerAnswers, viewerAnswer);
                }
                else
                {
                    filter = Builders<Question<ViewerAnswer>>.Filter.Where(q => q.Prompt == question.Prompt 
                                            && q.ViewerAnswers.Any(v => v.Username.Equals(viewerAnswer.Username)));
                    update = Builders<Question<ViewerAnswer>>.Update.Set(s => s.ViewerAnswers[-1], viewerAnswer);
                }
                var result1 = await collection.UpdateOneAsync(filter, update);
                result = result1.ModifiedCount;
            }

            return result;
        }

        public static Question<ViewerAnswer> GetQuestion(string questionPrompt, IMongoCollection<Question<ViewerAnswer>> collection)
        {
            return BsonSerializer.Deserialize<Question<ViewerAnswer>>(
                                                   collection.Find(x => x.Prompt == questionPrompt)
                                                   .Project("{_id: 0, Prompt: 1, ViewerAnswers: 1, QuestionId: 1}")
                                                   .ToList().FirstOrDefault()
                                               );
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

            List<ViewerAnswer> viewerAnswers = GetQuestion(questionPrompt, coll).ViewerAnswers;

            List<Answer> answers = viewerAnswers.GroupBy(viewerAnswer => viewerAnswer.Answer)
                                                .Select(group => new Answer(
                                                    group.Count(),
                                                    group.Key)
                                                ).ToList();

            return answers;
        }

        public async Task<Question<Answer>?> InsertAnswers(Question<ViewerAnswer> question, string collectionName)
        {
            //TODO needs to be tested
            var answers = GetAnswers(question.Prompt, collectionName);

            RestClient restClient = new RestClient("https://localhost:7200/");

            var restQuestion = new Question<Answer>(question.Prompt, answers, question.QuestionId);

            RestRequest restRequest = new RestRequest("api/Question/answers/{id}")
                .AddUrlSegment("id", question.QuestionId).AddJsonBody(restQuestion);

            var response = await restClient.ExecutePutAsync<Question<Answer>>(restRequest);

            return response.Data;
        }
    }
}
