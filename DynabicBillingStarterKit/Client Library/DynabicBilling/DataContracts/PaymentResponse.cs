using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// Represents the result of a payment
    /// </summary>
    [DataContract(Namespace="v1.0", Name="payment")]
    public class PaymentResponse
    {
        #region Data Members

        /// <summary>
        /// Gets or sets an AVS result
        /// </summary>
        [DataMember]
        public string AVSResult { get; set; }

        /// <summary>
        /// Gets or sets the authorization transaction identifier
        /// </summary>
        [DataMember]
        public string AuthorizationTransactionId { get; set; }

        /// <summary>
        /// Gets or sets the referenced transaction id.
        /// </summary>
        [DataMember]
        public string RefTransactionId { get; set; }

        /// <summary>
        /// Gets or sets the authorization transaction code
        /// </summary>
        [DataMember]
        public string AuthorizationTransactionCode { get; set; }

        /// <summary>
        /// Gets or sets the authorization transaction result
        /// </summary>
        [DataMember]
        public string AuthorizationTransactionResult { get; set; }

        /// <summary>
        /// Gets or sets the subscription transaction identifier
        /// </summary>
        [DataMember]
        public string SubscriptionTransactionId { get; set; }

        /// <summary>
        /// Gets or sets an error message for customer, or String.Empty if no errors
        /// </summary>
        [DataMember]
        public string Error { get; set; }

        /// <summary>
        /// Gets or sets a full error message, or String.Empty if no errors
        /// </summary>
        [DataMember]
        public string FullError { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the payment result is successfull.
        /// </summary>
        [DataMember]
        public bool IsSuccessfull { get; set; }

        /// <summary>
        /// Gets or sets the raw message sent to the payment provider. Used for debugging.
        /// </summary>
        [DataMember]
        public string RawRequest { get; set; }

        /// <summary>
        /// Gets or sets the raw message received from the payment provider. Used for debugging.
        /// </summary>
        [DataMember]
        public string RawResponse { get; set; }

        /// <summary>
        /// Gets or sets a formatted variant of the message sent to the payment provider. Used for human-friendly output
        /// </summary>
        [DataMember]
        public string FormattedRequest { get; set; }

        /// <summary>
        /// Gets or sets a formatted variant of the message received from the payment provider. Used for human-friendly output
        /// </summary>
        [DataMember]
        public string FormattedResponse { get; set; }

        /// <summary>
        /// Gets or sets the status of the payment after processing
        /// </summary>
        [DataMember]
        public string PaymentStatus { get; set; }


        #endregion Data Members

        public PaymentResponse()
        {
            AVSResult = string.Empty;
            AuthorizationTransactionCode = string.Empty;
            AuthorizationTransactionId = string.Empty;
            AuthorizationTransactionResult = string.Empty;
            Error = string.Empty;
            FormattedRequest = string.Empty;
            FormattedResponse = string.Empty;
            FullError = string.Empty;
            IsSuccessfull = false;
            PaymentStatus = string.Empty;
            RawRequest = string.Empty;
            RawResponse = string.Empty;
            RefTransactionId = string.Empty;
            SubscriptionTransactionId = string.Empty;
        }
    }
}
