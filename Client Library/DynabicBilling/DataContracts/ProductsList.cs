using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// A list of Products represented as a collection of ProductResponse objects
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "products")]
    public class ProductResponseList : Collection<ProductResponse>
    {
        public static implicit operator ProductRequestList(ProductResponseList response)
        {
            var list = new ProductRequestList();
            if (response != null)
                response.ToList().ForEach(i => list.Add(i));
            return list;
        }
    }

    /// <summary>
    /// A list of Products represented as a collection of ProductRequest objects
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "products_request_list")]
    public class ProductRequestList : Collection<ProductRequest>
    {
    }
}