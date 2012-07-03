using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// Address used for user's requests. Cointains basic information about an Address
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "address_request")]
    public class AddressRequest : AddressBase
    {
    }
}