using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// A lists of the subscription's components.
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "subscription_items_response_list")]
    public class SubscriptionItemResponseList : Collection<SubscriptionItemResponse>
    {
        public static implicit operator SubscriptionItemRequestList(SubscriptionItemResponseList response)
        {
            var list = new SubscriptionItemRequestList();
            if (response != null)
                response.ToList().ForEach(i => list.Add(i));
            return list;
        }
    }

    [CollectionDataContract(Namespace = "v1.0", Name = "subscription_item_update_history")]
    public class SubscriptionItemHistoryResponseList : Collection<SubscriptionItemHistoryResponse>
    {
    }

    [CollectionDataContract(Namespace = "v1.0", Name = "subscription_items_request_list")]
    public class SubscriptionItemRequestList : Collection<SubscriptionItemRequest>
    {
    }

}
