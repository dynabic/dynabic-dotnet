using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    [DataContract(Namespace = "v1.0", Name = "subscription_item")]
    public class SubscriptionItemBase
    {
        /// <summary>
        /// The subscription identifier
        /// </summary>
        [DataMember(Name = "subscription_id", IsRequired = true)]
        public int SubscriptionId { get; set; }

        /// <summary>
        /// The product item identifier
        /// </summary>
        [DataMember(Name = "product_item_id", IsRequired = true)]
        public int ProductItemId { get; set; }

        /// <summary>
        /// The value of the quantity
        /// </summary>
        [DataMember(Name = "quantity", IsRequired = true)]
        public decimal Quantity { get; set; }
    }
}
