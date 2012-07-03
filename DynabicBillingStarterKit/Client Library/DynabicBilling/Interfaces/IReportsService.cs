using DynabicBilling.RestApiDataContract;

namespace DynabicBilling.RestAPI.RestInterfaces
{
    public interface IReportsService
    {
        #region GET

        /// <summary>
        /// Gets the source data from OLAP cube for the "Products Signups" report.
        /// </summary>
        /// <param name="siteSubdomain"> The subdomain of the Site to which the Products belong to. </param>
        /// <param name="startDate"> Start date from when Product's signups are evaluated. </param>
        /// <param name="endDate"> End date till when Product's signups are evaluated. </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <returns> A ProductsSignupsEvolutionResponse object containing report data for evolution of Product's signups. </returns>
        ProductsSignupsEvolutionResponse GetProductsSignupsEvolution(string siteSubdomain, string startDate, string endDate = null, string format = "xml");

        /// <summary>
        /// Gets the source data from OLAP cube for the "Products Revenues" report.
        /// </summary>
        /// <param name="siteSubdomain"> The subdomain of the Site to which the Products belong to. </param>
        /// <param name="startDate"> Start date from when Product's revenues are evaluated. </param>
        /// <param name="endDate"> End date till when Product's revenues are evaluated. </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <returns> A ProductsRevenuesEvolutionResponse object containing report data for evolution of Product's revenues. </returns>
        ProductsRevenuesEvolutionResponse GetProductsRevenuesEvolution(string siteSubdomain, string startDate, string endDate = null, string format = "xml");

        /// <summary>
        /// Gets the total revenue amount for a Site
        /// </summary>
        /// <param name="siteSubdomain"> The subdomain of the Site for wich the total revenue is evaluated. </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <returns>A TotalRevenueAmountResponse object containing report data for Site's total revenues. </returns>
        TotalRevenueAmountResponse GetTotalRevenueAmount(string siteSubdomain, string format = "xml");

        /// <summary>
        /// Gets the count of total active Subscriptions from a Site.
        /// </summary>
        /// <param name="siteSubdomain"> The subdomain of the Site for wich count of active Subscriptions is evaluated. </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <returns> A ActiveSubscriptionsCountResponse object containing report data about Site's active Subscriptions. </returns>
        ActiveSubscriptionsCountResponse GetActiveSubscriptionsCount(string siteSubdomain, string format = "xml");

        /// <summary>
        /// Gets the total count of subscribers for a Site
        /// </summary>
        /// <param name="siteSubdomain"> The subdomain of the Site for wich count of subscribers is evaluated. </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <returns> A TotalSubscribersCountResponse object containing report data about Site's total subscribres count. </returns>
        TotalSubscribersCountResponse GetTotalSubscribersCount(string siteSubdomain, string format = "xml");

        /// <summary>
        /// Gets the amount of today's revenues.
        /// </summary>
        /// <param name="siteSubdomain"> The subdomain of the Site for wich today's amount is evaluated. </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <returns> A TodayRevenueAmountResponse object containing report data about Site's today's revenues. </returns>
        TodayRevenueAmountResponse GetTodayRevenueAmount(string siteSubdomain, string format = "xml");

        /// <summary>
        /// Gets the count of today's new subscribers.
        /// </summary>
        /// <param name="siteSubdomain"> The subdomain of the Site for wich today's new subscribers is evaluated. </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <returns> A TodayNewSubscribersCountResponse object containing report data for Site's today's new subscribers</returns>
        TodayNewSubscribersCountResponse GetTodayNewSubscribersCount(string siteSubdomain, string format = "xml");

        /// <summary>
        /// Gets the signups evolution for a Site.
        /// </summary>
        /// <param name="siteSubdomain"> The subdomain of the Site for which total signups are evaluated. </param>
        /// <param name="startDate"> Start date from when total signups are evaluated. </param>
        /// <param name="endDate"> End date till when total signups are evaluated. </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <returns> A SignupsEvolutionResponse object containing report data for evolution of Site's total signups. </returns>
        SignupsEvolutionResponse GetSignupsEvolution(string siteSubdomain, string startDate, string endDate = null, string format = "xml");

        /// <summary>
        /// Gets the revenues evolution for a Site.
        /// </summary>
        /// <param name="siteSubdomain"> The subdomain of the Site for which total revenues are evaluated. </param>
        /// <param name="startDate"> Start date from when total revenues are evaluated. </param>
        /// <param name="endDate"> End date till when total revenues are evaluated. </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <returns> A RevenuesEvolutionResponse object containing report data for evolution of Site's total revenues. </returns>
        RevenuesEvolutionResponse GetRevenuesEvolution(string siteSubdomain, string startDate, string endDate = null, string format = "xml");

        /// <summary>
        /// Gets the Customers evolution for a Site.
        /// </summary>
        /// <param name="siteSubdomain"> The subdomain of the Site for which Customers evolution is evaluated. </param>
        /// <param name="startDate"> Start date from when Customer's evolution is evaluated. </param>
        /// <param name="endDate"> End date till when Customer's evolution is evaluated. </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <returns> A CustomersEvolutionResponse object containing report data for evolution of Site's Customers. </returns>
        CustomersEvolutionResponse GetCustomersEvolution(string siteSubdomain, string startDate, string endDate = null, string format = "xml");

        /// <summary>
        /// Gets the Subscriptions evolution for a Site.
        /// </summary>
        /// <param name="siteSubdomain"> The subdomain of the Site for which Subscriptions evolution is evaluated. </param>
        /// <param name="startDate"> Start date from when Subscriptions evolution is evaluated. </param>
        /// <param name="endDate"> End date till when Subscriptions evolution is evaluated. </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <returns> A SubscriptionsEvolutionResponse object containing report data for evolution of Site's Subscriptions. </returns>
        SubscriptionsEvolutionResponse GetSubscriptionsEvolution(string siteSubdomain, string startDate, string endDate = null, string format = "xml");

        /// <summary>
        /// Gets the summary information for all Sites from the system.
        /// </summary>
        /// <param name="format">The format used for the data transfer (XML or JSON). </param>
        /// <returns> A SitesSummaryResponse object containing report data for all Site's summary</returns>        
        SitesSummaryResponse GetSitesSummary(string format = "xml");

        #endregion GET
    }
}
