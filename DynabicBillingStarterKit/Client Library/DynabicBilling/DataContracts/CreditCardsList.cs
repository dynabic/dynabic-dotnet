using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// A collection of Credit Cards
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "credit_cards")]
    public class CreditCardsList : Collection<CreditCardResponse>
    {
    }
}