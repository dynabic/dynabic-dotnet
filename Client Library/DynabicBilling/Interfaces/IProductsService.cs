using DynabicBilling.RestApiDataContract;

namespace DynabicBilling.RestAPI.RestInterfaces
{
    public interface IProductsService
    {
        #region GET

        /// <summary>
        /// Gets all Products for a Site
        /// </summary>
        /// <param name="siteSubdomain">The subdomain of the Site for which to retrieve the Products.</param>
        /// <param name="format">The format used for the data transfer (XML or JSON).</param>
        /// <param name="isArchived">
        /// If set to "true" the list of results will only contain Products marked as "Archived".
        /// </param>
        /// <param name="pageNumber">
        /// Optional parameter to be used when a paged response is expected.
        /// Use it to specify the index of the page to be retrieved (this value has to be greater than or equal to 1).
        /// </param>
        /// <param name="pageSize">
        /// Optional parameter to be used when a paged response is expected.
        /// Use it to specify the number of records a page should contain (this value has to be greater than or equal to 1).
        /// </param>
        /// <returns>A ProductResponseList object containing requested Product records. </returns>
        ProductResponseList GetProductsBySite(string siteSubdomain, string format = "xml", string isArchived = null, string pageNumber = null, string pageSize = null);

        /// <summary>
        /// Gets all Products for a Site within a Product Family.
        /// </summary>
        /// <param name="siteSubdomain">The subdomain of the Site for which to retrieve the Products.</param>
        /// <param name="productFamilyName">Name of the Product Family to which Products belong.</param>
        /// <param name="format">The format used for the data transfer (XML or JSON).</param>
        /// <param name="isArchived">
        /// If set to "true" the list of results will only contain Products marked as "Archived".
        /// </param>
        /// <returns>A ProductResponseList object containing requested Product records. </returns>
        ProductResponseList GetProductsBySiteAndFamily(string siteSubdomain, string productFamilyName, string format = "xml", string isArchived = null);

        /// <summary>
        /// Gets all Products from a Site which have the same ProductName.
        /// </summary>
        /// <param name="siteSubdomain">The subdomain of the Site for which to retrieve the Products.</param>
        /// <param name="productName">Name of the Product to be compared.</param>
        /// <param name="format">The format used for the data transfer (XML or JSON).</param>
        /// <returns>A ProductResponseList object containing requested Product records. </returns>
        ProductResponseList GetProductsBySiteAndProductName(string siteSubdomain, string productName, string format = "xml");

        /// <summary>
        /// Gets all Products from a Product Family.
        /// </summary>
        /// <param name="productFamilyId">The Product Family ID for which Products shell be retrieved. </param>
        /// <param name="format">The format used for the data transfer (XML or JSON). </param>
        /// <param name="isArchived">If set to "true" the list of results will only contain Products marked as "Archived". </param>
        /// <returns>A ProductResponseList object containing requested Product records. </returns>
        ProductResponseList GetProductsByFamilyId(string productFamilyId, string format = "xml", string isArchived = null);

        /// <summary>
        /// Gets a Product within a ProductFamily
        /// </summary>
        /// <param name="productFamilyId">The Product Family Id for which the Product shell be retrieved. </param>
        /// <param name="productName">Name of the Product to be retrieved. </param>
        /// <param name="format">The format used for the data transfer (XML or JSON). </param>
        /// <returns>A ProductResponse object corresponding to the specified ProductFamilyId and ProductName. </returns>
        ProductResponse GetProductByFamilyIdAndProductName(string productFamilyId, string productName, string format = "xml");

        /// <summary>
        /// Gets a Product by Id.
        /// </summary>
        /// <param name="productId">The Id of the Product to be retrieved. </param>
        /// <param name="format">The format used for the data transfer (XML or JSON).</param>
        /// <returns>A ProductResponse object corresponding to the specified ProductId. </returns>
        ProductResponse GetProductById(string productId, string format = "xml");

        /// <summary>
        /// Gets a Product by ApiRef
        /// </summary>
        /// <param name="siteId"> 
        /// The Id of the Site for which Product belongs to. 
        /// This is required since ApiRef is only unique within a Site's range.
        /// </param>        
        /// <param name="apiRef"> The ApiRef string used as a friendly identifier for the Product </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <returns> A ProductResponse object that corresponds to the specified ApiRef. </returns>
        ProductResponse GetProductByApiRef(string siteId, string apiRef, string format = "xml");

        #endregion GET

        #region POST

        /// <summary>
        /// Adds a new Product.
        /// </summary>
        /// <param name="newProduct">A ProductRequest object containing the data for the Product to be created.</param>
        /// <param name="format">The format used for the data transfer (XML or JSON). </param>
        /// <returns>>The ProductResponse object that corresponds to the newly-added Product record. </returns>
        ProductResponse AddProduct(ProductRequest newProduct, string format = "xml");

        #endregion POST

        #region PUT

        /// <summary>
        /// Updates a Product.
        /// </summary>
        /// <param name="updatedProduct">A ProductRequest object containing the Product record to be updated.</param>
        /// <param name="productId">The Id of the Product to be updated. </param>
        /// <param name="format">The format used for the data transfer (XML or JSON). </param>
        /// <returns>The ProductResponse object that corresponds to the updated Product</returns>
        ProductResponse UpdateProduct(ProductRequest updatedProduct, string productId, string format = "xml");

        #endregion PUT

        #region DELETE

        /// <summary>
        /// Deletes a Product.
        /// </summary>
        /// <param name="productId">The Id of the Product to be deleted. </param>
        void DeleteProduct(string productId);

        #endregion DELETE

        #region Misc

        /// <summary>
        /// Archives a Product.
        /// </summary>
        /// <param name="productId">Id of the Product to be Archieved. </param>
        void ArchiveProduct(string productId);

        /// <summary>
        /// Activates a Product.
        /// </summary>
        /// <param name="productId">Id of the Product to be Activated. </param>
        void ActivateProduct(string productId);

        #endregion Misc
    }
}
