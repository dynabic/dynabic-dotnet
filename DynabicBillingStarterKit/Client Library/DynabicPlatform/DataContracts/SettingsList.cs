using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace DynabicPlatform.RestApiDataContract
{
    /// <summary>
    /// A list of Settings represented a collection of SettingsResponse
    /// </summary>
    [CollectionDataContract(Name = "settings", Namespace = "v1.0")]
    public class SettingsList : Collection<SettingResponse>
    {       
    }
}
