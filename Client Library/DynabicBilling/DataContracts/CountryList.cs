using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// A list of Countries represented as a sequence of Country objects
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "countries")]
    public class CountryList : Collection<Country>
    {
    }
}
