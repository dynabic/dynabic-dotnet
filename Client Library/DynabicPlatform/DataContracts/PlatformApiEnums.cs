using System;

namespace DynabicPlatform.RestApiDataContract
{
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
        SubscriptionBillingDateChanged = 8192
    }

    #endregion Events
}
