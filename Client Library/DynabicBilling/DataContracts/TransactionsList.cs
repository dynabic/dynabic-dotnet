using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// A list of Transactions represented as a collection of TransactionResponse objects
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "transactions")]
    public class TransactionsList : Collection<TransactionResponse>
    {
    }
}