using System;
using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "subscription")]
    public class SubscriptionBase
    {
        #region Data Members

        /// <summary>
        /// Id of the Product for which the Customer subscribed
        /// </summary>
        [DataMember(Name = "product_id", IsRequired = true)]
        public int ProductId { set; get; }

        /// <summary>
        /// Id of the PricingPlan selected in the Subscription
        /// </summary>
        [DataMember(Name = "pricing_plan_id", IsRequired = true)]
        public int ProductPricingPlanId { set; get; }

        /// <summary>
        /// Id of a CreditCard.
        /// To be used with existing CreditCard
        /// </summary>
        [DataMember(Name = "credit_card_id", IsRequired = false)]
        public int? CreditCardId { get; set; }

        /// <summary>
        /// Id of the Customer.
        /// To be used with existing Customer
        /// </summary>
        [DataMember(Name = "customer_id", IsRequired = false)]
        public int CustomerId { get; set; }

        /// <summary>
        /// Id of an Address.
        /// To be used with existing Address
        /// </summary>
        [DataMember(Name = "billing_address_id", IsRequired = false)]
        public int BillingAddressId { get; set; }

        /// <summary>
        /// Date when the assessment of the Subscription starts
        /// </summary>
        [DataMember(Name = "start_date", IsRequired = false)]
        public DateTime StartDate { set; get; }

        /// <summary>
        /// Date when the Subscription expires
        /// </summary>
        [DataMember(Name = "end_date", IsRequired = false)]
        public DateTime? EndDate { set; get; }

        /// <summary>
        /// Date when the next assessment is scheduled
        /// </summary>
        [DataMember(Name = "next_assesment", IsRequired = false)]
        public DateTime? NextAssesment { set; get; }

        /// <summary>
        /// Represents the Subscription's current ballance
        /// </summary>
        [DataMember(Name = "current_ballance", IsRequired = false)]
        public Decimal CurrentBallance { set; get; }

        /// <summary>
        /// Currency used in Subscription for issuing bills, statements and receips
        /// </summary>
        [DataMember(Name = "currency", IsRequired = false)]
        public Currency Currency { set; get; }

        /// <summary>
        /// Indicates whether the subscription is to be 
        /// cancelled at the end of the current period
        /// </summary>
        [DataMember(Name = "is_cancelled_at_end_of_period", IsRequired = false)]
        public bool? IsCancelledAtEndOfPeriod { get; set; }

        /// <summary>
        /// Cancellation details(reason) provided by the customer
        /// </summary>
        [DataMember(Name = "cancellation_details", IsRequired = false)]
        public string CancellationDetails { get; set; }

        #endregion

        protected SubscriptionBase()
        {
            this.StartDate = DateTime.MinValue.ToUniversalTime();
        }
    }
}