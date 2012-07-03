using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    [DataContract(Namespace = "v1.0", Name = "metered_price")]
    public class ProductMeteredPriceBase
    {
        #region Data Members

        /// <summary>
        /// Name of the metered Product
        /// </summary>
        [DataMember(Name = "id", IsRequired = true)]
        public int Id { get; set; }

        /// <summary>
        /// Minimum value that a metered product may have
        /// </summary>
        [DataMember(Name = "start_quantity", IsRequired = true)]
        public decimal StartQuantity { get; set; }

        /// <summary>
        /// Maximum value that a metered product may have
        /// </summary>
        [DataMember(Name = "end_quantity", IsRequired = true)]
        public decimal? EndQuantity { get; set; }

        /// <summary>
        /// Price per unit of the metered Product
        /// </summary>
        [DataMember(Name = "unit_price", IsRequired = true)]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [DataMember(Name = "description", IsRequired = false)]
        public string Description { get; set; }

        #endregion
    }
}
