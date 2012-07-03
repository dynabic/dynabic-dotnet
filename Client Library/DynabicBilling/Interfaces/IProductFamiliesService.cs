namespace DynabicBilling.RestApiDataContract.RestInterfaces
{
    public interface IProductFamiliesService
    {
        #region GET

        /// <summary>
        /// Gets all Product Families for a Site
        /// </summary>
        /// <param name="siteSubdomain"> The Subdomain of the Site for which to retrieve all Product Families </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <param name="isArchived"> 
        /// Flag indicating whether to retrieve archived or non-archived Product Families
        /// If set to "true", then archived Product Families will be returned 
        /// </param>
        /// <param name="pageNumber"> 
        /// Optional parameter to be used when a paged response is expected
        /// Use it to specify the index of the page to be retrieved (this value has to be greater than or equal to 1)
        /// </param>
        /// <param name="pageSize">
        /// Optional parameter to be used when a paged response is expected
        /// Use it to specify the number of records a page should contain (this value has to be greater than or equal to 1) 
        /// </param>
        /// <returns></returns>
        ProductFamilyResponseList GetProductFamilies(string siteSubdomain, string format = "xml", string isArchived = null, string pageNumber = null, string pageSize = null);

        /// <summary>
        /// Gets a Product Family by Id
        /// </summary>
        /// <param name="productFamilyId"> The Id of the Product Family </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <returns> A ProductFamilyResponse object corresponding to the specified Id </returns>
        ProductFamilyResponse GetProductFamilyById(string productFamilyId, string format = "xml");

        /// <summary>
        /// Gets a Product Family by Name
        /// </summary>
        /// <param name="siteSubdomain"> The Subdomain of the Site to which the Product Family belongs </param>
        /// <param name="productFamilyName"> The Name of the Product Family </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <returns> A ProductFamilyResponse object corresponding to the specified Name </returns>
        ProductFamilyResponse GetProductFamilyByName(string siteSubdomain, string productFamilyName, string format = "xml");

        #endregion GET

        #region POST

        /// <summary>
        /// Adds a new Product Family
        /// </summary>
        /// <param name="newProductFamily"> A ProductFamilyRequest object containing the data for the Product Family to be created </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <returns> A ProductFamilyResponse object corresponding to the newly-added ProductFamily record </returns>
        ProductFamilyResponse AddProductFamily(ProductFamilyRequest newProductFamily, string format = "xml");

        #endregion POST

        #region PUT

        /// <summary>
        /// Updates a Product Family
        /// </summary>
        /// <param name="updatedFamily"> A ProductFamilyRequest object containing the updated Product Family record </param>
        /// <param name="productFamilyId"> The Id of the Product Family to be updated </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <returns> A ProductFamilyResponse object corresponding to the updated ProductFamily record </returns>
        ProductFamilyResponse UpdateProductFamily(ProductFamilyRequest updatedFamily, string productFamilyId, string format = "xml");

        #endregion PUT

        #region DELETE

        /// <summary>
        /// Deletes a Product Family
        /// </summary>
        /// <param name="productFamilyId"> The Id of the Product Family </param>
        void DeleteProductFamily(string productFamilyId);

        #endregion DELETE

        #region Misc

        /// <summary>
        /// Archives a Product Family
        /// </summary>
        /// <param name="productFamilyId"> The Id of the Product Family </param>
        void ArchiveProductFamily(string productFamilyId);

        /// <summary>
        /// Activates a Product Family that was previously archived
        /// </summary>
        /// <param name="productFamilyId"> The Id of the Product Family </param>
        /// <param name="activateProducts"> 
        /// Flag indicating whether to also activate all Products belonging to the Product Family
        /// If set to "true", then the Products will be activated, too
        /// </param>
        void ActivateProductFamily(string productFamilyId, string activateProducts = null);

        #endregion Misc
    }
}
