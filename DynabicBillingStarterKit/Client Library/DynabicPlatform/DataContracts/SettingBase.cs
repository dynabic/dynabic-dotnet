using System.Runtime.Serialization;

namespace DynabicPlatform.RestApiDataContract
{
    [DataContract(Name = "setting", Namespace = "v1.0")]
    public class SettingBase
    {
        #region Data Members

        /// <summary>
        /// Id of the Site to which the Setting is set
        /// </summary>
        [DataMember(Name = "site_id", IsRequired = true)]
        public int SiteId { set; get; }

        /// <summary>
        /// Name of the Setting
        /// </summary>
        [DataMember(Name = "name", IsRequired = true)]
        public string Name { set; get; }

        /// <summary>
        /// Value of the Setting
        /// </summary>
        [DataMember(Name = "value", IsRequired = true)]
        public string Value { set; get; }

        /// <summary>
        /// Description of the Setting. A text memo that describes the Setting's purpouse
        /// </summary>
        [DataMember(Name = "description", IsRequired = false)]
        public string Description { set; get; }

        #endregion
    }
}
