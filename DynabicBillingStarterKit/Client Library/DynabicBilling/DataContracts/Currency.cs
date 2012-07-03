using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract 
{
    [DataContract(Namespace = "v1.0", Name = "currency")]
    public class Currency {

        #region Data Members
 
        /// <summary>
        /// Name of the  Currency
        /// </summary>
        [DataMember(Name = "name", IsRequired = false)]
        public string Name { set; get; }

        /// <summary>
        /// Code of the Currency
        /// </summary>
        [DataMember(Name = "code", IsRequired = false)]
        public string Code { set; get; }

        /// <summary>
        /// Id of the Currency. It is generated and managed by Database.
        /// </summary>
        [DataMember(Name = "id", IsRequired = false)]
        public int Id { set; get; }
        #endregion

        public Currency () { }
    }
}