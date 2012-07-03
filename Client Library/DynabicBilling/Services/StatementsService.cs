﻿#pragma warning disable 1591

using DynabicBilling.RestAPI.RestInterfaces;
using DynabicBilling.RestApiDataContract;
using DynabicPlatform.Classes;

namespace DynabicBilling
{
    /// <summary>
    /// Provides methods for retrieving statements
    /// </summary>
    public class StatementsService : IStatementsService
    {
        private CommunicationLayer _service;
        private readonly string _gatewayURL;

        protected internal StatementsService(CommunicationLayer service)
        {
            _service = service;
            _gatewayURL = service.Environment.GatewayURL + "/statements";
        }

        #region GET

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
        public StatementsList GetStatementsForSubscription(string subscriptionId, string format = ContentFormat.XML, string pageNumber = null, string pageSize = null)
        {
            return _service.Get<StatementsList>(string.Format("{0}/subscription/{1}.{2}?pageNumber={3}&pageSize={4}", _gatewayURL, subscriptionId, format, pageNumber, pageSize));
        }

        #endregion GET
    }
}
