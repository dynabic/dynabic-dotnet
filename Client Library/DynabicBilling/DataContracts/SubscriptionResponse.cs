using System;
using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    [DataContract(Namespace = "v1.0", Name = "subscription_response")]
    public class SubscriptionResponse : SubscriptionBase
    {
        #region Data Members

        [DataMember(Name = "service_result", IsRequired = false)]
        public ServiceResult ServiceResult { set; get; }

        /// <summary>
        /// To be used for new credit card
        /// </summary>
        [DataMember(Name = "credit_card", IsRequired = false)]
        public CreditCardResponse CreditCard { set; get; }

        /// <summary>
        /// To be used for new customer
        /// </summary>
        [DataMember(Name = "customer", IsRequired = false)]
        public CustomerResponse Customer { set; get; }

        /// <summary>
        /// To be used for new address
        /// </summary>
        [DataMember(Name = "billing_address", IsRequired = false)]
        public AddressResponse BillingAddress { set; get; }

        [DataMember(Name = "id", IsRequired = false)]
        public int Id { set; get; }

        [DataMember(Name = "signup_date", IsRequired = false)]
        public DateTime SignupDate { set; get; }

        [DataMember(Name = "is_trial", IsRequired = false)]
        public bool IsTrial { set; get; }

        /// <summary>
        /// Gets or sets the actual cancellation date
        /// </summary>
        [DataMember(Name = "cancellation_date", IsRequired = false)]
        public DateTime? CancellationDate { get; set; }

        /// <summary>
        /// The total Revenue for the Subscription
        /// </summary>
        [DataMember(Name = "revenue", IsRequired = false)]
        public Decimal? Revenue { get; set; }

        /// <summary>
        /// When the subscription will be evaluated again, the new product 
        /// will become the NextProduct and the new invoices will be emitted 
        /// based on the newly set Product
        /// </summary>
        [DataMember(Name = "next_product", IsRequired = false)]
        public ProductResponse NextProduct { set; get; }

        /// <summary>
        /// Product for which the Customer subscribed.
        /// If a ProductId is provided, this Product will be ignored
        /// </summary>
        [DataMember(Name = "product", IsRequired = false)]
        public ProductResponse Product { set; get; }

        /// <summary>
        /// When the subscription will be evaluated again, the new ProductPricingPlan 
        /// will become the NextProductPricingPlan
        /// </summary>
        [DataMember(Name = "next_product_pricing_plan", IsRequired = false)]
        public PricingPlanResponse NextProductPricingPlan { set; get; }

        /// <summary>
        /// Currently set ProductPricingPlan used to evaluate the Subscription
        /// </summary>
        [DataMember(Name = "product_pricing_plan", IsRequired = false)]
        public PricingPlanResponse ProductPricingPlan { set; get; }

        /// <summary>
        /// Actual status of the Subscription
        /// Active = 1,
        /// Trialing = 2,
        /// ConfigurationError = 4,
        /// BillNotPaidOnTimeRetrying = 8,
        /// BillNotPaid = 16,
        /// Expired = 32,
        /// Cancelled = 64,
        /// CreditCardInvalid = 128,
        /// </summary>
        [DataMember(Name = "status", IsRequired = false)]
        public SubscriptionStatus Status { set; get; }

        ///// <summary>
        ///// Gets or sets the subscription items.
        ///// </summary>
        //[DataMember(Name = "subscription_items", IsRequired = false)]
        //public SubscriptionItemResponseList SubscriptionItems { set; get; }


        #endregion

        public SubscriptionResponse()
        {
            this.SignupDate = DateTime.MinValue.ToUniversalTime();
            this.ServiceResult = new ServiceResult();
            this.Status = SubscriptionStatus.ConfigurationError;
            //this.SubscriptionItems = new SubscriptionItemResponseList();
        }

        #region Response -> Request conversion

        public static implicit operator SubscriptionRequest(SubscriptionResponse response)
        {
            if (response == null) return null;
            return new SubscriptionRequest
            {
                BillingAddress = response.BillingAddress,
                BillingAddressId = response.BillingAddressId,
                CancellationDetails = response.CancellationDetails,
                CreditCard = response.CreditCard,
                CreditCardId = response.CreditCardId,
                Currency = response.Currency,
                CurrentBallance = response.CurrentBallance,
                Customer = response.Customer,
                CustomerId = response.CustomerId,
                EndDate = response.EndDate,
                IsCancelledAtEndOfPeriod = response.IsCancelledAtEndOfPeriod,
                NextAssesment = response.NextAssesment,
                NextProduct = response.NextProduct,
                NextProductPricingPlan = response.NextProductPricingPlan,
                Product = response.Product,
                ProductId = response.ProductId,
                ProductPricingPlan = response.ProductPricingPlan,
                ProductPricingPlanId = response.ProductPricingPlanId,
                StartDate = response.StartDate,
                //SubscriptionItems = response.SubscriptionItems,
            };
        }

        #endregion Response -> Request conversion
    }
}