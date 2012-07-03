using System;
using DynabicBilling.RestApiDataContract;

namespace DynabicBilling
{
    public class PaymentEventArgs : EventArgs
    {
        PaymentResponse _payment = null;

        public PaymentEventArgs(PaymentResponse payment)
        {
            _payment = payment;
        }

        public PaymentResponse Payment
        {
            get
            {
                return _payment;
            }
        }
    }
}
