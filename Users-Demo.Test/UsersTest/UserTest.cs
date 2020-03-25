using System.Threading.Tasks;
using Moq;
using Users_Demo.DAL.Models;
using Users_Demo.Repository.Interface;
using Xunit;

namespace Users_Demo.Test.UsersTest
{
    public class UserTest
    {
        private Mock<IRepository<Users>> _repo;

        public UserTest()
        {
            _repo = new Mock<IRepository<Users>>();
        }

        [Fact]
        public void GetUsers_With_Response()
        {
            GetUsersSetUp(true);

            var users = _repo.Object.Get();

            Assert.NotNull(users);
        }

        [Fact]
        public void GetUsers_Empty_Response()
        {
            GetUsersSetUp(false);

            var users = _repo.Object.Get();

            Assert.NotNull(users);
        }

        [Fact]
        public void When_Create_Expect_MethodCalled()
        {
            var user = FakeUser.GetSampleUser(true);

            _repo.Object.CreateAsync(user);
            _repo.Verify(x => x.CreateAsync(user), Times.Once);
        }

        [Fact]
        public async Task CreateUser_Returns_False()
        {
            var userData = FakeUser.GetSampleUser(true);
            var userResponse = await _repo.Object.CreateAsync(userData);
            Assert.False(userResponse);
        }

        [Fact]
        public async Task UpdateUser_Returns_False()
        {
            var userData = FakeUser.GetSampleUser(true);
            var userResponse = await _repo.Object.UpdateAsync(userData);
            Assert.False(userResponse);
        }

        [Fact]
        public void When_IdIsDigitAndNotZeroOrLess_Expect_EqualId()
        {
            int id = 3;
            GetTimeLogByIdSetUp(true);

            var user = _repo.Object.GetByIdAsync(id);
            var actual = user.Result;

            Assert.Equal(3, actual.Id);
        }

        private void GetUsersSetUp(bool hasData)
        {
            _repo.Setup(x => x.Get())
                .Returns(FakeUser.GetSampleUsers(hasData));
        }

        private void GetTimeLogByIdSetUp(bool hasData)
        {
            _repo.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(FakeUser.GetSampleUser(hasData)));
        }
    }
}
