using System.Runtime.Serialization;

namespace DynabicPlatform.RestApiDataContract
{
    [DataContract(Namespace = "v1.0", Name = "company_info")]
    public class CompanyInfoBase
    {
        #region Data Members

        [DataMember(Name = "name", IsRequired = true)]
        public string Name { get; set; }

        [DataMember(Name = "email", IsRequired = true)]
        public string Email { get; set; }

        [DataMember(Name = "phone", IsRequired = true)]
        public string Phone { get; set; }

        [DataMember(Name = "culture", IsRequired = true)]
        public string Culture { get; set; }

        [DataMember(Name = "country", IsRequired = true)]
        public string Country { get; set; }

        [DataMember(Name = "timezone", IsRequired = true)]
        public string TimeZoneOffset { get; set; }

        [DataMember(Name = "currency", IsRequired = true)]
        public string Currency { get; set; }

        #endregion Data Members
    }
}
