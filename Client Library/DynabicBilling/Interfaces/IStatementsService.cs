using DynabicBilling.RestApiDataContract;

namespace DynabicBilling.RestAPI.RestInterfaces
{
    public interface IStatementsService
    {
        /// <summary>
        /// Gets all Statements for a Subscription
        /// </summary>
        /// <param name="subscriptionId"> Id of the Subscription for which Statements were issued </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON)</param>
        /// <param name="pageNumber">
        /// Optional parameter to be used when a paged response is expected.
        /// Use it to specify the index of the page to be retrieved (this value has to be greater than or equal to 1).
        /// </param>
        /// <param name="pageSize">
        /// Optional parameter to be used when a paged response is expected.
        /// Use it to specify the number of records a page should contain (this value has to be greater than or equal to 1).
        /// </param>
        /// <returns> A StatementsList object containing all Statements that correspond to the specified Subscription </returns>
        StatementsList GetStatementsForSubscription(string subscriptionId, string format = "xml", string pageNumber = null, string pageSize = null);
    }
}
