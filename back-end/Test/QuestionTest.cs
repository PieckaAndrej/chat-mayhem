using API.Controllers;
using API.DTOs;
using API.Services;
using Data.DatabaseLayer;
using Data.ModelLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Test
{
    public class QuestionTest
    {
        private readonly ITestOutputHelper _extraOutput;
        private readonly Question _testQuestion;
        private readonly IConfiguration _testConfiguration;

        public QuestionTest(ITestOutputHelper extraOutput)
        {

            _extraOutput = extraOutput;

            _testQuestion = new Question(1, "testQuestion");

            Answer answer1 = new Answer(5, "answerTest");

            Answer answer2 = new Answer(10, "answerTest2");

            List<Answer> answers = new List<Answer>();

            answers.Add(answer1);

            answers.Add(answer2);

            _testQuestion.answers = answers;

            _testQuestion.QuestionPackId = 1;

            _testConfiguration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.Test.json", optional: false)
                .Build();
        }

        [Fact]
        public void TestCreateGame()
        {
            //Arrange
            var questionAccess = new QuestionAccess(_testConfiguration.GetConnectionString("ChatMayhem Connection") ?? "");
            int questionPackId = _testQuestion.QuestionPackId;

            //Act
            Question resultQuestion = questionAccess.InsertQuestion(_testQuestion, questionPackId);

            //Assert
            Assert.Equal(_testQuestion.text, resultQuestion?.text);
            Assert.Equal(_testQuestion.id, resultQuestion?.id);
            Assert.Equal(_testQuestion.QuestionPackId, resultQuestion?.QuestionPackId);
        }

        [Fact]
        public void TestGetAllGames()
        {
            var questionAccess = new QuestionAccess(_testConfiguration.GetConnectionString("ChatMayhem Connection") ?? "");

            List<Question>? resultQuestions = questionAccess.GetQuestions();

            Assert.NotNull(resultQuestions);
        }

        [Fact]
        public void TestGetGameById()
        {
            //Arrange
            var questionAccess = new QuestionAccess(_testConfiguration.GetConnectionString("ChatMayhem Connection") ?? "");
            int questionPackId = _testQuestion.QuestionPackId;

            //Act
            Question question = questionAccess.InsertQuestion(_testQuestion, questionPackId);
            Question resultQuestion = questionAccess.GetQuestionById(question.id);

            //Assert
            Assert.Equal(_testQuestion.text, resultQuestion?.text);
            Assert.Equal(_testQuestion.id, resultQuestion?.id);
            Assert.Equal(_testQuestion.QuestionPackId, resultQuestion?.QuestionPackId);
        }

        [Fact]
        public void TestInsertAnswersInQuestion()
        {
            //Arrange
            var questionAccess = new QuestionAccess(_testConfiguration.GetConnectionString("ChatMayhem Connection") ?? "");
            var questionController = new QuestionController(_testConfiguration);
            int questionPackId = _testQuestion.QuestionPackId;

            //Act
            Question question = questionAccess.InsertQuestion(_testQuestion, questionPackId);
            ActionResult<Question> result = questionController.Put(question, question.id);

            Question resultValue = result.Value;

            //Assert
            Assert.NotNull(resultValue.answers);
        }
    }
}
