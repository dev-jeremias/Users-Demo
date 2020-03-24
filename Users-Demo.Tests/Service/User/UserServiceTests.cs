using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Users_Demo.DAL.Models;
using Users_Demo.Repository.Interface;

namespace Users_Demo.Tests.Service.User
{
    public class UserServiceTests
    {
        private readonly Mock<IRepository<Users>> _repo;
        public UserServiceTests()
        {
            _repo = new Mock<IRepository<Users>>();
        }

        [Test]
        public void GetUsers_With_Response()
        {
            GetUsersSetUp(true);

            var users = _repo.Object.Get();

            Assert.NotNull(users);
        }

        [Test]
        public void GetUsers_Empty_Response()
        {
            GetUsersSetUp(false);

            var users = _repo.Object.Get();

            Assert.NotNull(users);
        }

        [Test]
        public void When_Create_Expect_MethodCalled()
        {
            var user = FakeUsersData.GetSampleUser(true);

            _repo.Object.CreateAsync(user);
            _repo.Verify(x => x.CreateAsync(user), Times.Once);
        }

        [Test]
        public async Task CreateUser_Returns_False()
        {
            var userData = FakeUsersData.GetSampleUser(true);
            var userResponse = await _repo.Object.CreateAsync(userData);
            Assert.IsFalse(userResponse);
        }

        [Test]
        public async Task UpdateUser_Returns_False()
        {
            var userData = FakeUsersData.GetSampleUser(true);
            var userResponse = await _repo.Object.UpdateAsync(userData);
            Assert.IsFalse(userResponse);
        }

        [Test]
        public void When_IdIsDigitAndNotZeroOrLess_Expect_EqualId()
        {
            int id = 3;
            GetTimeLogByIdSetUp(true);

            var user = _repo.Object.GetByIdAsync(id);
            var actual = user.Result;

            Assert.AreEqual(3, actual.Id);
        }

        private void GetUsersSetUp(bool hasData)
        {
            _repo.Setup(x => x.Get())
                .Returns(FakeUsersData.GetSampleUsers(hasData));
        }

        private void GetTimeLogByIdSetUp(bool hasData)
        {
            _repo.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(FakeUsersData.GetSampleUser(hasData)));
        }
    }
}
