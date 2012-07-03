using DynabicPlatform.Classes;
using DynabicPlatform.RestAPI.RestInterfaces;
using DynabicPlatform.RestApiDataContract;

namespace DynabicPlatform.Services
{
    public class UserService : IUsersService
    {
        private readonly CommunicationLayer _service;
        private readonly string _gatewayURL;

        public UserService(CommunicationLayer service)
        {
            _service = service;
            _gatewayURL = service.Environment.GatewayURL + "/users";
        }

        /// <summary>
        /// Gets all Users in a Company
        /// </summary>
        /// <param name="format">The format of the Response</param>
        /// <param name="pageNumber">The page number (Optional parameter, if is specified should be equal or greater than 1).</param>
        /// <param name="pageSize">Size of the page (Optional parameter, if is specified should be equal or greater than 1).</param>
        /// <returns>A UsersList object containing all users in the Company</returns>
        public UsersList GetAllUsers(string format = ContentFormat.XML, string pageNumber = null, string pageSize = null)
        {
            return _service.Get<UsersList>(string.Format("{0}/{1}?pageNumber={2}&pageSize={3}", _gatewayURL, format, pageNumber, pageSize));
        }

        /// <summary>
        /// Gets a User by Id
        /// </summary>
        /// <param name="userId">The Id of the User</param>
        /// <param name="format">The format of the Response</param>
        /// <returns>A UserResponse object that corresponds to the specified Id</returns>
        public UserResponse GetUserById(string userId, string format = ContentFormat.XML)
        {
            return _service.Get<UserResponse>(string.Format("{0}/{1}.{2}", _gatewayURL, userId, format));
        }

        /// <summary>
        /// Gets the user by UserName.
        /// </summary>
        /// <param name="userName">
        /// The userName may be: E-mail, Google apps. UserName, Yahoo UserName or Facebook Id.
        /// </param>
        /// <param name="format">The format of the Response</param>
        /// <returns>
        /// A UserResponse object that corresponds to the specified UserName
        /// </returns>
        public UserResponse GetUserByUserName(string userName, string format = ContentFormat.XML)
        {
            return _service.Get<UserResponse>(string.Format("{0}/byname/[{1}].{2}", _gatewayURL, userName, format));
        }

        /// <summary>
        /// Adds a new User
        /// </summary>
        /// <param name="user">The User to be added</param>
        /// <param name="format">The format of the Response</param>
        /// <returns>The UserResponse object that corresponds to the newly-added User</returns>
        public UserResponse AddUser(UserRequest user, string format = ContentFormat.XML)
        {
            return _service.Post<UserRequest, UserResponse>(string.Format("{0}/{1}", _gatewayURL, format), user);
        }

        /// <summary>
        /// Updates an existing User
        /// </summary>
        /// <param name="updatedUser">The User to be updated</param>
        /// <param name="userId">The user id.</param>
        /// <param name="format">The format of the Response</param>
        /// <returns>
        /// A UserResponse object that corresponds to the updated User
        /// </returns>
        public UserResponse UpdateUser(UserRequest updatedUser, string userId, string format = ContentFormat.XML)
        {
            return _service.Put<UserRequest, UserResponse>(string.Format("{0}/{1}.{2}", _gatewayURL, userId, format), updatedUser);
        }

        /// <summary>
        /// Deletes a User
        /// </summary>
        /// <param name="userId">The Id of the user to be deleted</param>
        public void DeleteUser(string userId)
        {
            _service.Delete(string.Format("{0}/{1}", _gatewayURL, userId));
        }

        /// <summary>
        /// Mark a user as deleted or undelited (but does not actually delete it from the database).
        /// </summary>
        /// <param name="userId">The Id of the User.</param>
        /// <param name="status">The deleted status.</param>
        public void SetDeletedStatus(string userId, string status)
        {
            _service.Put(string.Format("{0}/{1}/delete-status/{2}", _gatewayURL, userId, status));
        }

        /// <summary>
        /// Activates of Deactivates a user
        /// </summary>
        /// <param name="userId">The Id of the User.</param>
        /// <param name="status">The active status.</param>
        public void SetActiveStatus(string userId, string status)
        {
            _service.Put(string.Format("{0}/{1}/active-status/{2}", _gatewayURL, userId, status));
        }

        /// <summary>
        /// Resets the user password.
        /// </summary>
        /// <param name="userId">The Id of the User.</param>
        public void ResetPassword(string userId)
        {
            _service.Delete(string.Format("{0}/{1}/password", _gatewayURL, userId));
        }

        /// <summary>
        /// Modifies the user password.
        /// </summary>
        /// <param name="userId">The Id of the User.</param>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        public void ModifyPassword(string userId, string oldPassword, string newPassword)
        {
            _service.Put(string.Format("{0}/{1}/new-pwd/{2}/{3}", _gatewayURL, userId, oldPassword, newPassword));
        }

        /// <summary>
        /// Sets the roles to user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="roles">The comma separated list of the roles.</param>
        public void SetRoles(string userId, string roles)
        {
            _service.Put(string.Format("{0}/{1}/set-roles?roles={2}", _gatewayURL, userId, roles));
        }


        /// <summary>
        /// Gets the user API keys.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="format">The format of the Response.</param>
        /// <returns></returns>
        public UserApiKeysResponse GetUserApiKeys(string userId, string format = ContentFormat.XML)
        {
            return _service.Get<UserApiKeysResponse>(string.Format("{0}/apikeys/{1}.{2}", _gatewayURL, userId, format));
        }

        /// <summary>
        /// Deletes the API keys.
        /// </summary>
        /// <param name="userId">The user id.</param>
        public void DeleteApiKeys(string userId)
        {
            _service.Delete(string.Format("{0}/{1}/disable-api-access", _gatewayURL, userId));
        }

        /// <summary>
        /// Generates the new API keys.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="format">The format of the Response.</param>
        /// <returns></returns>
        public UserApiKeysResponse GenerateNewApiKeys(string userId, string format = ContentFormat.XML)
        {
            return _service.Put<UserApiKeysResponse>(string.Format("{0}/enable-api-access/{1}.{2}", _gatewayURL, userId, format));
        }
    }
}
