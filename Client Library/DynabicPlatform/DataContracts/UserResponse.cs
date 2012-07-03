using System;
using System.Runtime.Serialization;

namespace DynabicPlatform.RestApiDataContract
{
    /// <summary>
    /// Dynabic User. Used in reaponse to user's request
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "user_response")]
    public class UserResponse : UserBase
    {
        #region Data Members

        /// <summary>
        /// User's unique identifier. It is generated and managed by the database
        /// </summary>
        [DataMember(Name = "id", IsRequired = true)]
        public int Id { get; set; }

        /// <summary>
        /// Date when the User was registered at Dynabic
        /// </summary>
        [DataMember(Name = "registration_date", IsRequired = false)]
        public DateTime RegistrationDate { get; set; }

        [DataMember(Name = "public_key")]
        public string PublicKey { get; set; }

        [DataMember(Name = "private_key")]
        public string PrivateKey { get; set; }

        #endregion

        public UserResponse()
        {
            this.RegistrationDate = DateTime.MinValue.ToUniversalTime();
        }

        public static implicit operator UserRequest(UserResponse response)
        {
            if (response == null) return null;
            return new UserRequest
            {
                Active = response.Active,
                Country = response.Country,
                Culture = response.Culture,
                Currency = response.Currency,
                Deleted = response.Deleted,
                Email = response.Email,
                FacebookId = response.FacebookId,
                FirstName = response.FirstName,
                GoogleAppsUserName = response.GoogleAppsUserName,
                IsSubscribedToNewsletter = response.IsSubscribedToNewsletter,
                LastName = response.LastName,
                TimeZoneOffset = response.TimeZoneOffset,
                UserRoles = response.UserRoles,
                YahooUserName = response.YahooUserName,
            };
        }
    }
}