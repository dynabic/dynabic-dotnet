using DynabicPlatform.RestApiDataContract;

namespace DynabicPlatform.RestAPI.RestInterfaces
{
    public interface IDynabicMarketingPlansService
    {
        /// <summary>
        /// S
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        SetActiveMarketingPlanResponse SetActiveMarketingPlan(string productId);
    }
}
