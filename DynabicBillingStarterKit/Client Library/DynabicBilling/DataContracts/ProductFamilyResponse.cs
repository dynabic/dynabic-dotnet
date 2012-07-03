using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// A Product Family used as a response to user's request
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "product_family_response")]
    public class ProductFamilyResponse : ProductFamilyBase
    {
        #region Data Members

        /// <summary>
        /// ProductFamily's unique identifier. It is generated and managed by the database
        /// </summary>
        [DataMember(Name = "id", IsRequired = true)]
        public int Id { get; set; }

        /// <summary>
        /// Shows if the ProductFamily is marked as Archived
        /// </summary>
        [DataMember(Name = "is_archived", IsRequired = false)]
        public bool isArchived { get; set; }

        /// <summary>
        /// A list of Products that belong to ProductFamily represented as a collection of ProductResponse objects
        /// </summary>
        [DataMember(Name = "products", IsRequired = false)]
        public ProductResponseList Products { get; set; }

        #endregion

        public ProductFamilyResponse()
        {
            this.Products = new ProductResponseList();
        }

        public static implicit operator ProductFamilyRequest(ProductFamilyResponse response)
        {
            if (response == null) return null;
            return new ProductFamilyRequest
            {
                Description = response.Description,
                Name = response.Name,
                Products = response.Products,
                SiteId = response.SiteId,
            };
        }
    }
}