using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Users_Demo.Tests.User
{
    public class UsersTest : IClassFixture<TestingFactory<Startup>>
    {
        private readonly TestingFactory<Startup> _factory;

        public UsersTest(TestingFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("api/users")]
        public async Task Get_Users_Return_Success(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            var code = response.EnsureSuccessStatusCode();

            Assert.Equal("OK", response.ReasonPhrase);
            Assert.True(code.IsSuccessStatusCode);
        }

        [Theory]
        [InlineData("api/user")] //wrong route
        public async Task Get_UsersById_Return_Success(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            var statusCode = response.StatusCode;

            Assert.Equal("Not Found", response.ReasonPhrase);
            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        }
    }
}
