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
    public class StremerTest
    {
        private readonly ITestOutputHelper _extraOutput;
        private readonly Streamer _testStreamer;
        private readonly IConfiguration _testConfiguration;

        public StremerTest(ITestOutputHelper extraOutput)
        {
            _extraOutput = extraOutput;

            _testStreamer = new Streamer("ghjk", "456", "fghij");

            _testConfiguration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.Test.json", optional: false)
                .Build();
        }

        [Fact]
        public void TestCreateStreamer()
        {
            //Arrange
            var streamerAccess = new StreamerAccess(_testConfiguration.GetConnectionString("ChatMayhem Connection") ?? "");

            //Act
            var streamer = streamerAccess.UpdateStreamer(_testStreamer);
            var resultStreamer = streamerAccess.GetStreamerById(streamer.Id);

            //Assert
            Assert.Equal(_testStreamer.AccessToken, resultStreamer?.AccessToken);
            Assert.Equal(_testStreamer.Id, resultStreamer?.Id);
            Assert.Equal(_testStreamer.RefreshToken, resultStreamer?.RefreshToken);
        }

        [Fact]
        public void TestGetGame()
        {
            //Arrange
            _testStreamer.Id = "get";
            var streamerAccess = new StreamerAccess(_testConfiguration.GetConnectionString("ChatMayhem Connection") ?? "");

            //Act
            streamerAccess.UpdateStreamer(_testStreamer);
            var resultStreamer = streamerAccess.GetStreamerById(_testStreamer.Id);

            //Assert
            Assert.Equal(_testStreamer.AccessToken, resultStreamer?.AccessToken);
            Assert.Equal(_testStreamer.Id, resultStreamer?.Id);
            Assert.Equal(_testStreamer.RefreshToken, resultStreamer?.RefreshToken);
        }
    }
}
