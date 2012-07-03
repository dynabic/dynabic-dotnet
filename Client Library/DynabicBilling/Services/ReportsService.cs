#pragma warning disable 1591

using DynabicBilling.RestAPI.RestInterfaces;
using DynabicBilling.RestApiDataContract;
using DynabicPlatform.Classes;

namespace DynabicBilling
{
    /// <summary>
    /// Provides methods for retrieving reports
    /// </summary>
    public class ReportsService : IReportsService
    {
        private CommunicationLayer _service;
        private readonly string _gatewayURL;

        protected internal ReportsService(CommunicationLayer service)
        {
            _service = service;
            _gatewayURL = service.Environment.GatewayURL + "/reports";
        }

        /// <summary>
        /// Gets the source data from OLAP cube for the "Products Signups" report.
        /// </summary>
        /// <param name="siteSubdomain"> The subdomain of the Site to which the Products belong to. </param>
        /// <param name="startDate"> Start date from when Product's signups are evaluated. </param>
        /// <param name="endDate"> End date till when Product's signups are evaluated. </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <returns> A ProductsSignupsEvolutionResponse object containing report data for evolution of Product's signups. </returns>
        public ProductsSignupsEvolutionResponse GetProductsSignupsEvolution(string siteSubdomain, string startDate, string endDate = null, string format = ContentFormat.XML)
        {
            return _service.Get<ProductsSignupsEvolutionResponse>(string.Format("{0}/ProductsSignupsEvolution/{1}/{2}/{3}?endDate={4}", _gatewayURL, siteSubdomain, startDate, format, endDate));
        }

        /// <summary>
        /// Gets the source data from OLAP cube for the "Products Revenues" report.
        /// </summary>
        /// <param name="siteSubdomain"> The subdomain of the Site to which the Products belong to. </param>
        /// <param name="startDate"> Start date from when Product's revenues are evaluated. </param>
        /// <param name="endDate"> End date till when Product's revenues are evaluated. </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <returns> A ProductsRevenuesEvolutionResponse object containing report data for evolution of Product's revenues. </returns>
        public ProductsRevenuesEvolutionResponse GetProductsRevenuesEvolution(string siteSubdomain, string startDate, string endDate = null, string format = ContentFormat.XML)
        {
            return _service.Get<ProductsRevenuesEvolutionResponse>(string.Format("{0}/ProductsRevenuesEvolution/{1}/{2}/{3}?endDate={4}", _gatewayURL, siteSubdomain, startDate, format, endDate));
        }

        /// <summary>
        /// Gets the total revenue amount for a Site
        /// </summary>
        /// <param name="siteSubdomain"> The subdomain of the Site for wich the total revenue is evaluated. </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <returns>A TotalRevenueAmountResponse object containing report data for Site's total revenues. </returns>
        public TotalRevenueAmountResponse GetTotalRevenueAmount(string siteSubdomain, string format = ContentFormat.XML)
        {
            return _service.Get<TotalRevenueAmountResponse>(string.Format("{0}/TotalRevenueAmount/{1}/{2}", _gatewayURL, siteSubdomain, format));
        }

        /// <summary>
        /// Gets the count of total active Subscriptions from a Site.
        /// </summary>
        /// <param name="siteSubdomain"> The subdomain of the Site for wich count of active Subscriptions is evaluated. </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <returns> A ActiveSubscriptionsCountResponse object containing report data about Site's active Subscriptions. </returns>
        public ActiveSubscriptionsCountResponse GetActiveSubscriptionsCount(string siteSubdomain, string format = ContentFormat.XML)
        {
            return _service.Get<ActiveSubscriptionsCountResponse>(string.Format("{0}/ActiveSubscriptionsCount/{1}/{2}", _gatewayURL, siteSubdomain, format));
        }

        /// <summary>
        /// Gets the total count of subscribers for a Site
        /// </summary>
        /// <param name="siteSubdomain"> The subdomain of the Site for wich count of subscribers is evaluated. </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <returns> A TotalSubscribersCountResponse object containing report data about Site's total subscribres count. </returns>
        public TotalSubscribersCountResponse GetTotalSubscribersCount(string siteSubdomain, string format = ContentFormat.XML)
        {
            return _service.Get<TotalSubscribersCountResponse>(string.Format("{0}/TotalSubscribersCount/{1}/{2}", _gatewayURL, siteSubdomain, format));
        }

        /// <summary>
        /// Gets the amount of today's revenues.
        /// </summary>
        /// <param name="siteSubdomain"> The subdomain of the Site for wich today's amount is evaluated. </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <returns> A TodayRevenueAmountResponse object containing report data about Site's today's revenues. </returns>
        public TodayRevenueAmountResponse GetTodayRevenueAmount(string siteSubdomain, string format = ContentFormat.XML)
        {
            return _service.Get<TodayRevenueAmountResponse>(string.Format("{0}/TodayRevenueAmount/{1}/{2}", _gatewayURL, siteSubdomain, format));
        }

