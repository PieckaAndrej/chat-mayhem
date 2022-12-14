using Data.DatabaseLayer;
using Data.ModelLayer;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Test
{
    public class AnswerTest
    {
        private readonly ITestOutputHelper _extraOutput;
        private readonly Answer _testAnswer;
        private readonly IConfiguration _testConfiguration;
        private readonly int _questionId;

        public AnswerTest(ITestOutputHelper extraOutput)
        {
            _extraOutput = extraOutput;

            _testAnswer = new Answer(10, "Shirt " + DateTime.Now.ToString());

            _questionId = 1;
            _testConfiguration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.Test.json", optional: false)
                .Build();
        }

        [Fact]
        public void TestCreateAnswer()
        {
            //Arrange
            var answerAccess = new AnswerAccess(_testConfiguration.GetConnectionString("ChatMayhem Connection") ?? "");

            //Act
            var id = answerAccess.CreateAnswer(_testAnswer, _questionId);

            //Assert
            Assert.Equal(1, id);
        }

        [Fact]
        public void UpdatePoints()
        {
            //Arrange
            var answerAccess = new AnswerAccess(_testConfiguration.GetConnectionString("ChatMayhem Connection") ?? "");

            //Act
            int oldPoints = 100;
            answerAccess.CreateAnswer(_testAnswer, _questionId);
            int rowsAffected = answerAccess.UpdatePoints(_testAnswer, oldPoints, _questionId);

            //Assert
            Assert.Equal(1, rowsAffected);
        }

        [Fact]
        public void GetAnswersTest()
        {
            //Arrange
            var answerAccess = new AnswerAccess(_testConfiguration.GetConnectionString("ChatMayhem Connection") ?? "");

            //Act
            answerAccess.CreateAnswer(_testAnswer, _questionId);
            List<Answer> answers = new List<Answer>();
            answers = answerAccess.GetQuestionsAnswerById(_questionId);

            //Assert
            Assert.True(answers.Count != 0);
        }

        [Fact]
        public void InsertAnswersTest()
        {
            //Arrange
            var answerAccess = new AnswerAccess(_testConfiguration.GetConnectionString("ChatMayhem Connection") ?? "");

            //Act
            List<Answer> answers = new List<Answer>();
            List<Answer> returnAnswers = new List<Answer>();
            answers.Add(_testAnswer);
            returnAnswers = answerAccess.InsertAnswers(answers, _questionId);

            //Assert
            Assert.True(returnAnswers.Count == 1);
        }
    }
}
