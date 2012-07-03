using DynabicPlatform.Classes;
using DynabicPlatform.RestApiDataContract;
using NUnit.Framework;

namespace DynabicPlatform.Tests
{
    public class UserServiceTests : AssertionHelper
    {
        private PlatformGateway _gateway;

        [SetUp]
        public void Init()
        {
#if DEBUG
            _gateway = new PlatformGateway(PlatformEnvironment.DEVELOPMENT, Constants.PUBLIC_KEY, Constants.PRIVATE_KEY);
#else
            _gateway = new PlatformGateway(PlatformEnvironment.QA, Constants.PUBLIC_KEY, Constants.PRIVATE_KEY);
#endif
        }

        #region Helpers

        private UserResponse AddUser()
        {
            var newUser = new UserRequest();
            newUser.Email = "sergey.slavin@dynabic.com";
            newUser.FacebookId = "FacebookId";
            newUser.GoogleAppsUserName = "GoogleAppsUserName";
            newUser.FirstName = "FirstName";
            newUser.LastName = "LastName";
            newUser.YahooUserName = "YahooUserName";
            newUser.PasswordHash = "test123";
            newUser.Active = true;
            newUser.Deleted = false;

            return _gateway.Users.AddUser(newUser);
        }

        private void DeleteUser(int id)
        {
            _gateway.Users.DeleteUser(id.ToString());
        }

        #endregion Helpers

        [Test]
        public void GetAllUsers()
        {
            var users = _gateway.Users.GetAllUsers();
            Assert.IsNotNull(users);
        }

        [Test]
        public void GetUserById()
        {
            var newUser = AddUser();
            Assert.IsNotNull(newUser);
            try
            {
                var user = _gateway.Users.GetUserById(newUser.Id.ToString());
                Assert.IsNotNull(user);
                Assert.AreEqual(newUser.Id, user.Id);
            }
            finally
            {
                DeleteUser(newUser.Id);
            }
        }

        [Test]
        public void GetUserByUserName()
        {
            var newUser = AddUser();
            Assert.IsNotNull(newUser);
            try
            {
                var user = _gateway.Users.GetUserByUserName(newUser.Email);
                Assert.IsNotNull(user);
                Assert.AreEqual(newUser.Id, user.Id);
            }
            finally
            {
                DeleteUser(newUser.Id);
            }
        }

        [Test]
        public void AddAndDeleteUser()
        {
            var newUser = AddUser();
            Assert.IsNotNull(newUser);
            DeleteUser(newUser.Id);
        }

        [Test]
        public void UpdateUser()
        {
            var newUser = AddUser();
            Assert.IsNotNull(newUser);
            try
            {
                // get existing user
                var user = _gateway.Users.GetUserById(newUser.Id.ToString());
                Assert.IsNotNull(user);
                Assert.AreEqual(newUser.Id, user.Id);
                // update user
                var updateValue = "test user update";
                user.FirstName = updateValue;
                var updatedUser = _gateway.Users.UpdateUser(user, user.Id.ToString());
                Assert.IsNotNull(updatedUser);
                Assert.AreEqual(updateValue, updatedUser.FirstName);
            }
            finally
            {
                DeleteUser(newUser.Id);
            }
        }

        [Test]
        public void SetDeletedStatus()
        {
            var newUser = AddUser();
            Assert.IsNotNull(newUser);
            try
            {
                // get existing user
                var user = _gateway.Users.GetUserById(newUser.Id.ToString());
                Assert.IsNotNull(user);
                Assert.AreEqual(newUser.Id, user.Id);

                // change deleted status
                _gateway.Users.SetDeletedStatus(user.Id.ToString(), "true");

                var updatedUser = _gateway.Users.GetUserById(user.Id.ToString());
                Assert.IsNotNull(updatedUser);
                Assert.IsTrue(updatedUser.Deleted);
            }
            finally
            {
                DeleteUser(newUser.Id);
            }
        }

        [Test]
        public void SetActiveStatus()
        {
            var newUser = AddUser();
            Assert.IsNotNull(newUser);
            try
            {
                // get existing user
                var user = _gateway.Users.GetUserById(newUser.Id.ToString());
                Assert.IsNotNull(user);
                Assert.AreEqual(newUser.Id, user.Id);

                // change active status
                _gateway.Users.SetActiveStatus(user.Id.ToString(), "false");

                var updatedUser = _gateway.Users.GetUserById(user.Id.ToString());
                Assert.IsNotNull(updatedUser);
                Assert.IsFalse(updatedUser.Active);
            }
            finally
            {
                DeleteUser(newUser.Id);
            }
        }

        [Test]
        public void ResetPassword()
        {
            var newUser = AddUser();
            Assert.IsNotNull(newUser);
            try
            {
                // get existing user
                var user = _gateway.Users.GetUserById(newUser.Id.ToString());
                Assert.IsNotNull(user);
                Assert.AreEqual(newUser.Id, user.Id);

                // reset password
                _gateway.Users.ResetPassword(user.Id.ToString());
            }
            finally
            {
                DeleteUser(newUser.Id);
            }
        }

        [Test]
        public void ModifyPassword()
        {
            var newUser = AddUser();
            Assert.IsNotNull(newUser);
            try
            {
                _gateway.Users.ModifyPassword(newUser.Id.ToString(), "test123", "newpwd");
            }
            finally
            {
                DeleteUser(newUser.Id);
            }
        }

        [Test]
        public void SetRoles()
        {
            var newUser = AddUser();
            Assert.IsNotNull(newUser);
            try
            {
                // get existing user
                var user = _gateway.Users.GetUserById(newUser.Id.ToString());
                Assert.IsNotNull(user);
                Assert.AreEqual(newUser.Id, user.Id);

                // remove all roles
                _gateway.Users.SetRoles(user.Id.ToString(), string.Empty);

                user = _gateway.Users.GetUserById(user.Id.ToString());
                Assert.IsNotNull(user);
                Assert.IsEmpty(user.UserRoles);

                // grant user to two roles
                _gateway.Users.SetRoles(user.Id.ToString(), "user,admin");
                user = _gateway.Users.GetUserById(user.Id.ToString());
                Assert.IsNotNull(user);
                Assert.AreEqual(user.UserRoles, "Admin,User");
            }
            finally
            {
                DeleteUser(newUser.Id);
            }
        }
    }
}
