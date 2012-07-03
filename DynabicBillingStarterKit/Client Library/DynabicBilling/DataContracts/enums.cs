using System;
namespace DynabicBilling.RestApiDataContract
{
    #region Subscriptions

    /// <summary>
    /// Classifies the subscriptions status
    /// </summary>
    public enum SubscriptionStatus : short
    {
        /// <summary>All subscriptions will be shown.</summary>
        All = Active | Trialing |
              ConfigurationError | BillNotPaidOnTimeRetrying | BillNotPaid |
              Expired | Cancelled | CreditCardInvalid,

        /// <summary>
        /// A normal, active subscription. It is not in a trial, and is paid and up to date. 
        /// This is where you want all your Customers to be
        /// </summary>
        Active = 1,

        /// <summary>
        /// A subscription in trialing state has a valid trial subscription. 
        /// This type of subscription may transition to active once payment is received when the trial has ended. 
        /// Otherwise, it may go to a Problem or End of Life state.
        /// </summary>
        Trialing = 2,

        /// <summary>
        /// Indicates that normal assessment/processing of the subscription has failed for a reason that 
        /// cannot be fixed by the Customer. For example, a Soft Fail may result from a timeout at the gateway, 
        /// or incorrect credentials on the client part. 
        /// The subscriptions should be retried automatically. 
        /// An interface will be implemented to review problems resulting from these events to take manual action, when needed.
        /// </summary>
        ConfigurationError = 4,

        /// <summary>
        /// Indicates that the most recent payment has failed, and payment is past due for this subscription. 
        /// If you have enabled our automated dunning, this subscription will be in the dunning process. 
        /// If you are handling dunning and payment updates yourself, you will want to use this state to initiate a payment update 
        /// from your customers.
        /// </summary>
        BillNotPaidOnTimeRetrying = 8,

        /// <summary>
        /// Indicates an unpaid subscription. A subscription is marked unpaid if the retry period expires and the client have configured 
        /// the Dunning settings to have a Final Action of “mark the subscription unpaid”. 
        /// While a subscription is marked as unpaid, its period still advances and new charges continue to accrue. 
        /// However, we don't attempt to automatically collect any overdue balance. 
        /// Collecting the balance, or eliminating the balance through an Adjustment is the customer responsibility.
        /// </summary>
        BillNotPaid = 16,

        /// <summary>
        /// Indicates a subscription that has expired due to running its normal life cycle. 
        /// Some products may be configured to have an expiration period. An expired subscription then, 
        /// is one that stayed active until it fulfilled its full period.
        /// </summary>
        Expired = 32,

        /// <summary>
        /// Indicates a canceled subscription. This may happen at your request (via the API or the web interface) or due to 
        /// an expiration of the dunning process without payment. See the Reactivation documentation for info on how to restart a 
        /// canceled subscription. 
        /// While a subscription is canceled, its period will not advance, it will not accrue any new charges, and we will not 
        /// attempt to collect the overdue balance.
        /// </summary>
        Cancelled = 64,

        /// <summary>
        /// Indicates a subscription with an invalid payment information.
        /// </summary>
        CreditCardInvalid = 128,
    }

    #endregion

    #region Transactions

    /// <summary>
    /// Classifies the transaction
    /// </summary>
    [Flags]
    public enum TransactionType : short
    {
        /// <summary>All transactions will be shown.</summary>
        All = CreditCard | Credit | Charge | Adjustment,
        /// <summary>A credit card payment has been done.</summary>
        CreditCard = 1,
        /// <summary>The user account has been credited.</summary>
        Credit = 2,
        /// <summary>Invoice has been emitted.</summary>
        Charge = 4,
        /// <summary>The user account has been adjusted with an amount of money.</summary>
        Adjustment = 8,
        /// <summary>The CC validation transaction has been performed.</summary>
        CreditCardValidation = 16,
    }

    /// <summary>
    /// Classifies the transaction status
    /// </summary>
    public enum TransactionStatus : short
    {
        /// <summary>All transactions will be shown</summary>
        All = Successful | Failed,
        /// <summary>The transaction was successful</summary>
        Successful = 1,
        /// <summary>The transaction failed</summary>
        Failed = 2,
    }

    /// <summary>
    /// Represents a payment status enumeration
    /// </summary>
    public enum TransactionPaymentStatus : short
    {
        /// <summary>
        /// Error
        /// </summary>
        Error = -1,
        /// <summary>
        /// Pending
        /// </summary>
        Pending = 10,
        /// <summary>
        /// Authorized
        /// </summary>
        Authorized = 20,
        /// <summary>
        /// Paid
        /// </summary>
        Paid = 30,
        /// <summary>
        /// Partially Refunded
        /// </summary>
        PartiallyRefunded = 35,
        /// <summary>
        /// Refunded
        /// </summary>
        Refunded = 40,
        /// <summary>
        /// Voided
        /// </summary>
        Voided = 50,
        /// <summary>
        /// Captured
        /// </summary>
        Captured = 60,
    }

