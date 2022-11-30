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
