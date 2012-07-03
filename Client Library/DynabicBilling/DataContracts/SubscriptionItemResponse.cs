using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    [DataContract(Namespace = "v1.0", Name = "subscription_item_response")]
    public class SubscriptionItemResponse : SubscriptionItemBase
    {
        public SubscriptionItemResponse()
        {
            this.ChangesHistory = new SubscriptionItemHistoryResponseList();
        }

        [DataMember(Name = "update_history", IsRequired = true)]
        public SubscriptionItemHistoryResponseList ChangesHistory { get; set; }

        #region Response -> Request conversion

        public static implicit operator SubscriptionItemRequest(SubscriptionItemResponse response)
        {
            if (response == null) return null;
            return new SubscriptionItemRequest
            {
                ProductItemId = response.ProductItemId,
                Quantity = response.Quantity,
                SubscriptionId = response.SubscriptionId,
            };
        }

        #endregion Response -> Request conversion

    }
}
