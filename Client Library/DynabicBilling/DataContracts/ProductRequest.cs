using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// Represents a Product. Used as user's request.
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "product_request")]
    public class ProductRequest : ProductBase
    {
        #region Data Members

        /// <summary>
        /// A list of Pricing Plans represented as a collection of PricingPlanRequest objects
        /// </summary>
        [DataMember(Name = "pricing_plans", IsRequired = true)]
        public PricingPlanRequestList PricingPlans { set; get; }

        #endregion Data Members

        public ProductRequest()
        {
            this.PricingPlans = new PricingPlanRequestList();
        }
    }
}