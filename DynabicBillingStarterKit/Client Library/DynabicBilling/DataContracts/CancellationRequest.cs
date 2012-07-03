using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    [DataContract(Namespace = "v1.0", Name = "cancel")]
    public class CancellationRequest
    {
        /// <summary>
        /// false - cancel the subscription immediatly
        /// true - cancel the subscription at the end of subscription period
        /// null - cancel the subscritioin immediately 
        ///        (the call was made from AssessSubscriptions and so it denotes a subscription that was earlier set to be deleted @ end of period)
        /// </summary>
        [DataMember(Name = "isCancelledAtEndOfPeriod", IsRequired = false)]
        public bool? IsCancelledAtEndOfPeriod { get; set; }

        /// <summary>
        /// Info provided by the User concerning the cancellation reason
        /// </summary>
        [DataMember(Name = "cancelationDetails", IsRequired = true)]
        public string CancelationDetails { get; set; }
    }
}
