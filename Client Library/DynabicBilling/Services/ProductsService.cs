#pragma warning disable 1591

using DynabicBilling.RestAPI.RestInterfaces;
using DynabicBilling.RestApiDataContract;
using DynabicPlatform.Classes;

namespace DynabicBilling
{
    /// <summary>
    /// Provides operations for creating, finding, updating, and deleting products
    /// </summary>
    public class ProductsService : IProductsService
    {
        private CommunicationLayer _service;
        private readonly string _gatewayURL;

        protected internal ProductsService(CommunicationLayer service)
        {
            _service = service;
            _gatewayURL = service.Environment.GatewayURL + "/products";
        }

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
        public ProductResponseList GetProductsBySite(string siteSubdomain, string format = ContentFormat.XML, string isArchived = null, string pageNumber = null, string pageSize = null)
        {
            return _service.Get<ProductResponseList>(string.Format("{0}/bysite/{1}/{2}?isArchived={3}&pageNumber={4}&pageSize={5}", _gatewayURL, siteSubdomain, format, isArchived, pageNumber, pageSize));
        }

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
        public ProductResponseList GetProductsBySiteAndFamily(string siteSubdomain, string productFamilyName, string format = ContentFormat.XML, string isArchived = null)
        {
            return _service.Get<ProductResponseList>(string.Format("{0}/bysite/{1}/[{2}]/{3}?isArchived={4}", _gatewayURL, siteSubdomain, productFamilyName, format, isArchived));
        }

        /// <summary>
        /// Gets all Products from a Site which have the same ProductName.
        /// </summary>
        /// <param name="siteSubdomain">The subdomain of the Site for which to retrieve the Products.</param>
        /// <param name="productName">Name of the Product to be compared.</param>
        /// <param name="format">The format used for the data transfer (XML or JSON).</param>
        /// <returns>A ProductResponseList object containing requested Product records. </returns>
        public ProductResponseList GetProductsBySiteAndProductName(string siteSubdomain, string productName, string format = ContentFormat.XML)
        {
            return _service.Get<ProductResponseList>(string.Format("{0}/bysite/{1}/[{2}].{3}", _gatewayURL, siteSubdomain, productName, format));
        }

        /// <summary>
        /// Gets all Products from a Product Family.
        /// </summary>
        /// <param name="productFamilyId">The Product Family ID for which Products shell be retrieved. </param>
        /// <param name="format">The format used for the data transfer (XML or JSON). </param>
        /// <param name="isArchived">If set to "true" the list of results will only contain Products marked as "Archived". </param>
        /// <returns>A ProductResponseList object containing requested Product records. </returns>
        public ProductResponseList GetProductsByFamilyId(string productFamilyId, string format = ContentFormat.XML, string isArchived = null)
        {
            return _service.Get<ProductResponseList>(string.Format("{0}/byfamily/{1}/{2}?isArchived={3}", _gatewayURL, productFamilyId, format, isArchived));
        }

        /// <summary>
        /// Gets a Product within a ProductFamily
        /// </summary>
        /// <param name="productFamilyId">The Product Family Id for which the Product shell be retrieved. </param>
        /// <param name="productName">Name of the Product to be retrieved. </param>
        /// <param name="format">The format used for the data transfer (XML or JSON). </param>
        /// <returns>A ProductResponse object corresponding to the specified ProductFamilyId and ProductName. </returns>
        public ProductResponse GetProductByFamilyIdAndProductName(string productFamilyId, string productName, string format = ContentFormat.XML)
        {
            return _service.Get<ProductResponse>(string.Format("{0}/byfamily/{1}/[{2}].{3}", _gatewayURL, productFamilyId, productName, format));
        }

        /// <summary>
        /// Gets a Product by Id.
        /// </summary>
        /// <param name="productId">The Id of the Product to be retrieved. </param>
        /// <param name="format">The format used for the data transfer (XML or JSON).</param>
        /// <returns>A ProductResponse object corresponding to the specified ProductId. </returns>
        public ProductResponse GetProductById(string productId, string format = ContentFormat.XML)
        {
            return _service.Get<ProductResponse>(string.Format("{0}/{1}.{2}", _gatewayURL, productId, format));
        }

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
        public ProductResponse GetProductByApiRef(string siteId, string apiRef, string format = ContentFormat.XML)
        {
            return _service.Get<ProductResponse>(string.Format("{0}/byapiref/{1}/{2}.{3}", _gatewayURL, siteId, apiRef, format));
        }

        /// <summary>
        /// Adds a new Product.
        /// </summary>
        /// <param name="newProduct">A ProductRequest object containing the data for the Product to be created.</param>
        /// <param name="format">The format used for the data transfer (XML or JSON). </param>
        /// <returns>>The ProductResponse object that corresponds to the newly-added Product record. </returns>
        public ProductResponse AddProduct(ProductRequest newProduct, string format = ContentFormat.XML)
        {
            return _service.Post<ProductRequest, ProductResponse>(string.Format("{0}/{1}", _gatewayURL, format), newProduct);
        }

        /// <summary>
        /// Updates a Product.
        /// </summary>
        /// <param name="updatedProduct">A ProductRequest object containing the Product record to be updated.</param>
        /// <param name="productId">The Id of the Product to be updated. </param>
        /// <param name="format">The format used for the data transfer (XML or JSON). </param>
        /// <returns>The ProductResponse object that corresponds to the updated Product</returns>
        public ProductResponse UpdateProduct(ProductRequest updatedProduct, string productId, string format = ContentFormat.XML)
        {
            return _service.Put<ProductRequest, ProductResponse>(string.Format("{0}/{1}.{2}", _gatewayURL, productId, format), updatedProduct);
        }

        /// <summary>
        /// Deletes a Product.
        /// </summary>
        /// <param name="productId">The Id of the Product to be deleted. </param>
        public void DeleteProduct(string productId)
        {
            _service.Delete(string.Format("{0}/{1}", _gatewayURL, productId));
        }

        /// <summary>
        /// Archives a Product.
        /// </summary>
        /// <param name="productId">Id of the Product to be Archieved. </param>
        public void ArchiveProduct(string productId)
        {
            _service.Put(string.Format("{0}/archive/{1}", _gatewayURL, productId));
        }

        /// <summary>
        /// Activates a Product.
        /// </summary>
        /// <param name="productId">Id of the Product to be Activated. </param>
        public void ActivateProduct(string productId)
        {
            _service.Put(string.Format("{0}/activate/{1}", _gatewayURL, productId));
        }
    }
}
