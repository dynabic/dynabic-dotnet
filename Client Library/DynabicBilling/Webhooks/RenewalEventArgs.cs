using System;
using DynabicBilling.RestApiDataContract;

namespace DynabicBilling
{
    public class RenewalEventArgs : EventArgs
    {
        SubscriptionResponse _subscription = null;

        public RenewalEventArgs(SubscriptionResponse subscription)
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
