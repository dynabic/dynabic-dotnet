using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// State or Provice used in Billing or Shipping Address
    /// </summary>
    [DataContract(Namespace = "v1.0")]
    public class StateProvince
    {
        /// <summary>
        /// Name of the State/Province
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { set; get; }

        /// <summary>
        /// Country's International Standart code represented in two letters
        /// </summary>
        [DataMember(Name = "country_ISO_code")]
        public string CountryTwoLettersISOCode { set; get; }

        /// <summary>
        /// State/Provice unique identifier generated and managed by database
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { set; get; }

        /// <summary>
        /// Order of appearance of the State/Provice in a collection of StateProvince objects
        /// </summary>
        [DataMember(Name = "province_number")]
        public string ProvinceNumber { set; get; }

        public StateProvince () { }
    }
}