using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract 
{
    [DataContract(Namespace = "v1.0", Name="country")]
    public class Country
    {
        /// <summary>
        /// Id of the Country. Used to identify a record
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { set; get; }        

        /// <summary>
        /// Name of the Country
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { set; get; }

        /// <summary>
        /// International Standart Code represented by 2 letters
        /// </summary>
        [DataMember(Name = "two_letter_ISO_code")]
        public string TwoLetterISOCode { set; get; }

        /// <summary>
        /// International Standart Code represented by 3 letters
        /// </summary>
        [DataMember(Name = "three_letter_ISO_code")]
        public string ThreeLetterISOCode { set; get; }

        /// <summary>
        /// International Standart Code represented by a numeric value
        /// </summary>
        [DataMember(Name = "numeric_ISO_code")]
        public int NumericISOCode { set; get; }

        /// <summary>
        /// Numeric values that indicates the order of appearance of a country in a collection of countruies
        /// </summary>
        [DataMember(Name = "display_order")]
        public int DisplayOrder { set; get; }

        /// <summary>
        /// Federal Information Processing Standart for a Country
        /// </summary>
        [DataMember(Name = "fips")]
        public string Fips { set; get; }

        /// <summary>
        /// Capital city of the Country
        /// </summary>
        [DataMember(Name = "capital")]
        public string Capital { set; get; }

        /// <summary>
        /// Total surface occupied by the Country
        /// </summary>
        [DataMember(Name = "area")]
        public int? Area { set; get; }

        /// <summary>
        /// Population of the Country
        /// </summary>
        [DataMember(Name = "population")]
        public int? Population { set; get; }

        /// <summary>
        /// Continent where the Country is located in
        /// </summary>
        [DataMember(Name = "continent")]
        public string Continent { set; get; }

        /// <summary>
        /// Top-Level Domain. Used in Internet domains name
        /// </summary>
        [DataMember(Name = "tld")]
        public string Tld { set; get; }

        /// <summary>
        /// Code for Country's Currency represented by 3 letters
        /// </summary>
        [DataMember(Name = "currency_code")]
        public string CurrencyCode { set; get; }

        /// <summary>
        /// Name of the Country's Currency
        /// </summary>
        [DataMember(Name = "currency_name")]
        public string CurrencyName { set; get; }

        /// <summary>
        /// Format  of the Postal Code used in the Country
        /// </summary>
        [DataMember(Name = "postal_code_format")]
        public string PostalCodeFormat { set; get; }

        /// <summary>
        /// A regular expression that accepts a specific PostalCode
        /// </summary>
        [DataMember(Name = "postal_code_regex")]
        public string PostalCodeRegex { set; get; }

        /// <summary>
        /// Languages spoken in the Country.
        /// </summary>
        [DataMember(Name = "languages")]
        public string Languages { set; get; }

        /// <summary>
        /// Id of the geographical location of the country
        /// </summary>
        [DataMember(Name = "geoname_id")]
        public int? GeonameId { set; get; }

        /// <summary>
        /// Neighbour countries
        /// </summary>
        [DataMember(Name = "neighbours")]
        public string Neighbours { set; get; }

        /// <summary>
        /// An alternative FIPS code
        /// </summary>
        [DataMember(Name = "equivalent_fips_code")]
        public string EquivalentFipsCode { set; get; }

        public Country() { }
    }
}