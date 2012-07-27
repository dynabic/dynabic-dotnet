using System;
using System.Runtime.Serialization;
namespace DynabicBilling.RestApiDataContract
{
    #region Subscriptions

    /// <summary>
    /// Classifies the subscriptions status
    /// </summary>
    [Flags]
    [DataContract]
    public enum SubscriptionStatus : short
    {
        /// <summary>All subscriptions will be shown.</summary>
        [EnumMember]
        All = Active | Trialing |
              ConfigurationError | BillNotPaidOnTimeRetrying | BillNotPaid |
              Expired | Cancelled | CreditCardInvalid,

        /// <summary>
        /// A normal, active subscription. It is not in a trial, and is paid and up to date. 
        /// This is where you want all your Customers to be
        /// </summary>
        [EnumMember]
        Active = 1,

        /// <summary>
        /// A subscription in trialing state has a valid trial subscription. 
        /// This type of subscription may transition to active once payment is received when the trial has ended. 
        /// Otherwise, it may go to a Problem or End of Life state.
        /// </summary>
        [EnumMember]
        Trialing = 2,

        /// <summary>
        /// Indicates that normal assessment/processing of the subscription has failed for a reason that 
        /// cannot be fixed by the Customer. For example, a Soft Fail may result from a timeout at the gateway, 
        /// or incorrect credentials on the client part. 
        /// The subscriptions should be retried automatically. 
        /// An interface will be implemented to review problems resulting from these events to take manual action, when needed.
        /// </summary>
        [EnumMember]
        ConfigurationError = 4,

        /// <summary>
        /// Indicates that the most recent payment has failed, and payment is past due for this subscription. 
        /// If you have enabled our automated dunning, this subscription will be in the dunning process. 
        /// If you are handling dunning and payment updates yourself, you will want to use this state to initiate a payment update 
        /// from your customers.
        /// </summary>
        [EnumMember]
        BillNotPaidOnTimeRetrying = 8,

        /// <summary>
        /// Indicates an unpaid subscription. A subscription is marked unpaid if the retry period expires and the client have configured 
        /// the Dunning settings to have a Final Action of “mark the subscription unpaid”. 
        /// While a subscription is marked as unpaid, its period still advances and new charges continue to accrue. 
        /// However, we don't attempt to automatically collect any overdue balance. 
        /// Collecting the balance, or eliminating the balance through an Adjustment is the customer responsibility.
        /// </summary>
        [EnumMember]
        BillNotPaid = 16,

        /// <summary>
        /// Indicates a subscription that has expired due to running its normal life cycle. 
        /// Some products may be configured to have an expiration period. An expired subscription then, 
        /// is one that stayed active until it fulfilled its full period.
        /// </summary>
        [EnumMember]
        Expired = 32,

        /// <summary>
        /// Indicates a canceled subscription. This may happen at your request (via the API or the web interface) or due to 
        /// an expiration of the dunning process without payment. See the Reactivation documentation for info on how to restart a 
        /// canceled subscription. 
        /// While a subscription is canceled, its period will not advance, it will not accrue any new charges, and we will not 
        /// attempt to collect the overdue balance.
        /// </summary>
        [EnumMember]
        Cancelled = 64,

        /// <summary>
        /// Indicates a subscription with an invalid payment information.
        /// </summary>
        [EnumMember]
        CreditCardInvalid = 128,
    }

    #endregion

    #region Transactions

    /// <summary>
    /// Classifies the transaction
    /// </summary>
    [Flags]
    [DataContract]
    public enum TransactionType : short
    {
        /// <summary>All transactions will be shown.</summary>
        [EnumMember]
        All = CreditCard | Credit | Charge | Adjustment,
        /// <summary>A credit card payment has been done.</summary>
        [EnumMember]
        CreditCard = 1,
        /// <summary>The user account has been credited.</summary>
        [EnumMember]
        Credit = 2,
        /// <summary>Invoice has been emitted.</summary>
        [EnumMember]
        Charge = 4,
        /// <summary>The user account has been adjusted with an amount of money.</summary>
        [EnumMember]
        Adjustment = 8,
        /// <summary>The CC validation transaction has been performed.</summary>
        [EnumMember]
        CreditCardValidation = 16,
    }

    /// <summary>
    /// Classifies the transaction status
    /// </summary>
    [DataContract]
    public enum TransactionStatus : short
    {
        /// <summary>All transactions will be shown</summary>
        [EnumMember]
        All = Successful | Failed,
        /// <summary>The transaction was successful</summary>
        [EnumMember]
        Successful = 1,
        /// <summary>The transaction failed</summary>
        [EnumMember]
        Failed = 2,
    }

