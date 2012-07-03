using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// CreditCard that is used as user's request for Create or Update operations
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "credit_card_request")]
    public class CreditCardRequest : CreditCardBase
    {
    }
}