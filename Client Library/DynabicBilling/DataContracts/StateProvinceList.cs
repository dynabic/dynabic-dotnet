using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// A list of StateProvinces represented as a sequence of StateProvince objects
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "stateprovinces")]
    public class StateProvinceList : Collection<StateProvince>
    {
    }
}
