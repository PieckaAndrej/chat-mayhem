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

        public AnswerTest(ITestOutputHelper extraOutput)
        {
            _extraOutput = extraOutput;

            _testAnswer = new Answer(10, "Shirt", 1);

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
            Assert.Equal(_testAnswer.text, answer?.text);
            Assert.Equal(_testAnswer.questionId, answer?.questionId);
            Assert.Equal(_testAnswer.answerCount, answer?.answerCount);
        }
    }
}
