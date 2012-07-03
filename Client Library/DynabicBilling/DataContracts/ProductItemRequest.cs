using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// ProductItem used for user's requests for Create or Update operations.
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "product_item_request")]
    public class ProductItemRequest : ProductItemBase
    {
        #region Data Members

        /// <summary>
        /// A list of product's metered prices represented as a collection of ProductMeteredPriceRequest objects
        /// </summary>
        [DataMember(Name = "metered_prices", IsRequired = false)]
        public ProductMeteredPriceRequestList MeteredPriceList { get; set; }

        /// <summary>
        /// A list of Product Item represented as a collection of ProductItemRequest objects
        /// </summary>
        [DataMember(Name = "product_item_children", IsRequired = false)]
        public ProductItemRequestList Children { get; set; }

        #endregion Data Members

        public ProductItemRequest()
        {
            this.MeteredPriceList = new ProductMeteredPriceRequestList();
            this.Children = new ProductItemRequestList();
        }
    }
}
