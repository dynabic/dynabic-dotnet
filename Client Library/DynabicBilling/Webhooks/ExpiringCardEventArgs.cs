using System;
using DynabicBilling.RestApiDataContract;

namespace DynabicBilling
{
    public class ExpiringCardEventArgs : EventArgs
    {
        CreditCardResponse _creditCard = null;

        public ExpiringCardEventArgs(CreditCardResponse creditCard)
        {
            _creditCard = creditCard;
        }

        public CreditCardResponse CreditCard
        {
            get
            {
                return _creditCard;
            }
        }
    }
}
