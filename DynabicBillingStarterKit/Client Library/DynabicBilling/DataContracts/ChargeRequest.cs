using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// Represents a charge made o a Subscription. Used as user's request.
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "charge")]
    public class ChargeRequest
    {
        #region Data Members

        /// <summary>
        /// Amount of money that were charged
        /// </summary>
        [DataMember(Name = "amount", IsRequired = true)]
        public decimal Amount { set; get; }

        /// <summary>
        /// A note or description that specifies the reason of charge
        /// </summary>
        [DataMember(Name = "memo", IsRequired = true)]
        public string Memo { set; get; }
        #endregion
    }
}