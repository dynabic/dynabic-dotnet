using System;
using DynabicBilling.RestApiDataContract;

namespace DynabicBilling
{
    public class SubscriptionEventArgs : EventArgs
    {
        SubscriptionResponse _subscription = null;

        public SubscriptionEventArgs(SubscriptionResponse subscription)
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
