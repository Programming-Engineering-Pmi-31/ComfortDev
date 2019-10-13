using NUnit.Framework;
using ComfortDev.BLL.Services;
using ComfortDev.DAL.UnitsOfWork;
using ComfortDev.DAL;
using ComfortDev.Common.Entities;
using System.Linq;

namespace ComfortDev.Tests
{
    public class UserServiceTest
    {
        readonly string testConnectionString = "Host=localhost;Port=5432;Database=ComfortDevTest;Username=postgres;Password=1058;";
        
        UserService CreateUserService()
        {
            return new UserService(
                new ComfortDevUnitOfWork(
                    new ComfortDevContext(testConnectionString)
                )
            );
        }

        [TearDown]
        public void ClearDB()
        {
            var context = new ComfortDevContext(testConnectionString);
            context.Users.RemoveRange(context.Users);
            context.SaveChanges();
        }

        [Test]
        public void CreateTest()
        {
            var userService = CreateUserService();
            var username = "TestUserName";
            var password = "TestPassword123";

            userService.Create(username, password);
            var user = userService.GetAll().SingleOrDefault();
            Assert.AreEqual(username, user.Name);
        }

        [Test]
        public void DeleteUser()
        {
            var userService = CreateUserService();
            var username = "TestUserName";
            var password = "TestPassword123";

            userService.Create(username, password);
            var user = userService.GetAll().SingleOrDefault();

            userService.Delete(user.Id);
            Assert.AreEqual(0, userService.GetAll().Count());
        }

        [Test]
        public void AuthenticateTest()
        {
            var userService = CreateUserService();
            var username = "TestUserName";
            var password = "TestPassword123";

            userService.Create(username, password);
            var user = userService.Authenticate(username, password);
            Assert.NotNull(user);
        }

        [Test]
        public void GetByIdTest()
        {
            var userService = CreateUserService();
            var username = "TestUserName";
            var password = "TestPassword123";

            userService.Create(username, password);
            var user = userService.GetAll().FirstOrDefault();
            Assert.NotNull(userService.GetById(user.Id));
        }

        [Test]
        public void GetAll()
        {
            var userService = CreateUserService();
            var username = "TestUserName";
            var password = "TestPassword123";

            userService.Create(username, password);
            userService.Create(username + 2, password);

            Assert.AreEqual(2, userService.GetAll().Count());
        }
    }
}