        /// <summary>
        /// Gets the count of today's new subscribers.
        /// </summary>
        /// <param name="siteSubdomain"> The subdomain of the Site for wich today's new subscribers is evaluated. </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <returns> A TodayNewSubscribersCountResponse object containing report data for Site's today's new subscribers</returns>
        public TodayNewSubscribersCountResponse GetTodayNewSubscribersCount(string siteSubdomain, string format = ContentFormat.XML)
        {
            return _service.Get<TodayNewSubscribersCountResponse>(string.Format("{0}/TodayNewSubscribersCount/{1}/{2}", _gatewayURL, siteSubdomain, format));
        }

        /// <summary>
        /// Gets the signups evolution for a Site.
        /// </summary>
        /// <param name="siteSubdomain"> The subdomain of the Site for which total signups are evaluated. </param>
        /// <param name="startDate"> Start date from when total signups are evaluated. </param>
        /// <param name="endDate"> End date till when total signups are evaluated. </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <returns> A SignupsEvolutionResponse object containing report data for evolution of Site's total signups. </returns>
        public SignupsEvolutionResponse GetSignupsEvolution(string siteSubdomain, string startDate, string endDate = null, string format = ContentFormat.XML)
        {
            return _service.Get<SignupsEvolutionResponse>(string.Format("{0}/SignupsEvolution/{1}/{2}/{3}?endDate={4}", _gatewayURL, siteSubdomain, startDate, format, endDate));
        }

        /// <summary>
        /// Gets the revenues evolution for a Site.
        /// </summary>
        /// <param name="siteSubdomain"> The subdomain of the Site for which total revenues are evaluated. </param>
        /// <param name="startDate"> Start date from when total revenues are evaluated. </param>
        /// <param name="endDate"> End date till when total revenues are evaluated. </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <returns> A RevenuesEvolutionResponse object containing report data for evolution of Site's total revenues. </returns>
        public RevenuesEvolutionResponse GetRevenuesEvolution(string siteSubdomain, string startDate, string endDate = null, string format = ContentFormat.XML)
        {
            return _service.Get<RevenuesEvolutionResponse>(string.Format("{0}/RevenuesEvolution/{1}/{2}/{3}?endDate={4}", _gatewayURL, siteSubdomain, startDate, format, endDate));
        }

        /// <summary>
        /// Gets the Customers evolution for a Site.
        /// </summary>
        /// <param name="siteSubdomain"> The subdomain of the Site for which Customers evolution is evaluated. </param>
        /// <param name="startDate"> Start date from when Customer's evolution is evaluated. </param>
        /// <param name="endDate"> End date till when Customer's evolution is evaluated. </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <returns> A CustomersEvolutionResponse object containing report data for evolution of Site's Customers. </returns>
        public CustomersEvolutionResponse GetCustomersEvolution(string siteSubdomain, string startDate, string endDate = null, string format = ContentFormat.XML)
        {
            return _service.Get<CustomersEvolutionResponse>(string.Format("{0}/CustomersEvolution/{1}/{2}/{3}?endDate={4}", _gatewayURL, siteSubdomain, startDate, format, endDate));
        }

        /// <summary>
        /// Gets the Subscriptions evolution for a Site.
        /// </summary>
        /// <param name="siteSubdomain"> The subdomain of the Site for which Subscriptions evolution is evaluated. </param>
        /// <param name="startDate"> Start date from when Subscriptions evolution is evaluated. </param>
        /// <param name="endDate"> End date till when Subscriptions evolution is evaluated. </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <returns> A SubscriptionsEvolutionResponse object containing report data for evolution of Site's Subscriptions. </returns>
        public SubscriptionsEvolutionResponse GetSubscriptionsEvolution(string siteSubdomain, string startDate, string endDate = null, string format = ContentFormat.XML)
        {
            return _service.Get<SubscriptionsEvolutionResponse>(string.Format("{0}/SubscriptionsEvolution/{1}/{2}/{3}?endDate={4}", _gatewayURL, siteSubdomain, startDate, format, endDate));
        }

        /// <summary>
        /// Gets the summary information for all Sites from the system.
        /// </summary>
        /// <param name="format">The format used for the data transfer (XML or JSON). </param>
        /// <returns> A SitesSummaryResponse object containing report data for all Site's summary</returns>
        public SitesSummaryResponse GetSitesSummary(string format = ContentFormat.XML)
        {
            return _service.Get<SitesSummaryResponse>(string.Format("{0}/SitesSummary/{1}", _gatewayURL, format));
        }
    }
}
