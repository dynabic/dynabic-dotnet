using System;
using DynabicBilling.Classes;
using DynabicPlatform.Classes;
using NUnit.Framework;

namespace DynabicBilling.Tests
{
    public class EventServiceTests : AssertionHelper
    {
        private const int TIMEOUT_20_MINS = 20 * 60 * 1000;
        private BillingGateway _gateway;
        private TestsHelper _testsHelper;
        private TestDataValues _testData;

        [SetUp]
        public void Init()
        {
#if DEBUG
            _gateway = new BillingGateway(BillingEnvironment.DEVELOPMENT, Constants.PUBLIC_KEY, Constants.PRIVATE_KEY);
#else
            _gateway = new BillingGateway(BillingEnvironment.QA, Constants.PUBLIC_KEY, Constants.PRIVATE_KEY);
#endif
            _testsHelper = new TestsHelper(_gateway);
            _testData = _testsHelper.PrepareEventsTestData();
        }

        [TearDown]
        public void Cleanup()
        {
            _testsHelper.CleanupTestData();
        }

        [Test, Timeout(TIMEOUT_20_MINS)]
        public void GetEventsForSubscription()
        {
            var events = _gateway.Events.GetEventsForSubscription(_testData.SubscriptionId.ToString());
            Assert.IsNotNull(events);
        }

        [Test, Timeout(TIMEOUT_20_MINS)]
        public void GetEventsForSubscriptionBetweenDates()
        {
            var events = _gateway.Events.GetEventsForSubscriptionBetweenDates(_testData.SubscriptionId.ToString(), DateTime.Today.AddDays(-5).ToString(Constants.DATE_FORMAT));
            Assert.IsNotNull(events);
        }

        [Test, Timeout(TIMEOUT_20_MINS)]
        public void GetEvent()
        {
            var events = _gateway.Events.GetEventsForSubscription(_testData.SubscriptionId.ToString());
            Assert.IsNotNull(events);
            Assert.Greater(events.Count, 0);

            var ev = _gateway.Events.GetEvent(events[0].Id.ToString());
            Assert.IsNotNull(ev);
        }

        [Test, Timeout(TIMEOUT_20_MINS)]
        public void GetEventsForSite()
        {
            var events = _gateway.Events.GetEventsForSite(_testData.SiteId.ToString(), pageNumber: "1", pageSize: "100");
            Assert.IsNotNull(events);
        }

        [Test, Timeout(TIMEOUT_20_MINS)]
        public void GetEventsForSiteWithFilter()
        {
            var filter = string.Format("{0}|{1}", DynabicPlatform.RestApiDataContract.EventType.Payment, DynabicPlatform.RestApiDataContract.EventType.ChangeSetting);
            var events = _gateway.Events.GetEventsForSite(_testData.SiteId.ToString(), ContentFormat.XML, filter, "1", "100");
            Assert.IsNotNull(events);
        }

        [Test, Timeout(TIMEOUT_20_MINS)]
        public void GetEvents()
        {
            var filter = string.Format("{0}|{1}", DynabicPlatform.RestApiDataContract.EventType.Payment, DynabicPlatform.RestApiDataContract.EventType.ChangeSetting);
            var events = _gateway.Events.GetEvents(numberOfReturnedEvents: "100");
            Assert.IsNotNull(events);
        }

        [Test, Timeout(TIMEOUT_20_MINS)]
        public void GetEventsWithFilter()
        {
            var filter = string.Format("{0}|{1}", DynabicPlatform.RestApiDataContract.EventType.Payment, DynabicPlatform.RestApiDataContract.EventType.ChangeSetting);
            var events = _gateway.Events.GetEvents(ContentFormat.XML, filter, "100");
            Assert.IsNotNull(events);
        }
    }
}
