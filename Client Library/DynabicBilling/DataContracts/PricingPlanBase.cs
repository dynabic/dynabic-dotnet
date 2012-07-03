using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// Represents basic information about a PricingPlan
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "pricing_plan")]
    public class PricingPlanBase
    {
        #region Data Members

        /// <summary>
        /// Id of the PricingPlan record
        /// </summary>
        [DataMember(Name = "id", IsRequired = true)]
        public int Id { get; set; }

        /// <summary>
        /// Code of the Currency used in the PricingPlan
        /// </summary>
        [DataMember(Name = "currency_code", IsRequired = true)]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Name of the PricingPlan
        /// </summary>
        [DataMember(Name = "name", IsRequired = false)]
        public string Name { get; set; }

        /// <summary>
        /// The amount of money paid up-front the first assessment of a Subscription
        /// </summary>
        [DataMember(Name = "upfront_charge", IsRequired = false)]
        public decimal? UpfrontCharge { get; set; }

        /// <summary>
        /// The amount of money charged for the trial period
        /// </summary>
        [DataMember(Name = "trial_period_charge", IsRequired = false)]
        public decimal TrialPeriodCharge { get; set; }

        /// <summary>
        /// Number of days for trialing a Product
        /// </summary>
        [DataMember(Name = "trial_period_duration_days", IsRequired = false)]
        public int TrialPeriodDurationDays { get; set; }

        #endregion

        protected PricingPlanBase()
        {
        }
    }
}