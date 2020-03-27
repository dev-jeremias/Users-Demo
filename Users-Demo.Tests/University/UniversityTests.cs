using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Shouldly;
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
            //Arrange
            var client = _webFactory.CreateClient();

            //Act
            var response = await client.GetAsync(url);

            //Assert
            response.ReasonPhrase.ShouldBe("No Content");
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }


        [Theory]
        [InlineData("api/university")]
        public async Task Get_University_Return_Success(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            var code = response.EnsureSuccessStatusCode();

            response.ReasonPhrase.ShouldBe("OK");
            code.IsSuccessStatusCode.ShouldBeTrue();
        }

        [Theory]
        [InlineData("api/universities")] //wrong route
        public async Task Get_University_Return_NotFound(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            var statusCode = response.StatusCode;

            response.ReasonPhrase.ShouldBe("Not Found");
            statusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData("api/university/GetById?Id=1")]
        public async Task Get_UniversityById_Return_Success(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            var code = response.EnsureSuccessStatusCode();

            response.ReasonPhrase.ShouldBe("OK");
            code.IsSuccessStatusCode.ShouldBeTrue();
        }

        [Theory]
        [InlineData("api/university/GetById?Id=100")]
        public async Task Get_UniversityById_Return_NoContent(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            var statusCode = response.StatusCode;

            response.ReasonPhrase.ShouldBe("No Content");
            statusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Theory]
        [InlineData("api/university/GetUniversityByName?Name=TestingOnly")]
        public async Task Get_UniversityByName_Return_NoContent(string url)
        {
            var client = _webFactory.CreateClient();

            var response = await client.GetAsync(url);

            var statusCode = response.StatusCode;

            response.ReasonPhrase.ShouldBe("No Content");
            statusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Theory]
        [InlineData("api/university/GetUniversityByName?Name=Test1")]
        public async Task Get_UniversityByName_Return_Ok(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            var statusCode = response.StatusCode;

            response.ReasonPhrase.ShouldBe("OK");
            statusCode.ShouldBe(HttpStatusCode.OK);
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

            response.ReasonPhrase.ShouldBe("Created");
            respMessage.StatusCode.ShouldBe(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Post_University_Returns_BadRequest()
        {
            var client = _webFactory.CreateClient();
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
            var response = await client.PostAsync(request.Url, bodyContent);

            response.ReasonPhrase.ShouldBe("Bad Request");
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
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

            response.ReasonPhrase.ShouldBe("OK");
            respMessage.StatusCode.ShouldBe(HttpStatusCode.OK);
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

            response.ReasonPhrase.ShouldBe("Bad Request");
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("api/university?id=1")]
        public async Task Delete_University_Returns_OK(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync(url);

            var statusCode = response.StatusCode;

            response.ReasonPhrase.ShouldBe("OK");
            statusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("api/university?id=500")]
        public async Task Delete_University_Returns_BadRequest(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync(url);

            var statusCode = response.StatusCode;

            response.ReasonPhrase.ShouldBe("Internal Server Error");
            statusCode.ShouldBe(HttpStatusCode.InternalServerError);
        }
    }
}
