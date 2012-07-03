using System.Runtime.Serialization;

namespace DynabicPlatform.RestApiDataContract
{
    /// <summary>
    /// Setting for a Dynabic Site. Used as user's request for Create and Update operations.
    /// </summary>
    [DataContract(Name = "setting_request", Namespace = "v1.0")]
    public class SettingRequest : SettingBase
    {
    }
}
