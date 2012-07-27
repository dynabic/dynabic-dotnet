using System;
using System.Runtime.Serialization;

namespace DynabicPlatform.RestApiDataContract
{
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
        SubscriptionBillingDateChanged = 8192
    }

    #endregion Events
}
