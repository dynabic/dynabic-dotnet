using System;
using DynabicBilling.RestApiDataContract;

namespace DynabicBilling
{
    public class SignupEventArgs : EventArgs
    {
        CustomerResponse _customer = null;

        public SignupEventArgs(CustomerResponse customer)
        {
            _customer = customer;
        }

        public CustomerResponse Customer
        {
            get
            {
                return _customer;
            }
        }
    }
}
