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
            //Assert.Equal(_testAnswer.text, answer.text);
            //Assert.Equal(_testAnswer.answerCount, answer.answerCount);
            Assert.Equal(1, id);
        }
    }
}
