using DynabicPlatform.Classes;
using DynabicPlatform.RestAPI.RestInterfaces;
using DynabicPlatform.RestApiDataContract;

namespace DynabicPlatform.Services
{
    /// <summary>
    /// Provides operations for finding and updating applications
    /// </summary>
    public class ApplicationService : IApplicationsService
    {
        private readonly CommunicationLayer _service;
        private readonly string _gatewayURL;

        public ApplicationService(CommunicationLayer service)
        {
            _service = service;
            _gatewayURL = service.Environment.GatewayURL + "/apps";
        }

        /// <summary>
        /// Gets all applications supported by Dynabic Platform.
        /// </summary>
        /// <param name="format">The format of the response.</param>
        /// <returns>A list of applications.</returns>
        public ApplicationsList GetAllApplications(string format = ContentFormat.XML)
        {
            return _service.Get<ApplicationsList>(string.Format("{0}/{1}", _gatewayURL, format));
        }

        /// <summary>
        /// Gets the list of applications in company.
        /// </summary>
        /// <param name="format">The format of the response.</param>
        /// <returns>A list of applications.</returns>
        public ApplicationsInCompanyList GetCompanyApplications(string format = ContentFormat.XML)
        {
            return _service.Get<ApplicationsInCompanyList>(string.Format("{0}/bycompany/{1}", _gatewayURL, format));
        }

        /// <summary>
        /// Gets the company application by id.
        /// </summary>
        /// <param name="applicationId">The application id.</param>
        /// <param name="format">The response format.</param>
        /// <returns></returns>
        public ApplicationInCompanyResponse GetCompanyApplicationById(string applicationId, string format = ContentFormat.XML)
        {
            return _service.Get<ApplicationInCompanyResponse>(string.Format("{0}/{1}.{2}", _gatewayURL, applicationId, format));
        }

        /// <summary>
        /// Gets an ApplicationInCompany by Application name
        /// </summary>
        /// <param name="appName"> The Name of the Application </param>
        /// <param name="format"> The response format </param>
        /// <returns> An ApplicationInCompanyResponse object that corresponds to the specified Company Id and Application Name </returns>
        public ApplicationInCompanyResponse GetCompanyApplicationByName(string appName, string format = ContentFormat.XML)
        {
            return _service.Get<ApplicationInCompanyResponse>(string.Format("{0}/[{1}].{2}", _gatewayURL, appName, format));
        }

        /// <summary>
        /// Gets the application current plan.
        /// </summary>
        /// <param name="applicationId">The application id.</param>
        /// <param name="format">The response format.</param>
        /// <returns>
        /// The current plan
        /// </returns>
        public ApplicationPlan GetApplicationCurrentPlan(string applicationId, string format = ContentFormat.XML)
        {
            return _service.Get<ApplicationPlan>(string.Format("{0}/plan/{1}.{2}", _gatewayURL, applicationId, format));
        }

        /// <summary>
        /// Sets the application active status.
        /// </summary>
        /// <param name="applicationId">The application id.</param>
        /// <param name="status">The active status.</param>
        public void SetApplicationStatus(string applicationId, string status)
        {
            _service.Put(string.Format("{0}/{1}/{2}", _gatewayURL, applicationId, status));
        }
    }
}
