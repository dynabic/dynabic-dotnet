using System.Runtime.Serialization;

namespace DynabicPlatform.RestApiDataContract
{
    [DataContract(Namespace = "v1.0", Name = "site")]
    public class SiteBase
    {
        #region Data Members

        /// <summary>
        /// Site's name. Used to distinguish Site's in UI
        /// </summary>
        [DataMember(Name = "name", IsRequired = true)]
        public string Name { get; set; }

        /// <summary>
        /// Site's subdomain. Used as subdomain name in URL to access the site
        /// </summary>
        [DataMember(Name = "subdomain", IsRequired = true)]
        public string Subdomain { get; set; }

        /// <summary>
        /// Specifies if the Site is in Test or Production mode
        /// </summary>
        [DataMember(Name = "is_test_mode", IsRequired = true)]
        public bool IsTestMode { get; set; }

        #endregion
    }
}
