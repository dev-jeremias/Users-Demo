using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Users_Demo.Tests.University
{
    public class UniversityTests : IClassFixture<TestingFactory<Startup>>
    {
        private readonly TestingFactory<Startup> _factory;

        public UniversityTests(TestingFactory<Startup> factory)
        {
            _factory = factory;
        }


        [Theory]
        [InlineData("api/university")]
        public async Task Get_University_Return_Success(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            var code = response.EnsureSuccessStatusCode();

            Assert.Equal("OK", response.ReasonPhrase);
            Assert.True(code.IsSuccessStatusCode);
        }

        [Theory]
        [InlineData("api/universities")] //wrong route
        public async Task Get_UniversityById_Return_Success(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            var statusCode = response.StatusCode;

            Assert.Equal("Not Found", response.ReasonPhrase);
            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        }


    }
}
