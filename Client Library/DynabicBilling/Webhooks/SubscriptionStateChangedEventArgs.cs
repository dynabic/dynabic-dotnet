using System;
using DynabicBilling.RestApiDataContract;

namespace DynabicBilling
{
    public class SubscriptionStateChangedEventArgs : EventArgs
    {
        SubscriptionResponse _subscription = null;

        public SubscriptionStateChangedEventArgs(SubscriptionResponse subscription)
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
