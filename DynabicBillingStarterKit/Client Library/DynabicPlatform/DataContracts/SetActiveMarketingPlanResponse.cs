using System.Runtime.Serialization;

namespace DynabicPlatform.RestApiDataContract
{

    [DataContract(Namespace = "v1.0", Name = "activate_marketing_plan")]
    public class SetActiveMarketingPlanResponse
    {
        [DataMember(Name = "active_subscription_id", IsRequired = true)]
        public int ActiveSubscriptionId { get; set; }

        [DataMember(Name = "success", IsRequired = true)]
        public bool Success { get; set; }
    }
}
