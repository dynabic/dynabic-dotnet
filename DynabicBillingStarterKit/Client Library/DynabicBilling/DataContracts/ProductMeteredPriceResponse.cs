using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// A  list of Product Metered Prices represented as a collection of ProductMeteredPriceResponse
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "metered_price_response")]
    public class ProductMeteredPriceResponse : ProductMeteredPriceBase
    {
        #region Data Members

        /// <summary>
        /// Id of the child ProductItem for which MeteredProduct is attached
        /// </summary>
        [DataMember(Name = "product_item_id", IsRequired = true)]
        public int ProductItemId { get; set; }

        #endregion Data Members

        public static implicit operator ProductMeteredPriceRequest(ProductMeteredPriceResponse response)
        {
            if (response == null) return null;
            return new ProductMeteredPriceRequest
            {
                Description = response.Description,
                EndQuantity = response.EndQuantity,
                Id = response.Id,
                StartQuantity = response.StartQuantity,
                UnitPrice = response.UnitPrice,
            };
        }
    }
}
