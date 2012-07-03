using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// A list of Currencies represented as a sequence of Currency objects
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "currencies")]
    public class CurrencyList : Collection<Currency>
    {
    }
}
