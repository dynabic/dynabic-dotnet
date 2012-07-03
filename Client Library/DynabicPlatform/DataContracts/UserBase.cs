using System.Runtime.Serialization;

namespace DynabicPlatform.RestApiDataContract
{
    /// <summary>
    /// Dynabic User.
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "user")]
    public class UserBase
    {
        #region Data Members

        /// <summary>
        /// User's email address
        /// </summary>
        [DataMember(Name = "email", IsRequired = true)]
        public string Email { get; set; }

        /// <summary>
        /// User's first name
        /// </summary>
        [DataMember(Name = "first_name", IsRequired = true)]
        public string FirstName { get; set; }

        /// <summary>
        /// User's last name
        /// </summary>
        [DataMember(Name = "last_name", IsRequired = true)]
        public string LastName { get; set; }

        /// <summary>
        /// Specifies if the User is active or not
        /// </summary>
        [DataMember(Name = "active", IsRequired = true)]
        public bool Active { get; set; }

        /// <summary>
        /// Specifies if the User is marked as deleted
        /// </summary>
        [DataMember(Name = "deleted", IsRequired = true)]
        public bool Deleted { get; set; }

        /// <summary>
        /// Specifies if the User is subscribed to News Lettes
        /// </summary>
        [DataMember(Name = "is_subscribed_to_newsletter", IsRequired = true)]
        public bool IsSubscribedToNewsletter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "culture", IsRequired = false)]
        public string Culture { get; set; }

        /// <summary>
        /// User's Country
        /// </summary>
        [DataMember(Name = "country", IsRequired = false)]
        public string Country { get; set; }

        /// <summary>
        /// User's time zone
        /// </summary>
        [DataMember(Name = "timezone", IsRequired = false)]
        public string TimeZoneOffset { get; set; }

        /// <summary>
        /// User's Currency
        /// </summary>
        [DataMember(Name = "currency", IsRequired = false)]
        public string Currency { get; set; }

        /// <summary>
        /// User's Facebook email address
        /// </summary>
        [DataMember(Name = "facebook_id", IsRequired = false)]
        public string FacebookId { get; set; }

        /// <summary>
        /// User's Google apps. email address or username
        /// </summary>
        [DataMember(Name = "googleapps_user_name", IsRequired = false)]
        public string GoogleAppsUserName { get; set; }

        /// <summary>
        /// User's Yahoo email address
        /// </summary>
        [DataMember(Name = "yahoo_user_name", IsRequired = false)]
        public string YahooUserName { get; set; }

        /// <summary>
        /// A list of comma separated Roles
        /// </summary>
        [DataMember(Name = "user_roles", IsRequired = false)]
        public string UserRoles { get; set; }

        #endregion

        protected UserBase() { }
    }
}