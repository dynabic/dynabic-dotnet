using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// Transaction. Used for user's request for Create or Update operations.
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "transaction")]
    public class TransactionRequest : TransactionBase
    {
    }
}