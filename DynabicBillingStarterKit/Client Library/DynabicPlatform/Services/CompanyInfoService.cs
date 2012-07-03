using DynabicPlatform.Classes;
using DynabicPlatform.RestAPI.RestInterfaces;
using DynabicPlatform.RestApiDataContract;

namespace DynabicPlatform.Services
{
    public class CompanyInfoService : ICompanyInfoService
    {
        private readonly CommunicationLayer _service;
        private readonly string _gatewayURL;

        public CompanyInfoService(CommunicationLayer service)
        {
            _service = service;
            _gatewayURL = service.Environment.GatewayURL + "/companyinfo";
        }

        /// <summary>
        /// Gets a Company info by API key
        /// </summary>
        /// <param name="format">The format of the Response</param>
        /// <returns>A CompanyInfo object</returns>
        public CompanyInfoResponse GetCompanyInfo(string format = ContentFormat.XML)
        {
            return _service.Get<CompanyInfoResponse>(string.Format("{0}/{1}", _gatewayURL, format));
        }

        /// <summary>
        /// Updates the CompanyInfo
        /// </summary>
        /// <param name="updatedCompanyInfo">The CompanyInfo to be updated</param>
        /// <param name="format">The format of the Response</param>
        /// <returns>A CompanyInfoResponse object that corresponds to the updated CompanyInfo</returns>
        public CompanyInfoResponse UpdateCompanyInfo(CompanyInfoRequest updatedCompanyInfo, string format = ContentFormat.XML)
        {
            return _service.Put<CompanyInfoRequest, CompanyInfoResponse>(string.Format("{0}/{1}", _gatewayURL, format), updatedCompanyInfo);
        }
    }
}
