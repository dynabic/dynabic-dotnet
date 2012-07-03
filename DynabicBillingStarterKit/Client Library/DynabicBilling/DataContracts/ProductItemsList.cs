using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// A list of ProductItems represented as a collection of ProductItemResponse objects.
    /// It is used as response to user's request for Read operations
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "product_items")]
    public class ProductItemResponseList : Collection<ProductItemResponse>
    {
        public static implicit operator ProductItemRequestList(ProductItemResponseList response)
        {
            var list = new ProductItemRequestList();
            if (response != null)
                response.ToList().ForEach(i => list.Add(i));
            return list;
        }
    }

    /// <summary>
    /// A list of ProductItems represented as a collection of ProductItemRequest objects.
    /// It is used as user's request to Create or Update a list of ProductItems
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "product_items_request_list")]
    public class ProductItemRequestList : Collection<ProductItemRequest>
    {
    }
}