    /// <summary>
    /// Represents a payment status enumeration
    /// </summary>
    [DataContract]
    public enum TransactionPaymentStatus : short
    {
        /// <summary>
        /// Error
        /// </summary>
        [EnumMember]
        Error = -1,
        /// <summary>
        /// Pending
        /// </summary>
        [EnumMember]
        Pending = 10,
        /// <summary>
        /// Authorized
        /// </summary>
        [EnumMember]
        Authorized = 20,
        /// <summary>
        /// Paid
        /// </summary>
        [EnumMember]
        Paid = 30,
        /// <summary>
        /// Partially Refunded
        /// </summary>
        [EnumMember]
        PartiallyRefunded = 35,
        /// <summary>
        /// Refunded
        /// </summary>
        [EnumMember]
        Refunded = 40,
        /// <summary>
        /// Voided
        /// </summary>
        [EnumMember]
        Voided = 50,
        /// <summary>
        /// Captured
        /// </summary>
        [EnumMember]
        Captured = 60,
    }

    #endregion

    #region Statements

    /// <summary>
    /// Payment status of the statement
    /// </summary>
    [DataContract]
    public enum StatementPaidStatus : short
    {
        /// <summary>All statement details will be shown.</summary>
        [EnumMember]
        All = Paid | Unpaid | PartiallyPaid,
        /// <summary>This statement has been completly paid.</summary>
        [EnumMember]
        Paid = 1,
        /// <summary>No paiment was done for this statement.</summary>
        [EnumMember]
        Unpaid = 2,
        /// <summary>Some payments were done for this statement but there are still some money missing.</summary>
        [EnumMember]
        PartiallyPaid = 4,
    }

    #endregion

    #region Events

    /// <summary>
    /// Classifies the event
    /// </summary>
    [Flags]
    [DataContract]
    public enum EventType
    {
        [EnumMember]
        None = 0,
        /// <summary>All events will be shown.</summary>
        [EnumMember]
        All = Signup | AccountCancellation | SubscriptionRenewal | Payment | ChangeSetting | /*Report |*/ CustomerMailError | StatementGenerated |
            SubscriptionCancelled | ExpiringCreditCard | SubscriptionProductUpdated | SubscriptionActivated | SiteMailError,
        /// <summary>Users have created a new account.</summary>
        [EnumMember]
        Signup = 1,
        /// <summary>Users have cancelled an account.</summary>
        [EnumMember]
        AccountCancellation = 2,
        /// <summary>Subscription renewed.</summary>
        [EnumMember]
        SubscriptionRenewal = 4,
        [EnumMember]
        Payment = 8,
        [EnumMember]
        ChangeSetting = 16,
        //Report = 32,
        /// <summary>Mail error log item type</summary>
        [EnumMember]
        CustomerMailError = 64,
        [EnumMember]
        StatementGenerated = 128,
        /// <summary> Subscription cancelled </summary>
        [EnumMember]
        SubscriptionCancelled = 256,
        /// <summary> A Credit Card has entered the 'expiring' period (usually last 30 days of availability) </summary>
        [EnumMember]
        ExpiringCreditCard = 512,
        /// <summary> The Product that corresponds to a subscription has been updated </summary>
        [EnumMember]
        SubscriptionProductUpdated = 1024,
        /// <summary> A Subscription's status was switched from Trial to Active </summary>
        [EnumMember]
        SubscriptionActivated = 2048,
        [EnumMember]
        SiteMailError = 4096,
        [EnumMember]
        SubscriptionBillingDateChanged = 8192,
        /// <summary> A Subscription was Reactivated (after having been Canceled) </summary>
        [EnumMember]
        SubscriptionReactivated = 16384
    }

    /// <summary>
    /// Clasifies each category as positive or negative event
    /// </summary>
    [Flags]
    [DataContract]
    public enum EventResult
    {
        [EnumMember]
        All = Success | Failed | FailedResolved,
        [EnumMember]
        Success = 1,
        [EnumMember]
        Failed = 2,
        /// <summary>A payment transaction that has failed initially but was solved afterwards.</summary>
        [EnumMember]
        FailedResolved = 4
    }

    #endregion

    #region Products

    /// <summary>
    /// No, Yes, Yes(Optional)
    /// </summary>
    [DataContract]
    public enum BoolOptional : byte
    {
        [EnumMember]
        No = 0,
        [EnumMember]
        Yes = 1,
        [EnumMember]
        YesOptional = 2
    }

