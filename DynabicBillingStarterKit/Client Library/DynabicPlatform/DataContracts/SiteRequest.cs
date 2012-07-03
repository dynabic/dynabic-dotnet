using System.Runtime.Serialization;

namespace DynabicPlatform.RestApiDataContract
{
    /// <summary>
    /// Dynabic Site. Used in user's requests for Create and Update operations.
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "site_request")]
    public class SiteRequest : SiteBase
    {
    }
}