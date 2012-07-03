using DynabicPlatform.RestApiDataContract;

namespace DynabicPlatform.RestAPI.RestInterfaces
{
    public interface IUsersService
    {
        #region GET

        /// <summary>
        /// Retrieves all Users in a Company
        /// </summary>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <param name="pageNumber">
        /// Optional parameter to be used when a paged response is expected.
        /// Use it to specify the index of the page to be retrieved (this value has to be greater than or equal to 1).
        /// </param>
        /// <param name="pageSize">
        /// Optional parameter to be used when a paged response is expected.
        /// Use it to specify the number of records a page should contain (this value has to be greater than or equal to 1).
        /// </param>
        /// <returns>A UsersList object containing all Users in the Company</returns>
        UsersList GetAllUsers(string format = "xml", string pageNumber = null, string pageSize = null);

        /// <summary>
        /// Retrieves a User by it's unique identifier
        /// </summary>
        /// <param name="userId">The Id of the User</param>
        /// <param name="format">The format used for the data transfer (XML or JSON). </param>
        /// <returns>A UserResponse object that corresponds to the specified Id</returns>
        UserResponse GetUserById(string userId, string format = "xml");

        /// <summary>
        /// Retrieves a User by User.Name.
        /// </summary>
        /// <param name="userName"> The username may be: E-mail, Google apps. UserName, Yahoo UserName or Facebook Id. </param>
        /// <param name="format">The format used for the data transfer (XML or JSON). </param>
        /// <returns> A UserResponse object that corresponds to the specified username </returns>
        UserResponse GetUserByUserName(string userName, string format = "xml");

        /// <summary>
        /// Gets the user API keys.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        UserApiKeysResponse GetUserApiKeys(string userId, string format = "xml");

        #endregion

        #region POST

        /// <summary>
        /// Adds a new User
        /// </summary>
        /// <param name="user">The User to be added</param>
        /// <param name="format">The format used for the data transfer (XML or JSON). </param>
        /// <returns>The UserResponse object that corresponds to the newly-added User</returns>
        UserResponse AddUser(UserRequest user, string format = "xml");

        #endregion

        #region PUT

        /// <summary>
        /// Updates an existing User
        /// </summary>
        /// <param name="updatedUser">The User to be updated</param>
        /// <param name="userId">The User's unique identifier to be updated.</param>
        /// <param name="format">The format used for the data transfer (XML or JSON). </param>
        /// <returns> A UserResponse object that corresponds to the updated User </returns>
        UserResponse UpdateUser(UserRequest updatedUser, string userId, string format = "xml");

        #endregion

        #region DELETE

        /// <summary>
        /// Deletes a User
        /// </summary>
        /// <param name="userId">The Id of the user to be deleted</param>
        void DeleteUser(string userId);

        #endregion

        #region Misc

        /// <summary>
        /// Mark a User as deleted or undeleted (but does not actually delete it from the database).
        /// </summary>
        /// <param name="userId">The Id of the User.</param>
        /// <param name="status">The deleted status.</param>
        void SetDeletedStatus(string userId, string status);

        /// <summary>
        /// Activates or Deactivates a User
        /// </summary>
        /// <param name="userId">The Id of the User.</param>
        /// <param name="status">The active status.</param>
        void SetActiveStatus(string userId, string status);

        /// <summary>
        /// Resets the User's password.
        /// </summary>
        /// <param name="userId">The Id of the User.</param>
        void ResetPassword(string userId);

        /// <summary>
        /// Modifies the User's password.
        /// </summary>
        /// <param name="userId">The Id of the User.</param>
        /// <param name="oldPassword">The User's old password.</param>
        /// <param name="newPassword">The User's new password.</param>
        void ModifyPassword(string userId, string oldPassword, string newPassword);

        /// <summary>
        /// Sets the roles to user.
        /// </summary>
        /// <param name="userId">The Id of the User.</param>
        /// <param name="roles">The comma separated list of the roles.</param>
        void SetRoles(string userId, string roles);

        /// <summary>
        /// Deletes the API keys.
        /// </summary>
        /// <param name="userId">The user id.</param>
        void DeleteApiKeys(string userId);

        /// <summary>
        /// Generates the new API keys.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="format">The format.</param>
        UserApiKeysResponse GenerateNewApiKeys(string userId, string format = "xml");

        #endregion
    }
}
