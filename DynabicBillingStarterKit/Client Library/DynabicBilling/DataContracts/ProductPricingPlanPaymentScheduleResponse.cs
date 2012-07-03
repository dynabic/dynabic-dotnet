using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// A product's pricing plan payment schedule. Used as response to user's requests
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "pricing_plan_payment_schedule_response")]
    public class ProductPricingPlanPaymentScheduleResponse : ProductPricingPlanPaymentScheduleBase
    {
        #region Data Members

        /// <summary>
        /// Id of the product pricing plan to which this payment schedule applies to
        /// </summary>
        [DataMember(Name = "pricing_plan_id")]
        public int ProductPricingPlanId { get; set; }

        #endregion Data Members

        public static implicit operator ProductPricingPlanPaymentScheduleRequest(ProductPricingPlanPaymentScheduleResponse response)
        {
            if (response == null) return null;
            return new ProductPricingPlanPaymentScheduleRequest
            {
                EndDate = response.EndDate,
                EndDateOffsetDays = response.EndDateOffsetDays,
                FrequencyInterval = response.FrequencyInterval,
                FrequencyRecurrenceFactor = response.FrequencyRecurrenceFactor,
                FrequencyRelativeInterval = response.FrequencyRelativeInterval,
                FrequencyType = response.FrequencyType,
                Id = response.Id,
                Name = response.Name,
                StartDate = response.StartDate,
                StartDateOffsetDays = response.StartDateOffsetDays,
                SubscriptionPeriodCharge = response.SubscriptionPeriodCharge,
            };
        }
    }
}
