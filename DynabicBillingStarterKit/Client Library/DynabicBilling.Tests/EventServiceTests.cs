using System;
using DynabicBilling.Classes;
using DynabicPlatform.Classes;
using NUnit.Framework;

namespace DynabicBilling.Tests
{
    public class EventServiceTests : AssertionHelper
    {
        private const int TIMEOUT_10_MINS = 20 * 60 * 1000;
        private BillingGateway _gateway;
        private TestsHelper _testsHelper;

        [SetUp]
        public void Init()
        {
#if DEBUG
            _gateway = new BillingGateway(BillingEnvironment.DEVELOPMENT, Constants.PUBLIC_KEY, Constants.PRIVATE_KEY);
#else
            _gateway = new BillingGateway(BillingEnvironment.QA, Constants.PUBLIC_KEY, Constants.PRIVATE_KEY);
#endif
            _testsHelper = new TestsHelper(_gateway);
        }

        [Test, Timeout(TIMEOUT_10_MINS)]
        public void GetEventsForSubscription()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var events = _gateway.Events.GetEventsForSubscription(subscription.Id.ToString());
                    Assert.IsNotNull(events);
                }
                finally
                {
                    _testsHelper.DeleteSubscriptionData(subscription);
                }
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test, Timeout(TIMEOUT_10_MINS)]
        public void GetEventsForSubscriptionBetweenDates()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var events = _gateway.Events.GetEventsForSubscriptionBetweenDates(subscription.Id.ToString(), DateTime.Today.AddDays(-5).ToString(Constants.DATE_FORMAT));
                    Assert.IsNotNull(events);
                }
                finally
                {
                    _testsHelper.DeleteSubscriptionData(subscription);
                }
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test, Timeout(TIMEOUT_10_MINS)]
        public void GetEvent()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var events = _gateway.Events.GetEventsForSubscription(subscription.Id.ToString());
                    Assert.IsNotNull(events);
                    Assert.Greater(events.Count, 0);

                    var ev = _gateway.Events.GetEvent(events[0].Id.ToString());
                    Assert.IsNotNull(ev);
                }
                finally
                {
                    _testsHelper.DeleteSubscriptionData(subscription);
                }
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test, Timeout(TIMEOUT_10_MINS)]
        public void GetEventsForSite()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var events = _gateway.Events.GetEventsForSite(site.Id.ToString(), pageNumber: "1", pageSize: "100");
                Assert.IsNotNull(events);
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test, Timeout(TIMEOUT_10_MINS)]
        public void GetEventsForSiteWithFilter()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var filter = string.Format("{0}|{1}", DynabicPlatform.RestApiDataContract.EventType.Payment, DynabicPlatform.RestApiDataContract.EventType.ChangeSetting);
                var events = _gateway.Events.GetEventsForSite(site.Id.ToString(), ContentFormat.XML, filter, "1", "100");
                Assert.IsNotNull(events);
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test, Timeout(TIMEOUT_10_MINS)]
        public void GetEvents()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var filter = string.Format("{0}|{1}", DynabicPlatform.RestApiDataContract.EventType.Payment, DynabicPlatform.RestApiDataContract.EventType.ChangeSetting);
                var events = _gateway.Events.GetEvents(numberOfReturnedEvents: "100");
                Assert.IsNotNull(events);
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test, Timeout(TIMEOUT_10_MINS)]
        public void GetEventsWithFilter()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var filter = string.Format("{0}|{1}", DynabicPlatform.RestApiDataContract.EventType.Payment, DynabicPlatform.RestApiDataContract.EventType.ChangeSetting);
                var events = _gateway.Events.GetEvents(ContentFormat.XML, filter, "100");
                Assert.IsNotNull(events);
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }
    }
}
