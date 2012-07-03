using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// A Product's pricing plan used as  user's request for Create or Update operations
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "pricing_plan_request")]
    public class PricingPlanRequest : PricingPlanBase
    {
        #region Data Members

        /// <summary>
        /// A list of Product's Pricing Plans payment schedules represented as a collection of ProductPricingPlanPaymentScheduleRequest objects
        /// </summary>
        [DataMember(Name = "pricing_plan_payment_schedules", IsRequired = false)]
        public ProductPricingPlanPaymentScheduleRequestList PaymentScheduleList { get; set; }

        /// <summary>
        /// A list of ProductItems represented as a collection of ProductItemRequest objects
        /// </summary>
        [DataMember(Name = "product_items", IsRequired = false)]
        public ProductItemRequestList ProductItemsList { get; set; }

        #endregion Data Members

        public PricingPlanRequest()
        {
            this.PaymentScheduleList = new ProductPricingPlanPaymentScheduleRequestList();
            this.ProductItemsList = new ProductItemRequestList();
        }
    }
}
