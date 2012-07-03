using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// A list of Subscriptions represented as a collection of SubscriptionResponse objects
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "subscriptions")]
    public class SubscriptionsList : Collection<SubscriptionResponse>
    {
    }
}