using DynabicPlatform.Classes;
using DynabicPlatform.RestApiDataContract;
using NUnit.Framework;

namespace DynabicPlatform.Tests
{
    public class UserServiceTests : AssertionHelper
    {
        private PlatformGateway _gateway;
        private TestsHelper _testsHelper;
        private TestDataValues _testData;

        [SetUp]
        public void Init()
        {
#if DEBUG
            _gateway = new PlatformGateway(PlatformEnvironment.DEVELOPMENT, Constants.PUBLIC_KEY, Constants.PRIVATE_KEY);
#else
            _gateway = new PlatformGateway(PlatformEnvironment.QA, Constants.PUBLIC_KEY, Constants.PRIVATE_KEY);
#endif
            _testsHelper = new TestsHelper(_gateway);
            _testData = _testsHelper.PrepareUsersTestData();
        }

        [TearDown]
        public void Cleanup()
        {
            _testsHelper.CleanupTestData();
        }

        [Test]
        public void GetAllUsers()
        {
            var users = _gateway.Users.GetAllUsers();
            Assert.IsNotNull(users);
        }

        [Test]
        public void GetUserById()
        {
            var user = _gateway.Users.GetUserById(_testData.UserId.ToString());
            Assert.IsNotNull(user);
            Assert.AreEqual(_testData.UserId, user.Id);
        }

        [Test]
        public void GetUserByUserName()
        {
            var user = _gateway.Users.GetUserByUserName(_testData.UserEmail);
            Assert.IsNotNull(user);
            Assert.AreEqual(_testData.UserId, user.Id);
        }

        [Test]
        public void UpdateUser()
        {
            var user = _gateway.Users.GetUserById(_testData.UserId.ToString());
            Assert.IsNotNull(user);

            // update user
            var updateValue = "test user update";
            user.FirstName = updateValue;
            var updatedUser = _gateway.Users.UpdateUser(user, user.Id.ToString());
            Assert.IsNotNull(updatedUser);
            Assert.AreEqual(updateValue, updatedUser.FirstName);
        }

        [Test]
        public void SetDeletedStatus()
        {
            var user = _gateway.Users.GetUserById(_testData.UserId.ToString());
            Assert.IsNotNull(user);

            // change deleted status
            _gateway.Users.SetDeletedStatus(user.Id.ToString(), "true");

            var updatedUser = _gateway.Users.GetUserById(user.Id.ToString());
            Assert.IsNotNull(updatedUser);
            Assert.IsTrue(updatedUser.Deleted);
        }

        [Test]
        public void SetActiveStatus()
        {
            var user = _gateway.Users.GetUserById(_testData.UserId.ToString());
            Assert.IsNotNull(user);

            // change active status
            _gateway.Users.SetActiveStatus(user.Id.ToString(), "false");

            var updatedUser = _gateway.Users.GetUserById(user.Id.ToString());
            Assert.IsNotNull(updatedUser);
            Assert.IsFalse(updatedUser.Active);
        }

        [Test]
        public void ResetPassword()
        {
            var user = _gateway.Users.GetUserById(_testData.UserId.ToString());
            Assert.IsNotNull(user);

            // reset password
            _gateway.Users.ResetPassword(user.Id.ToString());
        }

        [Test]
        public void ModifyPassword()
        {
            _gateway.Users.ModifyPassword(_testData.UserId.ToString(), "test123", "newpwd123");
        }

        [Test]
        public void SetRoles()
        {
            // get existing user
            var user = _gateway.Users.GetUserById(_testData.UserId.ToString());
            Assert.IsNotNull(user);
            Assert.AreEqual(_testData.UserId, user.Id);

            // remove all roles. The roles field will be set to default "user" role
            _gateway.Users.SetRoles(user.Id.ToString(), string.Empty);

            user = _gateway.Users.GetUserById(user.Id.ToString());
            Assert.IsNotNull(user);
            
            // grant user to two roles
            _gateway.Users.SetRoles(user.Id.ToString(), "user,admin");
            user = _gateway.Users.GetUserById(user.Id.ToString());
            Assert.IsNotNull(user);
            Assert.AreEqual(user.UserRoles, "Admin,User");
        }
    }
}
