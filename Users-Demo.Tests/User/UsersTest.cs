using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Users_Demo.Tests.User
{
    public class UsersTest : IClassFixture<TestingFactory<Startup>>
    {
        private readonly TestingFactory<Startup> _factory;
        private readonly WebApplicationFactory<Startup> _webFactory;

        public UsersTest(TestingFactory<Startup> factory)
        {
            _factory = factory;
            _webFactory = new WebApplicationFactory<Startup>();
        }

        [Fact]
        public async Task Post_Users_Returns_OK()
        {
            var client = _webFactory.CreateClient();
            var request = new
            {
                Url = "api/users",
                Body = new
                {
                    Id = 10,
                    FirstName = "TestF10",
                    LastName = "TestL10",
                    IsDeleted = false,
                    DateOfBirth = DateTime.UtcNow,
                    IsActive = true
                }
            };
            var bodyContent = new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(request.Url, bodyContent);

            var respMessage = response.EnsureSuccessStatusCode();

            Assert.Equal("Created", response.ReasonPhrase);
            Assert.Equal(HttpStatusCode.Created, respMessage.StatusCode);
        }

        [Fact]
        public async Task Post_University_Returns_BadRequest()
        {
            var client = _webFactory.CreateClient();
            var request = new
            {
                Url = "api/users",
                Body = new
                {
                    Id = -1,
                    FirstName = "TestFNegativeOne",
                    LastName = "TestLNegativeOne",
                    IsDeleted = false,
                    DateOfBirth = DateTime.UtcNow,
                    IsActive = true
                }
            };
            var bodyContent = new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(request.Url, bodyContent);

            Assert.Equal("Bad Request", response.ReasonPhrase);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("api/users")]
        public async Task Get_Users_Return_NoContent(string url)
        {
            var client = _webFactory.CreateClient();

            var response = await client.GetAsync(url);

            var statusCode = response.StatusCode;

            Assert.Equal("No Content", response.ReasonPhrase);
            Assert.Equal(HttpStatusCode.NoContent, statusCode);
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
        public async Task Get_Users_Return_NotFound(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            var statusCode = response.StatusCode;

            Assert.Equal("Not Found", response.ReasonPhrase);
            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        }

        [Theory]
        [InlineData("api/users/GetById?Id=1")]
        public async Task Get_UsersById_Return_Success(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            var code = response.EnsureSuccessStatusCode();

            Assert.Equal("OK", response.ReasonPhrase);
            Assert.True(code.IsSuccessStatusCode);
        }

        [Theory]
        [InlineData("api/users/GetById?Id=100")]
        public async Task Get_UsersById_Return_NoContent(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            var statusCode = response.StatusCode;

            Assert.Equal("No Content", response.ReasonPhrase);
            Assert.Equal(HttpStatusCode.NoContent, statusCode);
        }

        [Theory]
        [InlineData("api/users/GetByFirstName?FirstName=TestF1")]
        public async Task Get_UsersByFirstName_Return_Ok(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            var statusCode = response.StatusCode;

            Assert.Equal("OK", response.ReasonPhrase);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }

        [Theory]
        [InlineData("api/users/GetByLastName?LastName=TestLastName")]
        public async Task Get_UsersByLastName_Return_NoContent(string url)
        {
            var client = _webFactory.CreateClient();

            var response = await client.GetAsync(url);

            var statusCode = response.StatusCode;

            Assert.Equal("No Content", response.ReasonPhrase);
            Assert.Equal(HttpStatusCode.NoContent, statusCode);
        }

        [Theory]
        [InlineData("api/users/GetByLastName?LastName=TestL2")]
        public async Task Get_UsersByLastName_Return_Ok(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            var statusCode = response.StatusCode;

            Assert.Equal("OK", response.ReasonPhrase);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }

        [Theory]
        [InlineData("api/users/GetByFirstName?FirstName=TestFirstName")]
        public async Task Get_UsersByFirstName_Return_NoContent(string url)
        {
            var client = _webFactory.CreateClient();

            var response = await client.GetAsync(url);

            var statusCode = response.StatusCode;

            Assert.Equal("No Content", response.ReasonPhrase);
            Assert.Equal(HttpStatusCode.NoContent, statusCode);
        }


        [Theory]
        [InlineData("api/users?id=5")]
        public async Task Delete_Users_Returns_OK(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync(url);

            var statusCode = response.StatusCode;

            Assert.Equal("OK", response.ReasonPhrase);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }

        [Theory]
        [InlineData("api/users?id=500")]
        public async Task Delete_Users_Returns_BadRequest(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync(url);

            var statusCode = response.StatusCode;

            Assert.Equal("Internal Server Error", response.ReasonPhrase);
            Assert.Equal(HttpStatusCode.InternalServerError, statusCode);
        }
    }
}
