using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// A list of Product Families represented as a collection of ProductFamilyResponse objects
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "product_families")]
    public class ProductFamilyResponseList : Collection<ProductFamilyResponse>
    {
    }
}