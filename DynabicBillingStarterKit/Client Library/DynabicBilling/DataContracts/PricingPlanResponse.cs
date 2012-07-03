using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    [DataContract(Namespace = "v1.0", Name = "pricing_plan_response")]
    public class PricingPlanResponse : PricingPlanBase
    {
        #region Data Members

        /// <summary>
        /// Id of the Product to which PricingPlan is attached
        /// </summary>
        [DataMember(Name = "product_id", IsRequired = true)]
        public int ProductId { get; set; }

        /// <summary>
        /// A list of PricingPlan schedules
        /// </summary>
        [DataMember(Name = "pricing_plan_payment_schedules", IsRequired = false)]
        public ProductPricingPlanPaymentScheduleResponseList PaymentScheduleList { get; set; }

        /// <summary>
        /// A list ProductItems represented as a collection of ProductItemResponse objects
        /// </summary>
        [DataMember(Name = "product_items", IsRequired = false)]
        public ProductItemResponseList ProductItemsList { get; set; }

        #endregion Data Members

        public PricingPlanResponse()
        {
            this.PaymentScheduleList = new ProductPricingPlanPaymentScheduleResponseList();
            this.ProductItemsList = new ProductItemResponseList();
        }

        public static implicit operator PricingPlanRequest(PricingPlanResponse response)
        {
            if (response == null) return null;
            return new PricingPlanRequest
            {
                CurrencyCode = response.CurrencyCode,
                Name = response.Name,
                PaymentScheduleList = response.PaymentScheduleList,
                ProductItemsList = response.ProductItemsList,
                TrialPeriodCharge = response.TrialPeriodCharge,
                TrialPeriodDurationDays = response.TrialPeriodDurationDays,
                UpfrontCharge = response.UpfrontCharge,
            };
        }
    }
}
