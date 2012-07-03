using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace DynabicPlatform.RestApiDataContract
{
    [CollectionDataContract(Namespace = "v1.0", Name = "applications_in_company")]
    public class ApplicationsInCompanyList : Collection<ApplicationInCompanyResponse>
    {
    }

}
