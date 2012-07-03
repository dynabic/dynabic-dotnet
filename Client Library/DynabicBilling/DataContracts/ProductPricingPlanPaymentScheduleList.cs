using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// A list of product pricing plans payment schedules represented as a collection of ProductPricingPlanPaymentScheduleResponse objects
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "pricing_plan_payment_schedules")]
    public class ProductPricingPlanPaymentScheduleResponseList : Collection<ProductPricingPlanPaymentScheduleResponse>
    {
        public static implicit operator ProductPricingPlanPaymentScheduleRequestList(ProductPricingPlanPaymentScheduleResponseList response)
        {
            var list = new ProductPricingPlanPaymentScheduleRequestList();
            if (response != null)
                response.ToList().ForEach(i => list.Add(i));
            return list;
        }
    }

    /// <summary>
    /// A list of product pricing plans payment schedules represented as a collection of ProductPricingPlanPaymentScheduleRequest objects
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "pricing_plan_payment_schedules_request_list")]
    public class ProductPricingPlanPaymentScheduleRequestList : Collection<ProductPricingPlanPaymentScheduleRequest>
    {
    }
}