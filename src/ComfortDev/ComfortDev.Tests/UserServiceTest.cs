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
            var password = "TestPassword";

            userService.Create(new User { Name = username }, password);
            var user = userService.GetAll().SingleOrDefault();
            Assert.AreEqual(username, user.Name);
        }

        [Test]
        public void DeleteUser()
        {
            var userService = CreateUserService();
            var username = "TestUserName";
            var password = "TestPassword";

            userService.Create(new User { Name = username }, password);
            var user = userService.GetAll().SingleOrDefault();

            userService.Delete(user.Id);
            Assert.AreEqual(0, userService.GetAll().Count());
        }

        [Test]
        public void AuthenticateTest()
        {
            var userService = CreateUserService();
            var username = "TestUserName";
            var password = "TestPassword";

            userService.Create(new User { Name = username }, password);
            var user = userService.Authenticate(username, password);
            Assert.NotNull(user);
        }

        [Test]
        public void GetByIdTest()
        {
            var userService = CreateUserService();
            var username = "TestUserName";
            var password = "TestPassword";

            int id = 100;
            userService.Create(new User { Name = username, Id = id }, password);
            Assert.NotNull(userService.GetById(id));
        }

        [Test]
        public void GetAll()
        {
            var userService = CreateUserService();
            var username = "TestUserName";
            var password = "TestPassword";

            userService.Create(new User { Id = 100, Name = username }, password);
            userService.Create(new User { Id = 101, Name = username + 2 }, password + 2);

            Assert.AreEqual(2, userService.GetAll().Count());
        }
    }
}