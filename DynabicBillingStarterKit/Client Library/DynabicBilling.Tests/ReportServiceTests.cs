using System;
using DynabicBilling.Classes;
using NUnit.Framework;

namespace DynabicBilling.Tests
{
    public class ReportServiceTests : AssertionHelper
    {
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

        [Test]
        public void GetProductsSignupsEvolution()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var reportData = _gateway.Reports.GetProductsSignupsEvolution(site.Subdomain, DateTime.Now.AddDays(-10).ToString(Constants.DATE_FORMAT));
                    Assert.IsNotNull(reportData);
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

        [Test]
        public void GetProductsRevenuesEvolution()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var reportData = _gateway.Reports.GetProductsRevenuesEvolution(site.Subdomain, DateTime.Now.AddDays(-10).ToString(Constants.DATE_FORMAT));
                    Assert.IsNotNull(reportData);
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

        [Test]
        public void GetTotalRevenueAmount()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var reportData = _gateway.Reports.GetTotalRevenueAmount(site.Subdomain);
                    Assert.IsNotNull(reportData);
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

        [Test]
        public void GetActiveSubscriptionsCount()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var reportData = _gateway.Reports.GetActiveSubscriptionsCount(site.Subdomain);
                    Assert.IsNotNull(reportData);
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

        [Test]
        public void GetTotalSubscribersCount()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var reportData = _gateway.Reports.GetTotalSubscribersCount(site.Subdomain);
                    Assert.IsNotNull(reportData);
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

        [Test]
        public void GetTodayRevenueAmount()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var reportData = _gateway.Reports.GetTodayRevenueAmount(site.Subdomain);
                    Assert.IsNotNull(reportData);
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

        [Test]
        public void GetTodayNewSubscribersCount()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var reportData = _gateway.Reports.GetTodayNewSubscribersCount(site.Subdomain);
                    Assert.IsNotNull(reportData);
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

        [Test]
        public void GetSignupsEvolution()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var reportData = _gateway.Reports.GetSignupsEvolution(site.Subdomain, DateTime.Now.AddDays(-10).ToString(Constants.DATE_FORMAT));
                    Assert.IsNotNull(reportData);
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

        [Test]
        public void GetRevenuesEvolution()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var reportData = _gateway.Reports.GetRevenuesEvolution(site.Subdomain, DateTime.Now.AddDays(-10).ToString(Constants.DATE_FORMAT));
                    Assert.IsNotNull(reportData);
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

        [Test]
        public void GetCustomersEvolution()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var reportData = _gateway.Reports.GetCustomersEvolution(site.Subdomain, DateTime.Now.AddDays(-10).ToString(Constants.DATE_FORMAT));
                    Assert.IsNotNull(reportData);
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

        [Test]
        public void GetSubscriptionsEvolution()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var reportData = _gateway.Reports.GetSubscriptionsEvolution(site.Subdomain, DateTime.Now.AddDays(-10).ToString(Constants.DATE_FORMAT));
                    Assert.IsNotNull(reportData);
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

        [Test]
        public void GetSitesSummary()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var reportData = _gateway.Reports.GetSitesSummary();
                    Assert.IsNotNull(reportData);
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
    }
}
