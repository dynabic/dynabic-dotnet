using System;
using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    [DataContract(Namespace = "v1.0", Name = "subscription_item_update")]
    public class SubscriptionItemHistoryResponse
    {
        /// <summary>
        /// Gets or sets the old quantity value
        /// </summary>
        [DataMember(Name = "old_value", IsRequired = true)]
        public decimal? OldValue { get; set; }

        /// <summary>
        /// Gets or sets the new quantity value
        /// </summary>
        [DataMember(Name = "new_value", IsRequired = true)]
        public decimal NewValue { get; set; }

        /// <summary>
        /// Gets or sets the update description
        /// </summary>
        [DataMember(Name = "description", IsRequired = false)]
        public string UpdateDescription { get; set; }

        /// <summary>
        /// Gets or sets the time when update is performed
        /// </summary>
        [DataMember(Name = "update_on", IsRequired = true)]
        public DateTime UpdatedOn { get; set; }
    }
}
