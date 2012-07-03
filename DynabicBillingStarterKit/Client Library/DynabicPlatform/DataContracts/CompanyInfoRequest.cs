using System.Runtime.Serialization;

namespace DynabicPlatform.RestApiDataContract
{
    /// <summary>
    /// Info about a Company used as requests from users
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "company_info_request")]
    public class CompanyInfoRequest : CompanyInfoBase
    {
    }
}
