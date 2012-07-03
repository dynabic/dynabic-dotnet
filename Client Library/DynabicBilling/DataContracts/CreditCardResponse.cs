using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    [DataContract(Namespace = "v1.0", Name = "credit_card_response")]
    public class CreditCardResponse : CreditCardBase
    {
        #region DataMembers

        /// <summary>
        /// Unique identifier for the Credit Card. It is generated and managed by the database
        /// </summary>
        [DataMember(Name = "id", IsRequired = false)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Customer id
        /// </summary>
        [DataMember(Name = "customer_id", IsRequired = false)]
        public int CustomerId { set; get; }

        /// <summary>
        /// The CreditCard's validity status(valid, expiring, expired)
        /// </summary>
        [DataMember(Name = "status", IsRequired = false)]
        public CreditCardStatus Status { get; set; }

        #endregion

        public CreditCardResponse() : base() { }

        public static implicit operator CreditCardRequest(CreditCardResponse response)
        {
            if (response == null) return null;
            return new CreditCardRequest
            {
                Cvv = response.Cvv,
                ExpirationDate = response.ExpirationDate,
                FirstNameOnCard = response.FirstNameOnCard,
                LastNameOnCard = response.LastNameOnCard,
                Number = response.Number,
            };
        }
    }
}