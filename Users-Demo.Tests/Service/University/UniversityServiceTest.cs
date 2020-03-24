using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Users_Demo.Repository.Interface;

namespace Users_Demo.Tests.Service.University
{
    public class UniversityServiceTest
    {
        private readonly Mock<IRepository<DAL.Models.University>> _repo;

        public UniversityServiceTest()
        {
            _repo = new Mock<IRepository<DAL.Models.University>>();
        }

        [Test]
        public void GetUsers_With_Response()
        {
            GetUsersSetUp(true);

            var universities = _repo.Object.Get();

            Assert.NotNull(universities);
        }

        [Test]
        public void GetUsers_Empty_Response()
        {
            GetUsersSetUp(false);

            var universities = _repo.Object.Get();

            Assert.NotNull(universities);
        }

        [Test]
        public void When_Create_Expect_MethodCalled()
        {
            var universities = FakeUniversityData.GetSampleUniversity(true);

            _repo.Object.CreateAsync(universities);
            _repo.Verify(x => x.CreateAsync(universities), Times.Once);
        }

        [Test]
        public void When_IdIsDigitAndNotZeroOrLess_Expect_EqualId()
        {
            int id = 3;
            GetTimeLogByIdSetUp(true);

            var university = _repo.Object.GetByIdAsync(id);
            var actual = university.Result;

            Assert.AreEqual(3, actual.Id);
        }

        [Test]
        public async Task CreateUniversity_Returns_False()
        {
            var universityData = FakeUniversityData.GetSampleUniversity(true);
            var universityResponse = await _repo.Object.CreateAsync(universityData);
            Assert.IsFalse(universityResponse);
        }

        [Test]
        public async Task UpdateUniversity_Returns_False()
        {
            var universityData = FakeUniversityData.GetSampleUniversity(true);
            var universityResponse = await _repo.Object.UpdateAsync(universityData);
            Assert.IsFalse(universityResponse);
        }

        private void GetUsersSetUp(bool hasData)
        {
            _repo.Setup(x => x.Get())
                .Returns(FakeUniversityData.GetSampleUniversities(hasData));
        }

        private void GetTimeLogByIdSetUp(bool hasData)
        {
            _repo.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(FakeUniversityData.GetSampleUniversity(hasData)));
        }
    }
}
