﻿using Moq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Users_Demo.Repository.Interface;
using Users_Demo.Services.Implementation;
using Users_Demo.Services.Interface;
using Xunit;

namespace Users_Demo.Test.UniversityTest
{
    public class UniversityTest
    {
        private Mock<IRepository<DAL.Models.University>> _repo;
        private IUniversityService _service;

        public UniversityTest()
        {
            var context1 = new Mock<DbContext>();
            _repo = new Mock<IRepository<DAL.Models.University>>();
            _service = new UniversityService(context1.Object);
        }

        [Fact]
        public void When_IdIsLessThanZero_Expect_ThrowNullException_DueTo_NegativeParameter()
        {
            int id = -1;

            var actual = Assert.ThrowsAsync<NullReferenceException>(() => _service.GetByIdAsync(id));
            var expected = $"Object reference not set to an instance of an object.";

            Assert.Equal(expected, actual.Result.Message);
        }

        [Fact]
        public void GetUsers_With_Response()
        {
            GetUsersSetUp(true);

            var universities = _repo.Object.Get();

            Assert.NotNull(universities);
        }

        [Fact]
        public void GetUsers_Empty_Response()
        {
            GetUsersSetUp(false);

            var universities = _repo.Object.Get();

            Assert.NotNull(universities);
        }

        [Fact]
        public void When_Create_Expect_MethodCalled()
        {
            var universities = FakeUniversity.GetSampleUniversity(true);

            _repo.Object.CreateAsync(universities);
            _repo.Verify(x => x.CreateAsync(universities), Times.Once);
        }

        [Fact]
        public void When_IdIsDigitAndNotZeroOrLess_Expect_EqualId()
        {
            int id = 3;
            GetTimeLogByIdSetUp(true);

            var university = _repo.Object.GetByIdAsync(id);
            var actual = university.Result;

            Assert.Equal(3, actual.Id);
        }

        [Fact]
        public async Task CreateUniversity_Returns_False()
        {
            var universityData = FakeUniversity.GetSampleUniversity(true);
            var universityResponse = await _repo.Object.CreateAsync(universityData);
            Assert.False(universityResponse);
        }

        [Fact]
        public async Task UpdateUniversity_Returns_False()
        {
            var universityData = FakeUniversity.GetSampleUniversity(true);
            var universityResponse = await _repo.Object.UpdateAsync(universityData);
            Assert.False(universityResponse);
        }

        [Fact]
        public async Task DeleteUniversity_Returns_True()
        {
            var id = 3;
            _repo.Setup(x => x.GetByIdAsync(id))
                .Returns(Task.FromResult(FakeUniversity.GetSampleUniversity(true)));

            _repo.Setup(x => x.UpdateAsync(FakeUniversity.GetSampleUniversity(true)));

            var univ = await _service.DeleteAsync(id);

            Assert.True(univ);
        }

        private void GetUsersSetUp(bool hasData)
        {
            _repo.Setup(x => x.Get())
                .Returns(FakeUniversity.GetSampleUniversities(hasData));
        }

        private void GetTimeLogByIdSetUp(bool hasData)
        {
            _repo.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(FakeUniversity.GetSampleUniversity(hasData)));
        }
    }
}