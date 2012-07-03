using System;
using DynabicBilling.RestApiDataContract;

namespace DynabicBilling
{
    public class SubscriptionProductChangedEventArgs : EventArgs
    {
        ProductResponse _product = null;

        public SubscriptionProductChangedEventArgs(ProductResponse product)
        {
            _product = product;
        }

        public ProductResponse Product
        {
            get
            {
                return _product;
            }
        }
    }
}
