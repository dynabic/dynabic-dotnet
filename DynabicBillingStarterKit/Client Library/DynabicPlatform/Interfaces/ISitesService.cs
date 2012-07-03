using DynabicPlatform.RestApiDataContract;

namespace DynabicPlatform.RestAPI.RestInterfaces
{
    public interface ISitesService
    {
        #region GET

        /// <summary>
        /// Retrieves all Sites belonging a Company
        /// </summary>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <param name="pageNumber">
        /// Optional parameter to be used when a paged response is expected.
        /// Use it to specify the index of the page to be retrieved (this value has to be greater than or equal to 1).
        /// </param>
        /// <param name="pageSize">
        /// Optional parameter to be used when a paged response is expected.
        /// Use it to specify the number of records a page should contain (this value has to be greater than or equal to 1).
        /// </param>
        // <returns> A SitesList object contaning all Sites in the Company </returns>
        SitesList GetSites(string format = "xml", string pageNumber = null, string pageSize = null);

        /// <summary>
        /// Retrives a Site using Site's unique identifier Site.Id
        /// </summary>
        /// <param name="siteId"> The Id of the Site to retrieve </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <returns> A SiteResponse object that corresponds to the specified Id </returns>
        SiteResponse GetSiteById(string siteId, string format = "xml");

        /// <summary>
        /// Retrives a Site using Site's subdomain
        /// </summary>
        /// <param name="siteSubdomain"> The subdomain of the Site to retrieve </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <returns> A SiteResponse object that corresponds to the specified subdomain </returns>
        SiteResponse GetSiteBySubdomain(string siteSubdomain, string format = "xml");

        /// <summary>
        /// Retrives a Site using Site's name
        /// </summary>
        /// <param name="siteName"> The name of the Site </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <returns> A SiteResponse object that corresponds to the specified name </returns>
        SitesList GetSitesByName(string siteName, string format = "xml");

        #endregion

        #region POST

        /// <summary>
        /// Adds a new Site
        /// </summary>
        /// <param name="newSite"> The Site to be added </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <returns> The SiteResponse object that corresponds to the newly-added Site </returns>
        SiteResponse AddSite(SiteRequest newSite, string format = "xml");

        #endregion

        #region PUT

        /// <summary>
        /// Updates an existing Site
        /// </summary>
        /// <param name="updatedSite">The Site to be updated</param>
        /// <param name="siteId">The Site's unique identifier to be updated.</param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <returns>
        /// The SiteResponse object that corresponds to the newly-updated Site
        /// </returns>
        SiteResponse UpdateSite(SiteRequest updatedSite, string siteId, string format = "xml");

        #endregion

        #region DELETE

        /// <summary>
        /// Deletes a Site
        /// </summary>
        /// <param name="siteId"> The Site's unque identifier to be deleted.</param>
        void DeleteSite(string siteId);

        #endregion
    }
}
