using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// ProductItemsused as response to user's requests
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "product_item_response")]
    public class ProductItemResponse : ProductItemBase
    {
        #region Data Members

        /// <summary>
        /// Id of the PricingPlan Assigned to this ProductItem
        /// </summary>
        [DataMember(Name = "pricing_plan_id", IsRequired = false)]
        public int ProductPricingPlanId { get; set; }

        /// <summary>
        /// A list of product's metered prices represented as a collection of ProductMeteredPriceResponse objects
        /// </summary>
        [DataMember(Name = "metered_prices", IsRequired = false)]
        public ProductMeteredPriceResponseList MeteredPriceList { get; set; }

        /// <summary>
        /// A list of children ProductItems represented as a collection of ProductItemResponse objects
        /// </summary>
        [DataMember(Name = "product_item_children", IsRequired = false)]
        public ProductItemResponseList Children { get; set; }

        #endregion Data Members

        public ProductItemResponse()
        {
            this.MeteredPriceList = new ProductMeteredPriceResponseList();
            this.Children = new ProductItemResponseList();
        }

        public static implicit operator ProductItemRequest(ProductItemResponse response)
        {
            if (response == null) return null;
            return new ProductItemRequest
            {
                ChargeModel = response.ChargeModel,
                Children = response.Children,
                Description = response.Description,
                MeteredPriceList = response.MeteredPriceList,
                Name = response.Name,
                ProductItemId = response.ProductItemId,
            };
        }
    }
}
