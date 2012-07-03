using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// A Product Family used as User's request for Create or Update operations
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "product_family_request")]
    public class ProductFamilyRequest : ProductFamilyBase
    {
        #region Data Members

        /// <summary>
        /// A list of Products represented as a collection of ProductRequest objects
        /// </summary>
        [DataMember(Name = "products", IsRequired = false)]
        public ProductRequestList Products { get; set; }

        #endregion

        public ProductFamilyRequest()
        {
            this.Products = new ProductRequestList();
        }
    }
}