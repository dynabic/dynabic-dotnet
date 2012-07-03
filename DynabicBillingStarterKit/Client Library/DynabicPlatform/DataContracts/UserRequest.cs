using System.Runtime.Serialization;

namespace DynabicPlatform.RestApiDataContract
{
    /// <summary>
    /// Dynabic User. Used in user's request for Create and Update operations
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "user_request")]
    public class UserRequest : UserBase
    {
        #region Data Members

        [DataMember(Name = "password")]
        public string PasswordHash { get; set; }

        #endregion

        public UserRequest() { }
    }
}