    #endregion

    #region Statements

    /// <summary>
    /// Payment status of the statement
    /// </summary>
    public enum StatementPaidStatus : short
    {
        /// <summary>All statement details will be shown.</summary>
        All = Paid | Unpaid | PartiallyPaid,
        /// <summary>This statement has been completly paid.</summary>
        Paid = 1,
        /// <summary>No paiment was done for this statement.</summary>
        Unpaid = 2,
        /// <summary>Some payments were done for this statement but there are still some money missing.</summary>
        PartiallyPaid = 4,
    }

    #endregion

    #region Events

    /// <summary>
    /// Classifies the event
    /// </summary>
    [Flags]
    public enum EventType
    {
        None = 0,
        /// <summary>All events will be shown.</summary>
        All = Signup | AccountCancellation | SubscriptionRenewal | Payment | ChangeSetting | /*Report |*/ CustomerMailError | StatementGenerated |
            SubscriptionCancelled | ExpiringCreditCard | SubscriptionProductUpdated | SubscriptionActivated | SiteMailError,
        /// <summary>Users have created a new account.</summary>
        Signup = 1,
        /// <summary>Users have cancelled an account.</summary>
        AccountCancellation = 2,
        /// <summary>Subscription renewed.</summary>
        SubscriptionRenewal = 4,
        Payment = 8,
        ChangeSetting = 16,
        //Report = 32,
        /// <summary>Mail error log item type</summary>
        CustomerMailError = 64,
        StatementGenerated = 128,
        /// <summary> Subscription cancelled </summary>
        SubscriptionCancelled = 256,
        /// <summary> A Credit Card has entered the 'expiring' period (usually last 30 days of availability) </summary>
        ExpiringCreditCard = 512,
        /// <summary> The Product that corresponds to a subscription has been updated </summary>
        SubscriptionProductUpdated = 1024,
        /// <summary> A Subscription's status was switched from Trial to Active </summary>
        SubscriptionActivated = 2048,
        SiteMailError = 4096,
        SubscriptionBillingDateChanged = 8192,
        /// <summary> A Subscription was Reactivated (after having been Canceled) </summary>
        SubscriptionReactivated = 16384
    }

    /// <summary>
    /// Clasifies each category as positive or negative event
    /// </summary>
    [Flags]
    public enum EventResult
    {
        All = Success | Failed | FailedResolved,
        Success = 1,
        Failed = 2,
        /// <summary>A payment transaction that has failed initially but was solved afterwards.</summary>
        FailedResolved = 4
    }

    #endregion

    #region Products

    /// <summary>
    /// No, Yes, Yes(Optional)
    /// </summary>
    public enum BoolOptional : byte
    {
        No = 0,
        Yes = 1,
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
    public enum ChargeModel
    {
        PerUnit = 0,
        Volume,
        Tiered,
        Stairstep,
        /// <summary> This is used to indicate a ProductItem is just a container for other ProductItems </summary>
        Container,
        Unavailable
    }

    /// <summary>
    /// Represents a recurring product cycle period
    /// </summary>
    public enum RecurringCyclePeriod : int
    {
        /// <summary>
        /// One time only
        /// </summary>
        OneTimeOnly = 1,
        /// <summary>
        /// Days
        /// </summary>
        Daily = 4,
        /// <summary>
        /// Weekly
        /// </summary>
        Weekly = 8,
        /// <summary>
        /// Monthly
        /// </summary>
        Monthly = 16,
        /// <summary>
        /// Monthly, relative to FrequencyRelativeInterval
        /// </summary>
        MonthlyRelativeToFrequencyRelativeInterval = 32,
        /// <summary>
        /// Yearly
        /// </summary>
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
    public enum FrequencyOccurrence
    {
        None = 0,
        First = 1,
        Second = 2,
        Third = 4,
        Fourth = 8,
        Fifth = 16,
        //TODO remove the following since they are not supported by aspose.iCalculator
        Last = 32,
        SecondToLast = 64,
        ThirdToLast = 128,
        FourthToLast = 256,
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
    public enum CreditCardStatus : int
    {
        /// <summary>
        /// The credit card is more than
        /// one month away from expiring
        /// </summary>
        Valid = 1,

        /// <summary>
        /// The credit card is in the
        /// last 30 days of validity
        /// </summary>
        Expiring = 2,

        /// <summary>
        /// The credit card is literally expired
        /// </summary>
        Expired = 4
    }

    #endregion

    #region ProductItemType

    /// <summary>
    /// The types of the product's items.
    /// </summary>
    public enum ProductItemType
    {
        OnOff = 0,
        Quantity = 1,
        Metered = 2,
    }

    #endregion
}