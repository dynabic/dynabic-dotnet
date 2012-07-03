using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace DynabicPlatform.RestApiDataContract
{
    /// <summary>
    /// A list of Sites represented as a collection of SiteResponse objects
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "sites")]
    public class SitesList : Collection<SiteResponse>
    {
    }
}