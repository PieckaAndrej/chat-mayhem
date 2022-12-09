using Data.ModelLayer;
using Microsoft.Extensions.Configuration;
using Npgsql;
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
    public class ConnectionTest
    {

        private readonly ITestOutputHelper _extraOutput;
        private readonly IConfiguration _configuration;

        public ConnectionTest(ITestOutputHelper extraOutput)
        {

            _extraOutput = extraOutput;

            _configuration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", optional: false)
                .Build();
        }

        [Fact]
        public void TestConnection()
        {
            _extraOutput.WriteLine(_configuration.GetConnectionString("ChatMayhem Connection"));
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("ChatMayhem Connection")))
            {
                connection.Open();
                Assert.Equal(System.Data.ConnectionState.Open, connection.State);
            }
        }
    }
}
