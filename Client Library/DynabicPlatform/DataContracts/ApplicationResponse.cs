using System.Runtime.Serialization;

namespace DynabicPlatform.RestApiDataContract
{
    /// <summary>
    /// An Dynabic Platform Application.
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "application")]
    public class ApplicationResponse
    {
        /// <summary>
        /// Application's unique identifier
        /// </summary>
        [DataMember(Name = "id", IsRequired = true)]
        public int Id { get; set; }

        /// <summary>
        /// Application's name
        /// </summary>
        [DataMember(Name = "name", IsRequired = false)]
        public string Name { get; set; }
    }

    /// <summary>
    /// Pricing plan used for an Application
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "application_plan")]
    public class ApplicationPlan
    {
        /// <summary>
        /// Id of the Application to which it refers
        /// </summary>
        [DataMember(Name = "application_id", IsRequired = true)]
        public int ApplicationId { get; set; }

        /// <summary>
        /// Id of the Product to which is attached the Pricing Plan
        /// </summary>
        [DataMember(Name = "product_id", IsRequired = true)]
        public int ProductId { get; set; }

        /// <summary>
        /// Name of the Application's Plan
        /// </summary>
        [DataMember(Name = "name", IsRequired = true)]
        public string Name { get; set; }

        /// <summary>
        /// Description of the Application's Plan
        /// </summary>
        [DataMember(Name = "description", IsRequired = false)]
        public string Description { get; set; }

        /// <summary>
        /// Code of the Currency used in Application's Plan
        /// </summary>
        [DataMember(Name = "currency_code", IsRequired = true)]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Amount of money charged for subscription period
        /// </summary>
        [DataMember(Name = "subscription_period_charge", IsRequired = true)]
        public decimal? SubscriptionPeriodCharge { get; set; }
    }
}
