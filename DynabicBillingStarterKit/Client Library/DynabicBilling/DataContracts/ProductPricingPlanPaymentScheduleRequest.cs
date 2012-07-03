using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// A product's pricing plan payment schedule. Used as user's request for Create or Update operations
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "pricing_plan_payment_schedule_request")]
    public class ProductPricingPlanPaymentScheduleRequest : ProductPricingPlanPaymentScheduleBase
    {
    }
}
