using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "subscription_request")]
    public class SubscriptionRequest : SubscriptionBase
    {
        #region Data Members

        /// <summary>
        /// Credit Card. Used for new CreditCard
        /// </summary>
        [DataMember(Name = "credit_card", IsRequired = false)]
        public CreditCardRequest CreditCard { set; get; }

        /// <summary>
        /// Customer. Used for new Customer
        /// </summary>
        [DataMember(Name = "customer", IsRequired = false)]
        public CustomerRequest Customer { set; get; }

        /// <summary>
        /// Address. Used for new Address
        /// </summary>
        [DataMember(Name = "billing_address", IsRequired = false)]
        public AddressRequest BillingAddress { set; get; }

        /// <summary>
        /// When the subscription will be evaluated again, the new product 
        /// will become the NextProduct and the new invoices will be emitted 
        /// based on the newly set Product
        /// </summary>
        [DataMember(Name = "next_product", IsRequired = false)]
        public ProductRequest NextProduct { set; get; }

        /// <summary>
        /// Product for which the Customer subscribed.
        /// If a ProductId is provided, this Product will be ignored
        /// </summary>
        [DataMember(Name = "product", IsRequired = false)]
        public ProductRequest Product { set; get; }

        /// <summary>
        /// When the subscription will be evaluated again, the new ProductPricingPlan 
        /// will become the NextProductPricingPlan
        /// </summary>
        [DataMember(Name = "next_product_pricing_plan", IsRequired = false)]
        public PricingPlanRequest NextProductPricingPlan { set; get; }

        /// <summary>
        /// Currently set ProductPricingPlan used to evaluate the Subscription
        /// </summary>
        [DataMember(Name = "product_pricing_plan", IsRequired = false)]
        public PricingPlanRequest ProductPricingPlan { set; get; }

        /// <summary>
        /// Gets or sets the subscription items.
        /// </summary>
        [DataMember(Name = "subscription_items", IsRequired = false)]
        public SubscriptionItemRequestList SubscriptionItems { set; get; }

        #endregion

        public SubscriptionRequest()
        {
            this.SubscriptionItems = new SubscriptionItemRequestList();
        }
    }
}