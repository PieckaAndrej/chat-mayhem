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
        private readonly List<Answer> _testAnswer;
        private readonly IConfiguration _testConfiguration;

        public AnswerTest(ITestOutputHelper extraOutput)
        {
            _extraOutput = extraOutput;

            _testAnswer = new List<Answer>() { new Answer(10, "Shirt " + DateTime.Now.ToString() , 1) };

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
            var answer = answerAccess.CreateAnswer(_testAnswer);

            //Assert
            Assert.Equal(_testAnswer[0].text, answer?[0].text);
            Assert.Equal(_testAnswer[0].questionId, answer?[0].questionId);
            Assert.Equal(_testAnswer[0].answerCount, answer?[0].answerCount);
        }
    }
}
