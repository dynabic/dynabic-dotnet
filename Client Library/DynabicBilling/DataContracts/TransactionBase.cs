using System;
using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    [DataContract(Namespace = "v1.0", Name = "transaction")]
    public class TransactionBase
    {
        #region Data Members

        /// <summary>
        /// Id of the Subscription for which the Transaction was made
        /// </summary>
        [DataMember(Name = "subscription_id")]
        public int SubscriptionId { get; set; }

        /// <summary>
        /// Type of the Transaction
        /// CreditCard = 1,
        /// Credit = 2,
        /// Charge = 4,
        /// Adjustement = 8,
        /// </summary>
        [DataMember(Name = "transaction_type")]
        public TransactionType TransactionType { get; set; }

        /// <summary>
        /// Status of the Trasaction
        /// Successfull = 1,
        /// Failed = 2,
        /// </summary>
        [DataMember(Name = "transaction_status")]
        public TransactionStatus TransactionStatus { get; set; }

        /// <summary>
        ///  Transacted amount of money
        /// </summary>
        [DataMember(Name = "amount")]
        public Decimal Amount { get; set; }

        /// <summary>
        /// Currency used for Transaction
        /// </summary>
        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Memo text describing the Transaction
        /// </summary>
        [DataMember(Name = "details")]
        public string Details { get; set; }

        #endregion

        #region Unserialized Properties

        /// <summary>
        /// Used to keep the reference to a DynabicBilling.BusinessServiceDataContract.Currency
        /// </summary>
        public int CurrencyId { set; get; }

        #endregion
    }
}
