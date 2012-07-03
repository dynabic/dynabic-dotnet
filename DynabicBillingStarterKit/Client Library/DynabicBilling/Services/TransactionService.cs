#pragma warning disable 1591

using DynabicBilling.RestAPI.RestInterfaces;
using DynabicBilling.RestApiDataContract;
using DynabicPlatform.Classes;

namespace DynabicBilling
{
    /// <summary>
    /// Provides operations for transactions
    /// </summary>
    public class TransactionService : ITransactionsService
    {
        private CommunicationLayer _service;
        private readonly string _gatewayURL;

        protected internal TransactionService(CommunicationLayer service)
        {
            _service = service;
            _gatewayURL = service.Environment.GatewayURL + "/transactions";
        }

        #region GET

        /// <summary>
        /// Gets all Transactions for a specific Customer
        /// </summary>
        /// <param name="customerId"> Id of the Customer for which all transactions were issued. </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <param name="pageNumber">
        /// Optional parameter to be used when a paged response is expected.
        /// Use it to specify the index of the page to be retrieved (this value has to be greater than or equal to 1).
        /// </param>
        /// <param name="pageSize">
        /// Optional parameter to be used when a paged response is expected.
        /// Use it to specify the number of records a page should contain (this value has to be greater than or equal to 1).
        /// </param>
        /// <returns> A TTransactionsList object containing all Transactions for the specified Customer </returns>
        public TransactionsList GetTransactionsForCustomer(string customerId, string format = ContentFormat.XML, string pageNumber = null, string pageSize = null)
        {
            return _service.Get<TransactionsList>(string.Format("{0}/customer/{1}.{2}?pageNumber={3}&pageSize={4}", _gatewayURL, customerId, format, pageNumber, pageSize));
        }

        /// <summary>
        /// Gets all Transactions for a specific Site
        /// </summary>
        /// <param name="siteSubdomain"> The subdomain of the Site to which the Transactions were made. </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <param name="pageNumber">
        /// Optional parameter to be used when a paged response is expected.
        /// Use it to specify the index of the page to be retrieved (this value has to be greater than or equal to 1).
        /// </param>
        /// <param name="pageSize">
        /// Optional parameter to be used when a paged response is expected.
        /// Use it to specify the number of records a page should contain (this value has to be greater than or equal to 1).
        /// </param>
        /// <returns> A TransactionsList object containing all Transactions for the specified Site </returns>
        public TransactionsList GetTransactionsForSite(string siteSubdomain, string format = ContentFormat.XML, string pageNumber = null, string pageSize = null)
        {
            return _service.Get<TransactionsList>(string.Format("{0}/{1}.{2}?pageNumber={3}&pageSize={4}", _gatewayURL, siteSubdomain, format, pageNumber, pageSize));
        }

        /// <summary>
        /// Gets all Transactions for a specific Subscription
        /// </summary>
        /// <param name="subscriptionId"> Id of the Subscription for which all Transactions were made. </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <param name="pageNumber">
        /// Optional parameter to be used when a paged response is expected.
        /// Use it to specify the index of the page to be retrieved (this value has to be greater than or equal to 1).
        /// </param>
        /// <param name="pageSize">
        /// Optional parameter to be used when a paged response is expected.
        /// Use it to specify the number of records a page should contain (this value has to be greater than or equal to 1).
        /// </param>
        /// <returns> A TransactionsList object containing all Transactions for the specified Subscription </returns>
        public TransactionsList GetTransactionsForSubscription(string subscriptionId, string format = ContentFormat.XML, string pageNumber = "", string pageSize = "")
        {
            return _service.Get<TransactionsList>(string.Format("{0}/subscription/{1}.{2}?pageNumber={3}&pageSize={4}", _gatewayURL, subscriptionId, format, pageNumber, pageSize));
        }

        #endregion GET
    }
}
