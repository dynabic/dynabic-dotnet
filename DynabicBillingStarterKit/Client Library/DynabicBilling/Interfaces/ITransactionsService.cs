using DynabicBilling.RestApiDataContract;

namespace DynabicBilling.RestAPI.RestInterfaces
{
    public interface ITransactionsService
    {
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
        TransactionsList GetTransactionsForCustomer(string customerId, string format = "xml", string pageNumber = null, string pageSize = null);

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
        TransactionsList GetTransactionsForSite(string siteSubdomain, string format = "xml", string pageNumber = null, string pageSize = null);

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
        TransactionsList GetTransactionsForSubscription(string subscriptionId, string format = "xml", string pageNumber = null, string pageSize = null);
    }
}
