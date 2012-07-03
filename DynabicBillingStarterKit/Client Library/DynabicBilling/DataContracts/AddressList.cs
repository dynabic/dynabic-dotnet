using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// A list of Addresses represented as a sequence of AddressResponse objects.
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "addresses")]
    public class AddressList : Collection<AddressResponse>
    {
    }
}