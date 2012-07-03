using System;
using DynabicBilling.RestApiDataContract;

namespace DynabicBilling
{
    public class BillingDateChangedEventArgs : EventArgs
    {
        SubscriptionResponse _subscription = null;

        public BillingDateChangedEventArgs(SubscriptionResponse subscription)
        {
            _subscription = subscription;
        }

        public SubscriptionResponse Subscription
        {
            get
            {
                return _subscription;
            }
        }
    }
}
