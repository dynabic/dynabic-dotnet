using DynabicPlatform.RestApiDataContract;

namespace DynabicPlatform.RestAPI.RestInterfaces
{
    public interface IEventsService
    {
        /// <summary>
        /// Gets all Events that correspond to a specific Subscription
        /// </summary>
        /// <param name="subscriptionId"> Subscription's unique identifier </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON). </param>
        /// <param name="pageNumber">
        /// Optional parameter to be used when a paged response is expected.
        /// Use it to specify the index of the page to be retrieved (this value has to be greater than or equal to 1).
        /// </param>
        /// <param name="pageSize">
        /// Optional parameter to be used when a paged response is expected.
        /// Use it to specify the number of records a page should contain (this value has to be greater than or equal to 1).
        /// </param>
        /// <returns> An EventList object containing all Event objects that correspond to the specified Subscription </returns>
        EventsList GetEventsForSubscription(string subscriptionId, string format = "xml", string pageNumber = null, string pageSize = null);

        /// <summary>
        /// Returns a list with the latest events for a subscription between the given dates.
        /// </summary>
        /// <param name="subscriptionId">The subscription id.</param>
        /// <param name="fromDate">The start date.</param>
        /// <param name="toDate">The end date. If the end date is not specified the current date will be used.</param>
        /// <param name="format">The format.</param>
        EventsList GetEventsForSubscriptionBetweenDates(string subscriptionId, string fromDate, string toDate = null, string format = "xml");

        /// <summary>
        /// Gets the event.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="format">The format used for the data transfer (XML or JSON).</param>
        EventResponse GetEvent(string eventId, string format = "xml");

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
        EventsList GetEventsForSite(string siteId, string format = "xml", string eventTypeFilter = null, string pageNumber = null, string pageSize = null);

        /// <summary>
        /// Returns a list with the latest events.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="eventTypeFilter">The event type filter.</param>
        /// <param name="numberOfReturnedEvents">The number of returned events.</param>
        EventsList GetEvents(string format = "xml", string eventTypeFilter = null, string numberOfReturnedEvents = null);
    }
}
