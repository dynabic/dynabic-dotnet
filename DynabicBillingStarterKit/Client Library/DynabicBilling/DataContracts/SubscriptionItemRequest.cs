using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    [DataContract(Namespace = "v1.0", Name = "subscription_item_request")]
    public class SubscriptionItemRequest : SubscriptionItemBase
    {
        /// <summary>
        /// The update details
        /// </summary>
        [DataMember(Name = "description", IsRequired = false)]
        public string UpdateDescription { get; set; }
    }
}
