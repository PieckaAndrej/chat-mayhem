using WebApp.Services;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            StreamerService.RefreshToken("lol", "yo mama is gae");
        }
    }
}