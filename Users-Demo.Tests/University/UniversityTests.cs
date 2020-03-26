﻿using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
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
    }
}
