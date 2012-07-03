using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// A list of PricingPlans represented as a collection of PricingPlanResponse objects
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "pricing_plans")]
    public class PricingPlanResponseList : Collection<PricingPlanResponse>
    {
        public static implicit operator PricingPlanRequestList(PricingPlanResponseList response)
        {
            var list = new PricingPlanRequestList();
            if (response != null)
                response.ToList().ForEach(i => list.Add(i));
            return list;
        }
    }

    /// <summary>
    /// A list of PricingPlans represented as a collection of PricingPlanRequest objects
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "pricing_plans_request_list")]
    public class PricingPlanRequestList : Collection<PricingPlanRequest>
    {
    }
}