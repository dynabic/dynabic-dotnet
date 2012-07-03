using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// A list of ProductMeteredPrices represented as a collection of ProductMeteredPriceResponse objects
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "metered_prices")]
    public class ProductMeteredPriceResponseList : Collection<ProductMeteredPriceResponse>
    {
        public static implicit operator ProductMeteredPriceRequestList(ProductMeteredPriceResponseList response)
        {
            var list = new ProductMeteredPriceRequestList();
            if (response != null)
                response.ToList().ForEach(i => list.Add(i));
            return list;
        }
    }

    /// <summary>
    /// A list of ProductMeteredPrices represented as a collection of ProductMeteredPriceRequest objects
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "metered_prices_request_list")]
    public class ProductMeteredPriceRequestList : Collection<ProductMeteredPriceRequest>
    {
    }
}