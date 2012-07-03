using System.Runtime.Serialization;

namespace DynabicPlatform.RestApiDataContract
{
    [DataContract(Namespace = "v1.0", Name = "company_info_response")]
    public class CompanyInfoResponse : CompanyInfoBase
    {
        #region Data Members

        /// <summary>
        /// Company's unque identifier
        /// </summary>
        [DataMember(Name = "id", IsRequired = true)]
        public int Id { get; set; }

        #endregion

        public static implicit operator CompanyInfoRequest(CompanyInfoResponse response)
        {
            if (response == null) return null;
            return new CompanyInfoRequest
            {
                Country = response.Country,
                Culture = response.Culture,
                Currency = response.Currency,
                Email = response.Email,
                Name = response.Name,
                Phone = response.Phone,
                TimeZoneOffset = response.TimeZoneOffset,
            };
        }
    }
}
