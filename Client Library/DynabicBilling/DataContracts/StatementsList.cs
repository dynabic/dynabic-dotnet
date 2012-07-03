using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// A list of Statements represented as a collection of Statement objects
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "statements")]
    public class StatementsList : Collection<Statement>
    {
    }
}