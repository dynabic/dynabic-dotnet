using DynabicPlatform.Classes;
using DynabicPlatform.RestAPI.RestInterfaces;
using DynabicPlatform.RestApiDataContract;

namespace DynabicPlatform.Services
{
    public class SiteService : ISitesService
    {
        private readonly CommunicationLayer _service;
        private readonly string _gatewayURL;

        public SiteService(CommunicationLayer service)
        {
            _service = service;
            _gatewayURL = service.Environment.GatewayURL + "/sites";
        }

        /// <summary>
        /// Gets all Sites for a Company
        /// </summary>
        /// <param name="format"> The format of the Response </param>
        /// <param name="pageNumber">The page number (Optional parameter, if is specified should be equal or greater than 1).</param>
        /// <param name="pageSize">Size of the page (Optional parameter, if is specified should be equal or greater than 1).</param>
        /// <returns> A SitesList object contaning all Sites in the Company </returns>
        public SitesList GetSites(string format = ContentFormat.XML, string pageNumber = null, string pageSize = null)
        {
            return _service.Get<SitesList>(string.Format("{0}/{1}?pageNumber={2}&pageSize={3}", _gatewayURL, format, pageNumber, pageSize));
        }

        /// <summary>
        /// Gets a Site, by Id
        /// </summary>
        /// <param name="siteId"> The Id of the Site </param>
        /// <param name="format"> The format of the Response </param>
        /// <returns> A SiteResponse object that corresponds to the specified Id </returns>
        public SiteResponse GetSiteById(string siteId, string format = ContentFormat.XML)
        {
            return _service.Get<SiteResponse>(string.Format("{0}/{1}.{2}", _gatewayURL, siteId, format));
        }

        /// <summary>
        /// Gets a Site, by Subdomain
        /// </summary>
        /// <param name="siteSubdomain"> The Subdomain of the Site </param>
        /// <param name="format"> The format of the Response </param>
        /// <returns> A SiteResponse object that corresponds to the specified Subdomain </returns>
        public SiteResponse GetSiteBySubdomain(string siteSubdomain, string format = ContentFormat.XML)
        {
            return _service.Get<SiteResponse>(string.Format("{0}/subdomain/{1}.{2}", _gatewayURL, siteSubdomain, format));
        }

        /// <summary>
        /// Gets a Site, by Name
        /// </summary>
        /// <param name="siteName"> The Name of the Site </param>
        /// <param name="format"> The format of the Response </param>
        /// <returns> A SiteResponse object that corresponds to the specified Name </returns>
        public SitesList GetSitesByName(string siteName, string format = ContentFormat.XML)
        {
            return _service.Get<SitesList>(string.Format("{0}/name/{1}.{2}", _gatewayURL, siteName, format));
        }

        /// <summary>
        /// Adds a new Site
        /// </summary>
        /// <param name="newSite"> The Site to be added </param>
        /// <param name="format"> The format of the Response </param>
        /// <returns> The SiteResponse object that corresponds to the newly-added Site </returns>
        public SiteResponse AddSite(SiteRequest newSite, string format = ContentFormat.XML)
        {
            return _service.Post<SiteRequest, SiteResponse>(string.Format("{0}/{1}", _gatewayURL, format), newSite);
        }

        /// <summary>
        /// Updates an existing Site
        /// </summary>
        /// <param name="updatedSite">The Site to be updated</param>
        /// <param name="siteId">The site id.</param>
        /// <param name="format">The format of the Response</param>
        /// <returns>
        /// The SiteResponse object that corresponds to the newly-updated Site
        /// </returns>
        public SiteResponse UpdateSite(SiteRequest updatedSite, string siteId, string format = ContentFormat.XML)
        {
            return _service.Put<SiteRequest, SiteResponse>(string.Format("{0}/{1}.{2}", _gatewayURL, siteId, format), updatedSite);
        }

        /// <summary>
        /// Deletes a Site
        /// </summary>
        /// <param name="siteId">The site id.</param>
        public void DeleteSite(string siteId)
        {
            _service.Delete(string.Format("{0}/{1}", _gatewayURL, siteId));
        }
    }
}
