using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// Product Item
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "product_item")]
    public class ProductItemBase
    {
        #region Data Members

        /// <summary>
        /// ProductItem's unique identifier. It is generated and managed by database
        /// </summary>
        [DataMember(Name = "id", IsRequired = true)]
        public int Id { get; set; }

        /// <summary>
        /// Id of a child ProductItem, if such exists. Otherwise, this field will remain NULL
        /// </summary>
        [DataMember(Name = "product_item_id", IsRequired = false)]
        public int? ProductItemId { get; set; }

        /// <summary>
        /// Name of the ProductItem
        /// </summary>
        [DataMember(Name = "name", IsRequired = true)]
        public string Name { get; set; }

        /// <summary>
        /// Description of the ProductItem
        /// </summary>
        [DataMember(Name = "description", IsRequired = false)]
        public string Description { get; set; }

        /// <summary>
        /// Type of the charge modelused for the ProductItem
        /// </summary>
        [DataMember(Name = "charge_model", IsRequired = true)]
        public ChargeModel ChargeModel { get; set; }

        /// <summary>
        /// Gets or sets the ProductItemType
        /// </summary>
        [DataMember(Name = "item_type", IsRequired = true)]
        public ProductItemType ProductItemType { get; set; }

        /// <summary>
        /// Name of the unit of the ProductItem
        /// </summary>
        [DataMember(Name = "unit_name", IsRequired = true)]
        public string UnitName { get; set; }

        /// <summary>
        /// Whether or not this ProductItem
        /// should be visible on Hosted Pages
        /// 
        /// True, by default (if not specified)
        /// </summary>
        [DataMember(Name="is_visible_on_hosted_page", IsRequired = false)]
        public bool IsVisibleOnHostedPage { get; set; }

        #endregion

        protected ProductItemBase()
        {
        }
    }
}