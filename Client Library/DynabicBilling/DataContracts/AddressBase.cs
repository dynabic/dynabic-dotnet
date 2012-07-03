using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    [DataContract(Namespace = "v1.0", Name = "address")]
    public class AddressBase
    {
        #region Data Members

        /// <summary>
        /// First Name of the Addressee
        /// </summary>
        [DataMember(Name = "first_name")]
        public string FirstName { set; get; }

        /// <summary>
        /// Last Name of the Addressee
        /// </summary>
        [DataMember(Name = "last_name")]
        public string LastName { set; get; }

        /// <summary>
        /// Phone Number of Addressee
        /// </summary>
        [DataMember(Name = "phone_number")]
        public string PhoneNumber { set; get; }

        /// <summary>
        /// Email Address of Addressee
        /// </summary>
        [DataMember(Name = "email")]
        public string Email { set; get; }

        /// <summary>
        /// Fax Number of Addressee
        /// </summary>
        [DataMember(Name = "fax_number")]
        public string FaxNumber { set; get; }

        /// <summary>
        /// Name of the Company of Addressee
        /// </summary>
        [DataMember(Name = "company")]
        public string Company { set; get; }

        /// <summary>
        /// Address of Addressee (Field1)
        /// </summary>
        [DataMember(Name = "address1")]
        public string Address1 { set; get; }

        /// <summary>
        /// Address of Addressee (Field2)
        /// </summary>
        [DataMember(Name = "address2")]
        public string Address2 { set; get; }

        /// <summary>
        /// City Name of the Addressee
        /// </summary>
        [DataMember(Name = "city")]
        public string City { set; get; }

        /// <summary>
        /// State or Province Name of the Addressee
        /// </summary>
        [DataMember(Name = "state_province", IsRequired = false)]
        public string StateProvince { set; get; }

        /// <summary>
        /// Country Name of the Addressee
        /// </summary>
        [DataMember(Name = "country", IsRequired = false)]
        public string Country { set; get; }

        /// <summary>
        /// ZIP or Postal Code of the Addressee
        /// </summary>
        [DataMember(Name = "zip_postal_code")]
        public string ZipPostalCode { set; get; }

        #endregion
    }
}
