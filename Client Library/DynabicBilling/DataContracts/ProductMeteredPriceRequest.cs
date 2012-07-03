using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// A  list of Product Metered Prices represented as a collection of ProductMeteredPriceRequest.
    /// Is used for metered products. It will contain information about the 
    /// minimum value and the maximum value within the metered product may 
    /// take values. It will also contain information about price per unit.
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "metered_price_request")]
    public class ProductMeteredPriceRequest : ProductMeteredPriceBase
    {
    }
}
