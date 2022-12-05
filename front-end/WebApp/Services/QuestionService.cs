﻿using MongoDB.Bson;
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


        public static async Task<int> InsertAnswers(ViewerAnswer viewerAnswer, 
            string collectionName, Question<ViewerAnswer> question)
        {
            int result = 0;

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
                q.Answers.Add(viewerAnswer);
                await collection.InsertOneAsync(q);
                result = 1;
            } 
            else
            {
                MongoDB.Driver.FilterDefinition<Question<ViewerAnswer>> filter;
                MongoDB.Driver.UpdateDefinition<Question<ViewerAnswer>> update;

                if (!GetQuestion(question.Prompt,collection).Answers
                                .Select<ViewerAnswer, string>(a => a.Username).ToList()
                                .Contains(viewerAnswer.Username))
                {
                    filter = Builders<Question<ViewerAnswer>>.Filter.Eq(q => q.Prompt, question.Prompt);
                    update = Builders<Question<ViewerAnswer>>.Update.AddToSet(s => s.Answers, viewerAnswer);
                    result = 1;
                }
                else
                {
                    filter = Builders<Question<ViewerAnswer>>.Filter.Where(q => q.Prompt == question.Prompt 
                                            && q.Answers.Any(v => v.Username.Equals(viewerAnswer.Username)));
                    update = Builders<Question<ViewerAnswer>>.Update.Set(s => s.Answers[-1], viewerAnswer);
                }
                await collection.UpdateOneAsync(filter, update);
            }

            return result;
        }

        public static Question<ViewerAnswer>? GetQuestion(string questionPrompt, IMongoCollection<Question<ViewerAnswer>> collection)
        {
            var question = collection.Find(x => x.Prompt == questionPrompt)
                .Project("{_id: 0, Prompt: 1, Answers: 1, QuestionId: 1}")
                .ToList().FirstOrDefault();

            if (question == null)
            {
                return null;
            }

            return BsonSerializer.Deserialize<Question<ViewerAnswer>>(question);
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

            var question = GetQuestion(questionPrompt, coll);

            if (question == null)
            {
                return new List<Answer>();
            }

            List<ViewerAnswer> viewerAnswers = question.Answers;

            List<Answer> answers = viewerAnswers.GroupBy(viewerAnswer => viewerAnswer.Answer)
                                                .Select(group => new Answer(
                                                    group.Count(),
                                                    group.Key)
                                                ).ToList();

            answers.Sort((answer1, answer2) => 
                answer1.Points > answer2.Points ? 1 : answer1.Points == answer2.Points ? 0 : -1);

            return answers;
        }

        public async Task<List<AnswerDto>> PostAnswers(string questionPrompt, string collectionName)
        {
            List<Answer> answers = new List<Answer>();

            List<AnswerDto> answersDto = new List<AnswerDto>();

            Question<ViewerAnswer> question = new Question<ViewerAnswer>();

            answers = GetAnswers(questionPrompt, collectionName);

            RestClient restClient = new RestClient("https://localhost:7200/");

            var client = new MongoClient(_con);

            var db = client.GetDatabase("ChatMayhem");

            var collection = db.GetCollection<Question<ViewerAnswer>>(collectionName);

            question = GetQuestion(questionPrompt, collection);

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