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
    public class QuestionPackTest
    {
        private readonly ITestOutputHelper _extraOutput;
        private readonly QuestionPack _testQuestionPack;
        private readonly IConfiguration _testConfiguration;

        public QuestionPackTest(ITestOutputHelper extraOutput)
        {
            _extraOutput = extraOutput;

            _testQuestionPack = new QuestionPack("me", "best questions",
                new String[1] {"m"}, "questions", DateTime.Parse("2022-11-15"));

            _testQuestionPack.Id = 100;

            _testConfiguration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.Test.json", optional: false)
                .Build();
        }

        [Fact]
        public void TestGetGame()
        {
            //Arrange
            var questionPackAccess = new QuestionPackAccess(_testConfiguration.GetConnectionString("ChatMayhem Connection") ?? "");

            //Act
            questionPackAccess.CreateQuestionPack(_testQuestionPack);
            var resultQuestioPack = questionPackAccess.GetQuestionPackById(_testQuestionPack.Id);

            //Assert
            Assert.Equal(_testQuestionPack.Author, resultQuestioPack?.Author);
            Assert.Equal(_testQuestionPack.Name, resultQuestioPack?.Name);
            Assert.Equal(_testQuestionPack.Category, resultQuestioPack?.Category);
            Assert.Equal(_testQuestionPack.CreationDate, resultQuestioPack?.CreationDate);
        }
    }
}
