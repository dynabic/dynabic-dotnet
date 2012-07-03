using System.Runtime.Serialization;

namespace DynabicPlatform.RestApiDataContract
{
    /// <summary>
    /// Dynabic Site. Used as response to user's requests.
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "site_response")]
    public class SiteResponse : SiteBase
    {
        #region Data members

        /// <summary>
        /// Site's unique identifier. It is generated and managed by database
        /// </summary>
        [DataMember(Name = "id", IsRequired = true)]
        public int Id { get; set; }

        /// <summary>
        /// Total amount of revenues from the Site
        /// </summary>
        [DataMember(Name = "revenue", IsRequired = true)]
        public double Revenue { get; set; }

        /// <summary>
        /// Id of the Company to which the Site belongs to
        /// </summary>
        [DataMember(Name = "company_id", IsRequired = true)]
        public int CompanyInfoId { get; set; }

        #endregion

        public SiteResponse() { }
        public static implicit operator SiteRequest(SiteResponse response)
        {
            if (response == null) return null;
            return new SiteRequest
            {
                IsTestMode = response.IsTestMode,
                Name = response.Name,
                Subdomain = response.Subdomain,
            };
        }
    }
}