    /// <summary>
    /// Holds the possible charge models for ProductItems:
    /// 
    /// Per-Unit
    /// Only 1 price bracket is allowed. Any quantity of allocation (that falls within the allowed range) is charged the same per-unit cost. 
    /// i.e., if the defined price is $1 and user buys 10 items then the total price is $1 Euro * 10 = $10
    ///
    /// Volume
    /// All units are assessed the same per-unit cost according to the prevailing price for the quantity. 
    /// The price per item is the same for the entire quantity.
    /// The price per item is determined based on the total number of units purchased. 
    /// From the example below, if a total of 5 units are bought then the price per unit for all units is 2.
    ///                         if a total of 15 units are bought then the price per unit for all units is 0,5
    /// i.e., 
    /// StartQuantity EndQuantity UnitPrice (see the class ProductMeteredPrice)
    ///     1              5          2
    ///     6              10         1
    ///     11             999999     0.5
    /// 
    /// from the example above,  6 units would cost $1 * 6 = $6
    ///                         20 units would cost $0,5 * 20 = $10
    ///
    /// Tiered
    /// Every unit used is assessed a cost within its own tier. 
    /// i.e., from the example above, 
    ///  6 units would cost: $2/each for the first 5 units and $1 for the sixth
    ///                      ($2 * 5) + ($1 * 1) = $11
    /// 20 units would cost: $2/each for the first 5 units, $1/each for the units from 6 to 10 and $0.5/each for the units from 11 to 20
    ///                      ($2 * 5) + ($1 * 5) + ($0.5 * 10) = $20
    ///
    /// Stairstep
    /// A total cost is assessed according to the price bracket of the quantity used. 
    /// i.e. from the example above:
    ///    3 units will cost $2 (because if falls into the 1-5 interval)
    ///    5 units will also cost $2 (because if falls into the 1-5 interval)
    ///    6 units will cost $1 (because8 if falls into the 6-10 interval)
    ///    8 units will cost $1 (because if falls into the 6-10 interval)
    ///    
    /// Stairstep can be used to define charge models like: from the 0-50 customers is free, 51-500 customers is $49, etc. 
    /// </summary>
    [DataContract]
    public enum ChargeModel
    {
        [EnumMember]
        PerUnit = 0,
        [EnumMember]
        Volume,
        [EnumMember]
        Tiered,
        [EnumMember]
        Stairstep,
        /// <summary> This is used to indicate a ProductItem is just a container for other ProductItems </summary>
        [EnumMember]
        Container,
        [EnumMember]
        Unavailable
    }

    /// <summary>
    /// Represents a recurring product cycle period
    /// </summary>
    [Flags]
    [DataContract]
    public enum RecurringCyclePeriod : int
    {
        /// <summary>
        /// One time only
        /// </summary>
        [EnumMember]
        OneTimeOnly = 1,
        /// <summary>
        /// Days
        /// </summary>
        [EnumMember]
        Daily = 4,
        /// <summary>
        /// Weekly
        /// </summary>
        [EnumMember]
        Weekly = 8,
        /// <summary>
        /// Monthly
        /// </summary>
        [EnumMember]
        Monthly = 16,
        /// <summary>
        /// Monthly, relative to FrequencyRelativeInterval
        /// </summary>
        [EnumMember]
        MonthlyRelativeToFrequencyRelativeInterval = 32,
        /// <summary>
        /// Yearly
        /// </summary>
        [EnumMember]
        Yearly = 64,
    }

    /// <summary>
    /// Indicates the occurrence of the specific day within a
    /// MONTHLY or YEARLY recurrence frequency. For example, within
    /// a MONTHLY frequency, consider the following:
    /// 
    /// RecurrencePattern r = new RecurrencePattern();
    /// r.Frequency = FrequencyType.Monthly;
    /// r.ByDay.Add(new WeekDay(DayOfWeek.Monday, FrequencyOccurrence.First));
    /// 
    /// The above example represents the first Monday within the month,
    /// whereas if FrequencyOccurrence.Last were specified, it would 
    /// represent the last Monday of the month.
    /// 
    /// For a YEARLY frequency, consider the following:
    /// 
    /// Recur r = new Recur();
    /// r.Frequency = FrequencyType.Yearly;
    /// r.ByDay.Add(new WeekDay(DayOfWeek.Monday, FrequencyOccurrence.Second));
    /// 
    /// The above example represents the second Monday of the year.  This can
    /// also be represented with the following code:
    /// 
    /// r.ByDay.Add(new WeekDay(DayOfWeek.Monday, 2));
    /// </summary>
    [Flags]
    [DataContract]
    public enum FrequencyOccurrence
    {
        [EnumMember]
        None = 0,
        [EnumMember]
        First = 1,
        [EnumMember]
        Second = 2,
        [EnumMember]
        Third = 4,
        [EnumMember]
        Fourth = 8,
        [EnumMember]
        Fifth = 16,
        //TODO remove the following since they are not supported by aspose.iCalculator
        [EnumMember]
        Last = 32,
        [EnumMember]
        SecondToLast = 64,
        [EnumMember]
        ThirdToLast = 128,
        [EnumMember]
        FourthToLast = 256,
        [EnumMember]
        FifthToLast = 512
    }

    #endregion

    #region CreditCard

    /// <summary>
    /// Holds the credit card validity status:
    /// - valid
    /// - expiring
    /// - expired
    /// </summary>
    [Flags]
    [DataContract]
    public enum CreditCardStatus : int
    {
        /// <summary>
        /// The credit card is more than
        /// one month away from expiring
        /// </summary>
        [EnumMember]
        Valid = 1,

        /// <summary>
        /// The credit card is in the
        /// last 30 days of validity
        /// </summary>
        [EnumMember]
        Expiring = 2,

        /// <summary>
        /// The credit card is literally expired
        /// </summary>
        [EnumMember]
        Expired = 4
    }

    #endregion

    #region ProductItemType

    /// <summary>
    /// The types of the product's items.
    /// </summary>
    [DataContract]
    public enum ProductItemType
    {
        [EnumMember]
        OnOff = 0,
        [EnumMember]
        Quantity = 1,
        [EnumMember]
        Metered = 2,
    }

    #endregion
}