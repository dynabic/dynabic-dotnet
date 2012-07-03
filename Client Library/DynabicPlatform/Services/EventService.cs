using DynabicPlatform.Classes;
using DynabicPlatform.RestAPI.RestInterfaces;
using DynabicPlatform.RestApiDataContract;

namespace DynabicPlatform.Services
{
    public class EventService : IEventsService
    {
        private readonly CommunicationLayer _service;
        private readonly string _gatewayURL;

        public EventService(CommunicationLayer service)
        {
            _service = service;
            _gatewayURL = service.Environment.GatewayURL + "/events";
        }

        /// <summary>
        /// Gets all Events that correspond to a specific Subscription
        /// </summary>
        /// <param name="subscriptionId">Subscription's unique identifier</param>
        /// <param name="format">The format used for the data transfer (XML or JSON).</param>
        /// <param name="pageNumber">Optional parameter to be used when a paged response is expected.
        /// Use it to specify the index of the page to be retrieved (this value has to be greater than or equal to 1).</param>
        /// <param name="pageSize">Optional parameter to be used when a paged response is expected.
        /// Use it to specify the number of records a page should contain (this value has to be greater than or equal to 1).</param>
        /// <returns>
        /// An EventList object containing all Event objects that correspond to the specified Subscription
        /// </returns>
        public EventsList GetEventsForSubscription(string subscriptionId, string format = "xml", string pageNumber = null, string pageSize = null)
        {
            return _service.Get<EventsList>(string.Format("{0}/subscription/{1}.{2}?pageNumber={3}&pageSize={4}", _gatewayURL, subscriptionId, format, pageNumber, pageSize));
        }

        /// <summary>
        /// Returns a list with the latest events for a subscription between the given dates.
        /// </summary>
        /// <param name="subscriptionId">The subscription id.</param>
        /// <param name="fromDate">The start date.</param>
        /// <param name="toDate">The end date. If the end date is not specified the current date will be used.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        public EventsList GetEventsForSubscriptionBetweenDates(string subscriptionId, string fromDate, string toDate = null, string format = "xml")
        {
            return _service.Get<EventsList>(string.Format("{0}/subscription/{1}/{2}/{3}?toDate={4}", _gatewayURL, subscriptionId, fromDate, format, toDate));
        }

        /// <summary>
        /// Gets the event.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="format">The format used for the data transfer (XML or JSON).</param>
        /// <returns></returns>
        public EventResponse GetEvent(string eventId, string format = "xml")
        {
            return _service.Get<EventResponse>(string.Format("{0}/{1}.{2}", _gatewayURL, eventId, format));
        }

        /// <summary>
        /// Gets all Events that correspond to a specific Site
        /// </summary>
        /// <param name="siteId">Site's unique identifier</param>
        /// <param name="format">The format used for the data transfer (XML or JSON).</param>
        /// <param name="eventTypeFilter">The event type filter.</param>
        /// <param name="pageNumber">Optional parameter to be used when a paged response is expected.
        /// Use it to specify the index of the page to be retrieved (this value has to be greater than or equal to 1).</param>
        /// <param name="pageSize">Optional parameter to be used when a paged response is expected.
        /// Use it to specify the number of records a page should contain (this value has to be greater than or equal to 1).</param>
        /// <returns>
        /// An EventList object containing all Event objects that correspond to the specified Site
        /// </returns>
        public EventsList GetEventsForSite(string siteId, string format = "xml", string eventTypeFilter = null, string pageNumber = null, string pageSize = null)
        {
            return _service.Get<EventsList>(string.Format("{0}/site/{1}.{2}?eventTypeFilter={3}&pageNumber={4}&pageSize={5}", _gatewayURL, siteId, format, eventTypeFilter, pageNumber, pageSize));
        }

        /// <summary>
        /// Returns a list with the latest events.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="eventTypeFilter">The event type filter.</param>
        /// <param name="numberOfReturnedEvents">The number of returned events.</param>
        /// <returns></returns>
        public EventsList GetEvents(string format = "xml", string eventTypeFilter = null, string numberOfReturnedEvents = null)
        {
            return _service.Get<EventsList>(string.Format("{0}/{1}?eventTypeFilter={2}&numberOfReturnedEvents={3}", _gatewayURL, format, eventTypeFilter, numberOfReturnedEvents));
        }
    }
}
