using DynabicPlatform.Classes;
using DynabicPlatform.RestAPI.RestInterfaces;
using DynabicPlatform.RestApiDataContract;

namespace DynabicPlatform.Services
{
    public class DynabicMarketingPlansService : IDynabicMarketingPlansService
    {
        private readonly CommunicationLayer _service;
        private readonly string _gatewayURL;

        public DynabicMarketingPlansService(CommunicationLayer service)
        {
            _service = service;
            // TODO: this REST service is not implemented yet.
            _gatewayURL = service.Environment.GatewayURL + "";
        }

        /// <summary>
        /// Activates the marketing plan.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <returns></returns>
        public SetActiveMarketingPlanResponse SetActiveMarketingPlan(string productId)
        {
            return _service.Put<SetActiveMarketingPlanResponse>(string.Format("{0}/activate/{1}", _gatewayURL, productId));
        }
    }
}
