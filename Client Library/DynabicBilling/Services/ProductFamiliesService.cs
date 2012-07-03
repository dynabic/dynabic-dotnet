#pragma warning disable 1591

using DynabicBilling.RestApiDataContract;
using DynabicBilling.RestApiDataContract.RestInterfaces;
using DynabicPlatform.Classes;

namespace DynabicBilling
{
    /// <summary>
    /// Provides operations for creating, finding, updating, and deleting product families
    /// </summary>
    public class ProductFamiliesService : IProductFamiliesService
    {
        private CommunicationLayer _service;
        private readonly string _gatewayURL;

        protected internal ProductFamiliesService(CommunicationLayer service)
        {
            _service = service;
            _gatewayURL = service.Environment.GatewayURL + "/productfamily";
        }

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
        /// <returns> A ProductFamilyResponseList object containing all Product Families for the specified Site </returns>
        public ProductFamilyResponseList GetProductFamilies(string siteSubdomain, string format = ContentFormat.XML, string isArchived = null, string pageNumber = null, string pageSize = null)
        {
            return _service.Get<ProductFamilyResponseList>(string.Format("{0}/{1}/{2}?isArchived={3}&pageNumber={4}&pageSize={5}", _gatewayURL, siteSubdomain, format, isArchived, pageNumber, pageSize));
        }

        /// <summary>
        /// Gets a Product Family by Id
        /// </summary>
        /// <param name="productFamilyId"> The Id of the Product Family </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <returns> A ProductFamilyResponse object corresponding to the specified Id </returns>
        public ProductFamilyResponse GetProductFamilyById(string productFamilyId, string format = ContentFormat.XML)
        {
            return _service.Get<ProductFamilyResponse>(string.Format("{0}/{1}.{2}", _gatewayURL, productFamilyId, format));
        }

        /// <summary>
        /// Gets a Product Family by Name
        /// </summary>
        /// <param name="siteSubdomain"> The Subdomain of the Site to which the Product Family belongs </param>
        /// <param name="productFamilyName"> The Name of the Product Family </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <returns> A ProductFamilyResponse object corresponding to the specified Name </returns>
        public ProductFamilyResponse GetProductFamilyByName(string siteSubdomain, string productFamilyName, string format = ContentFormat.XML)
        {
            return _service.Get<ProductFamilyResponse>(string.Format("{0}/{1}/[{2}].{3}", _gatewayURL, siteSubdomain, productFamilyName, format));
        }

        #endregion GET

        #region POST

        /// <summary>
        /// Adds a new Product Family
        /// </summary>
        /// <param name="newProductFamily"> A ProductFamilyRequest object containing the data for the Product Family to be created </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <returns> A ProductFamilyResponse object corresponding to the newly-added ProductFamily record </returns>
        public ProductFamilyResponse AddProductFamily(ProductFamilyRequest newProductFamily, string format = ContentFormat.XML)
        {
            return _service.Post<ProductFamilyRequest, ProductFamilyResponse>(string.Format("{0}/{1}", _gatewayURL, format), newProductFamily);
        }

        #endregion POST

        #region PUT

        /// <summary>
        /// Updates a Product Family
        /// </summary>
        /// <param name="updatedFamily"> A ProductFamilyRequest object containing the updated Product Family record </param>
        /// <param name="productFamilyId"> The Id of the Product Family to be updated </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <returns> A ProductFamilyResponse object corresponding to the updated ProductFamily record </returns>
        public ProductFamilyResponse UpdateProductFamily(ProductFamilyRequest updatedFamily, string productFamilyId, string format = ContentFormat.XML)
        {
            return _service.Put<ProductFamilyRequest, ProductFamilyResponse>(string.Format("{0}/{1}.{2}", _gatewayURL, productFamilyId, format), updatedFamily);
        }

        #endregion PUT

        #region DELETE

        /// <summary>
        /// Deletes a Product Family
        /// </summary>
        /// <param name="productFamilyId"> The Id of the Product Family </param>
        public void DeleteProductFamily(string productFamilyId)
        {
            _service.Delete(string.Format("{0}/{1}", _gatewayURL, productFamilyId));
        }

        #endregion DELETE

        #region Misc

        /// <summary>
        /// Archives a Product Family
        /// </summary>
        /// <param name="productFamilyId"> The Id of the Product Family </param>
        public void ArchiveProductFamily(string productFamilyId)
        {
            _service.Put(string.Format("{0}/archive/{1}", _gatewayURL, productFamilyId));
        }

        /// <summary>
        /// Activates a Product Family that was previously archived
        /// </summary>
        /// <param name="productFamilyId"> The Id of the Product Family </param>
        /// <param name="activateProducts"> 
        /// Flag indicating whether to also activate all Products belonging to the Product Family
        /// If set to "true", then the Products will be activated, too
        /// </param>
        public void ActivateProductFamily(string productFamilyId, string activateProducts = null)
        {
            _service.Put(string.Format("{0}/activate/{1}?activateProducts={2}", _gatewayURL, productFamilyId, activateProducts));
        }

        #endregion Misc
    }
}
