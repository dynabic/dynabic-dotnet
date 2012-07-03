using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// Represents a charge made o a Subscription. Used as user's response.
    /// </summary>
    [DataContract(Namespace="v1.0",Name="charge_response")]
    public class ChargeResponse : ChargeRequest
    {
        #region Data Members

        /// <summary>
        /// Result of operation of charge. It is auto-generated.
        /// </summary>
        [DataMember(Name = "success", IsRequired = true)]
        public bool Success { set; get; }

        #endregion

        public ChargeResponse () { }
    }
}