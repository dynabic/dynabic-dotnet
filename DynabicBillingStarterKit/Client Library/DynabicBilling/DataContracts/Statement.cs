using System;
using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// Represents a Statement
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "statement")]
    public class Statement
    {
        #region Data Members

        /// <summary>
        /// Id of the Statement. Unique identifier generated and managed by database
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Id of the Subscription for which the Statement was issued
        /// </summary>
        [DataMember(Name = "subscription_id")]
        public int SubscriptionId { get; set; }

        /// <summary>
        /// First Name of the Customer that has subscribed to this Subscription
        /// </summary>
        [DataMember(Name = "customer_first_name")]
        public string CustomerFirstName { get; set; }

        /// <summary>
        /// Last Name of the Customer that has subscribed to this Subscription
        /// </summary>
        [DataMember(Name = "customer_last_name")]
        public string CustomerLastName { get; set; }

        /// <summary>
        /// Email address of the Customer that has subscribed to this Subscription
        /// </summary>
        [DataMember(Name = "email")]
        public string Email { get; set; }

        /// <summary>
        /// Date when the Statement wad issued
        /// </summary>
        [DataMember(Name = "statement_date")]
        public DateTime StatementDate { get; set; }

        /// <summary>
        /// Date from when assessment of the Subscription started
        /// </summary>
        [DataMember(Name = "billing_start_date")]
        public DateTime BillingStartDate { get; set; }

        /// <summary>
        /// Date when assessment of the Subscription will end
        /// </summary>
        [DataMember(Name = "billing_end_date")]
        public DateTime BillingEndDate { get; set; }

        /// <summary>
        /// Amount of money that represent the initial Subscription's balance
        /// </summary>
        [DataMember(Name = "starting_balance")]
        public Decimal? StartingBalance { get; set; }

        /// <summary>
        /// Amount of money that represent total amount of charges
        /// </summary>
        [DataMember(Name = "current_charges")]
        public Decimal? CurrentCharges { get; set; }

        /// <summary>
        /// Amount of money that represent total amount of payments and credits
        /// </summary>
        [DataMember(Name = "payments_and_credits")]
        public Decimal? PaymentsAndCredits { get; set; }

        /// <summary>
        /// Status of Statement
        /// Paid = 1,
        /// Unpaid = 2,
        /// PartiallyPaid = 4,
        /// </summary>
        [DataMember(Name = "status")]
        public StatementPaidStatus Status { get; set; }

        /// <summary>
        /// Id of the Address that is used as BillingAddress
        /// </summary>
        [DataMember(Name = "billing_address_id")]
        public int? BillingAddressId { get; set; }

        /// <summary>
        /// A list of Statement details represented as a collection of StatementDetail objects
        /// </summary>
        [DataMember(Name = "statement_details")]
        public StatementDetailsList StatementDetails { get; set; }

        #endregion

        public Statement()
        {
            this.StatementDetails = new StatementDetailsList();
            this.StatementDate = DateTime.MinValue.ToUniversalTime();
            this.BillingStartDate = DateTime.MinValue.ToUniversalTime();
            this.BillingEndDate = DateTime.MinValue.ToUniversalTime();
        }
    }
}