using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// A list of Customers represented as a collection of CustomerResponse objects
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "customers")]
    public class CustomersList : Collection<CustomerResponse>
    {
    }
}