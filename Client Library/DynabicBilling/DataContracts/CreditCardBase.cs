using System;
using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    [DataContract(Namespace = "v1.0", Name = "credit_card")]
    public class CreditCardBase
    {
        #region Data Members

        /// <summary>
        /// First name printed on the credit card
        /// </summary>
        [DataMember(Name = "first_name_on_card", IsRequired = true)]
        public string FirstNameOnCard { get; set; }

        /// <summary>
        /// Last name printed on the credit card
        /// </summary>
        [DataMember(Name = "last_name_on_card", IsRequired = true)]
        public string LastNameOnCard { get; set; }

        /// <summary>
        /// Credit card number printed on the credit card
        /// </summary>
        [DataMember(Name = "card_number", IsRequired = true)]
        public string Number { get; set; }

        /// <summary>
        /// Expiration date printed on the credit card
        /// </summary>
        [DataMember(Name = "expiration_date", IsRequired = false)]
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// CVV printed on the back of the card
        /// </summary>
        [DataMember(Name = "cvv", IsRequired = false)]
        public string Cvv { get; set; }

        ///// <summary>
        ///// Gets or sets the DynabicBilling.RestApi.Classes.Customer
        ///// </summary>        
        //[DataMember(Name="customer_request", IsRequired=false)]
        //public CustomerRequest Customer { get; set; }
        #endregion
    }
}
