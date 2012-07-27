using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// Classifies one event occurrence
    /// </summary>
    [Flags]
    [DataContract]
    public enum ChargeReason : short
    {
        /// <summary>All occurrences details will be shown.</summary>
        [EnumMember]
        All = InitialUpFront | Trial | Recurrency | OneTimeOnly | RecurrencyExpired | TrialExpired,
        /// <summary>This event has occurred as an initial upfront charge.</summary>
        [EnumMember]
        InitialUpFront = 1,
        /// <summary>This event has occurred as a charge for the trial period.</summary>
        [EnumMember]
        Trial = 2,
        /// <summary>This event has occurred for a recurrent payment.</summary>
        [EnumMember]
        Recurrency = 4,
        /// <summary>This event will occur only once.</summary>
        [EnumMember]
        OneTimeOnly = 8,
        /// <summary>This event will occur when the recurrency expires. The customer will be charged with an amount proportional with the time passed since the last payment event.
        /// We won't charge for the entire recurring period but only for the time passed since the last payment occurred</summary>
        [EnumMember]
        RecurrencyExpired = 16,
        /// <summary>This event will occur when the trial period expires. The customer will now be charged here. It is used only to mark the end of a period</summary>
        [EnumMember]
        TrialExpired = 32,
    }

    /// <summary>
    /// A list of Statement details represented as a collection of StatementDetail objects
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "statement_details")]
    public class StatementDetailsList : Collection<StatementDetail>
    {
    }

    /// <summary>
    /// Contains information about details of a Statement
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "statement_detail")]
    public class StatementDetail
    {
        #region Data Members

        /// <summary>
        /// Statement detail identifier
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Statement reference id
        /// </summary>
        [DataMember(Name = "statement_id")]
        public int StatementId { get; set; }

        /// <summary>
        /// Statement detail date
        /// </summary>
        [DataMember(Name = "date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Statement detail type (Int16): Recurrency, OneTimeOnly
        /// </summary>
        [DataMember(Name = "type")]
        public ChargeReason Type { get; set; }

        /// <summary>
        /// Derails describing the Statement
        /// </summary>
        [DataMember(Name = "details")]
        public string Details { get; set; }

        /// <summary>
        /// Amount of money for Statement
        /// </summary>
        [DataMember(Name = "amount")]
        public Decimal Amount { get; set; }

        /// <summary>
        /// Id of the Currency used in the issued Statement
        /// </summary>
        [DataMember(Name = "currency_id")]
        public int CurrencyId { get; set; }

        #endregion Data Members

        public StatementDetail()
        {
            this.Date = DateTime.MinValue.ToUniversalTime();
        }
    }
}
