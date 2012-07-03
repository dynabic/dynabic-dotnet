using DynabicPlatform.RestApiDataContract;

namespace DynabicPlatform.RestAPI.RestInterfaces
{
    public interface IApplicationsService
    {

        #region GET

        /// <summary>
        /// Retrievs all Applications supported by Dynabic DynabicPlatform.
        /// </summary>
        /// <param name="format">The format used for the data transfer (XML or JSON).</param>
        /// <returns>An ApplicationsList object containing all requested Applications.</returns>
        ApplicationsList GetAllApplications(string format = "xml");

        /// <summary>
        /// Retrievs all Applications used by Company.
        /// </summary>
        /// <param name="format">The format used for the data transfer (XML or JSON).</param>
        /// <returns>An ApplicationsInCompanyList object containing all requested Applications.</returns>
        ApplicationsInCompanyList GetCompanyApplications(string format = "xml");

        /// <summary>
        /// Retrievs an Application by Id.
        /// </summary>
        /// <param name="applicationId">The Application's unique identifier.</param>
        /// <param name="format">The format used for the data transfer (XML or JSON).</param>
        /// <returns>An ApplicationInCompanyBase object containing the requested  Application</returns>
        ApplicationInCompanyResponse GetCompanyApplicationById(string applicationId, string format = "xml");

        /// <summary>
        /// Gets an ApplicationInCompany by Application name
        /// </summary>
        /// <param name="appName"> The Name of the Application </param>
        /// <param name="format">The format used for the data transfer (XML or JSON).</param>
        /// <returns> An ApplicationInCompanyResponse object that corresponds to the specified Company Id and Application Name </returns>
        ApplicationInCompanyResponse GetCompanyApplicationByName(string appName, string format = "xml");

        /// <summary>
        /// Retrievs the PricingPlan used for the Application
        /// </summary>
        /// <param name="applicationId">The Application's unique identifier.</param>
        /// <param name="format">The format used for the data transfer (XML or JSON) </param>
        /// <returns>
        /// An ApplicationPlan object containing currently selected Pricing Plan for requested Application.
        /// The current plan
        /// </returns>
        ApplicationPlan GetApplicationCurrentPlan(string applicationId, string format = "xml");

        #endregion GET

        #region Misc

        /// <summary>
        /// Sets the application active status.
        /// </summary>
        /// <param name="applicationId">The application id.</param>
        /// <param name="status">The active status.</param>
        void SetApplicationStatus(string applicationId, string status);

        #endregion Misc
    }
}
