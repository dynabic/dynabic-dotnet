using System;
using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// Transaction. Used as response to user's requests
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "transaction_response")]
    public class TransactionResponse : TransactionBase
    {
        #region Data Members

        [DataMember(Name = "service_result", IsRequired = false)]
        public ServiceResult ServiceResult { set; get; }

        /// <summary>
        /// Date when the Transaction was made
        /// </summary>
        [DataMember(Name = "transaction_date")]
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Unique Transaction identified generated and managed by database
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { get; set; }

        /// <summary>
        /// The Gateway response code
        /// </summary>
        [DataMember(Name = "gateway_response_code")]
        public string GatewayResponseCode { get; set; }

        /// <summary>
        /// The message corresponding to the Gateway response code
        /// </summary>
        [DataMember(Name = "gateway_response_message")]
        public string GatewayResponseMessage { get; set; }

        /// <summary>
        /// The description string associated with the Gateway response
        /// </summary>
        [DataMember(Name = "gateway_response_description")]
        public string GatewayResponseDescription { get; set; }

        /// <summary>
        /// The complete, formatted request that was sent to the Gateway
        /// </summary>
        [DataMember(Name = "gateway_raw_request")]
        public string GatewayRawRequest { get; set; }

        /// <summary>
        /// The complete, formatted response that was received from the Gateway
        /// </summary>
        [DataMember(Name = "gateway_raw_response")]
        public string GatewayRawResponse { get; set; }

        /// <summary>
        /// The Id of the associated Gateway transaction
        /// </summary>
        [DataMember(Name = "gateway_transaction_id")]
        public string GatewayTransactionId { get; set; }

        /// <summary>
        /// The Id of the referred Gateway transaction, if any
        /// </summary>
        [DataMember(Name = "gateway_ref_transaction_id")]
        public string GatewayRefTransactionId { get; set; }

        /// <summary>
        /// The payment status returned by the Gateway
        /// </summary>
        [DataMember(Name = "gateway_status")]
        public TransactionPaymentStatus GatewayStatus { get; set; }

        /// <summary>
        /// The total refunded amount for this transaction
        /// </summary>
        [DataMember(Name = "refunded")]
        public decimal Refunded { get; set; }

        /// <summary>
        /// The settlement date for the transaction
        /// </summary>
        [DataMember(Name = "gateway_settle_date")]
        public DateTime? GatewaySettleDate { get; set; }

        /// <summary>
        /// The subscription balance
        /// </summary>
        [DataMember(Name = "subscription_balance")]
        public decimal? SubscriptionBalance { get; set; }

        /// <summary>
        /// Purchase order reference
        /// </summary>
        [DataMember(Name = "purchase_order_reference")]
        public string PurchaseOrderReference { get; set; }

        /// <summary>
        /// The number of settlement attempts for the transaction
        /// </summary>
        [DataMember(Name = "settlement_attempts_count")]
        public int? SettlementAttemptsCount { get; set; }

        /// <summary>
        /// The time of the last settlement attempt for the transaction
        /// </summary>
        [DataMember(Name = "last_settlement_attempt_date")]
        public DateTime? LastSettlementAttemptDate { get; set; }

        #endregion

        public TransactionResponse()
        {
            this.TransactionDate = DateTime.MinValue.ToUniversalTime();
            this.ServiceResult = new ServiceResult();
        }
    }
}