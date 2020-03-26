using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Users_Demo.Tests.University
{
    public class UniversityTests : IClassFixture<TestingFactory<Startup>>
    {
        private readonly TestingFactory<Startup> _factory;
        private readonly WebApplicationFactory<Startup> _webFactory;

        public UniversityTests(TestingFactory<Startup> factory)
        {
            _factory = factory;
            _webFactory = new WebApplicationFactory<Startup>();
        }

        [Theory]
        [InlineData("api/university")]
        public async Task Get_University_Return_NoContent(string url)
        {
            var client = _webFactory.CreateClient();

            var response = await client.GetAsync(url);

            var statusCode = response.StatusCode;

            Assert.Equal("No Content", response.ReasonPhrase);
            Assert.Equal(HttpStatusCode.NoContent, statusCode);
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
        public async Task Get_University_Return_NotFound(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            var statusCode = response.StatusCode;

            Assert.Equal("Not Found", response.ReasonPhrase);
            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        }

        [Theory]
        [InlineData("api/university/GetById?Id=1")]
        public async Task Get_UniversityById_Return_Success(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            var code = response.EnsureSuccessStatusCode();

            Assert.Equal("OK", response.ReasonPhrase);
            Assert.True(code.IsSuccessStatusCode);
        }

        [Theory]
        [InlineData("api/university/GetById?Id=100")]
        public async Task Get_UniversityById_Return_NoContent(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            var statusCode = response.StatusCode;

            Assert.Equal("No Content", response.ReasonPhrase);
            Assert.Equal(HttpStatusCode.NoContent, statusCode);
        }

        [Theory]
        [InlineData("api/university/GetUniversityByName?Name=TestingOnly")]
        public async Task Get_UniversityByName_Return_NoContent(string url)
        {
            var client = _webFactory.CreateClient();

            var response = await client.GetAsync(url);

            var statusCode = response.StatusCode;

            Assert.Equal("No Content", response.ReasonPhrase);
            Assert.Equal(HttpStatusCode.NoContent, statusCode);
        }

        [Theory]
        [InlineData("api/university/GetUniversityByName?Name=Test1")]
        public async Task Get_UniversityByName_Return_Ok(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            var statusCode = response.StatusCode;

            Assert.Equal("OK", response.ReasonPhrase);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public async Task Post_University_Returns_OK()
        {
            var client = _webFactory.CreateClient();
            var request = new
            {
                Url = "api/university",
                Body = new
                {
                    id = 1,
                    name = "test",
                    isActive = true,
                    isDeleted = true
                }
            };
            var bodyContent = new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(request.Url, bodyContent);

            var respMessage = response.EnsureSuccessStatusCode();

            Assert.Equal("Created", response.ReasonPhrase);
            Assert.Equal(HttpStatusCode.Created, respMessage.StatusCode);
        }

        [Fact]
        public async Task Update_University_Returns_Ok()
        {
            var client = _factory.CreateClient();
            var request = new
            {
                Url = "api/university",
                Body = new
                {
                    id = 1,
                    name ="test",
                    isActive = true,
                    isDeleted = true
                }
            };
            var bodyContent = new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.UTF8, "application/json");
            var response = await client.PutAsync(request.Url, bodyContent);

            var respMessage = response.EnsureSuccessStatusCode();

            Assert.Equal("OK", response.ReasonPhrase);
            Assert.Equal(HttpStatusCode.OK, respMessage.StatusCode);
        }

        [Fact]
        public async Task Update_University_Returns_BadRequest()
        {
            var client = _factory.CreateClient();
            var request = new
            {
                Url = "api/university",
                Body = new
                {
                    id = -1,
                    name = "test",
                    isActive = true,
                    isDeleted = true
                }
            };
            var bodyContent = new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.UTF8, "application/json");
            var response = await client.PutAsync(request.Url, bodyContent);

            Assert.Equal("Bad Request", response.ReasonPhrase);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("api/university?id=1")]
        public async Task Delete_University_Returns_OK(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync(url);

            var statusCode = response.StatusCode;

            Assert.Equal("OK", response.ReasonPhrase);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }

        [Theory]
        [InlineData("api/university?id=500")]
        public async Task Delete_University_Returns_BadRequest(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync(url);

            var statusCode = response.StatusCode;

            Assert.Equal("Internal Server Error", response.ReasonPhrase);
            Assert.Equal(HttpStatusCode.InternalServerError, statusCode);
        }
    }
}
