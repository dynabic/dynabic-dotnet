using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// Represents a Product Falmily. Used as a container for Products
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "product_family")]
    public class ProductFamilyBase
    {
        #region Data Members

        /// <summary>
        /// Id of the Site to which the ProductFamily belongs to
        /// </summary>
        [DataMember(Name = "site_id", IsRequired = true)]
        public int SiteId { get; set; }

        /// <summary>
        /// Name of the PrductFamily
        /// </summary>
        [DataMember(Name = "name", IsRequired = true)]
        public string Name { get; set; }

        /// <summary>
        /// Description of the Product Family
        /// </summary>
        [DataMember(Name = "description", IsRequired = false)]
        public string Description { get; set; }

        #endregion

        protected ProductFamilyBase()
        {
        }

    }
}