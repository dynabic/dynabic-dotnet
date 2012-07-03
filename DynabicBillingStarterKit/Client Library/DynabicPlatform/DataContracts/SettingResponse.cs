using System.Runtime.Serialization;

namespace DynabicPlatform.RestApiDataContract
{
    /// <summary>
    /// Setting for a Dynabic Site. Used as Response to user's requests
    /// </summary>
    [DataContract(Name = "setting_response", Namespace = "v1.0")]
    public class SettingResponse : SettingBase
    {
        #region Data Members

        /// <summary>
        /// Setting's unique identifier
        /// </summary>
        [DataMember(Name = "id", IsRequired = false)]
        public int SettingId { set; get; }

        #endregion

        public static implicit operator SettingRequest(SettingResponse response)
        {
            if (response == null) return null;
            return new SettingRequest
            {
                Description = response.Description,
                Name = response.Name,
                SiteId = response.SiteId,
                Value = response.Value,
            };
        }
    }
}
