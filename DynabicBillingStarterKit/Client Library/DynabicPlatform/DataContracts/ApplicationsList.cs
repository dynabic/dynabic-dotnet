using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace DynabicPlatform.RestApiDataContract
{
    [CollectionDataContract(Namespace = "v1.0", Name = "applications")]
    public class ApplicationsList : Collection<ApplicationResponse>
    {
    }
}
