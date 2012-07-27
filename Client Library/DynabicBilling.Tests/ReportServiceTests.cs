using System;
using DynabicBilling.Classes;
using NUnit.Framework;

namespace DynabicBilling.Tests
{
    public class ReportServiceTests : AssertionHelper
    {
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
            _testData = _testsHelper.PrepareReportsTestData();
        }

        [TearDown]
        public void Cleanup()
        {
            _testsHelper.CleanupTestData();
        }

        [Test]
        public void GetProductsSignupsEvolution()
        {
            var reportData = _gateway.Reports.GetProductsSignupsEvolution(_testData.Subdomain, DateTime.Now.AddDays(-10).ToString(Constants.DATE_FORMAT));
            Assert.IsNotNull(reportData);
        }

        [Test]
        public void GetProductsRevenuesEvolution()
        {
            var reportData = _gateway.Reports.GetProductsRevenuesEvolution(_testData.Subdomain, DateTime.Now.AddDays(-10).ToString(Constants.DATE_FORMAT));
            Assert.IsNotNull(reportData);
        }

        [Test]
        public void GetTotalRevenueAmount()
        {
            var reportData = _gateway.Reports.GetTotalRevenueAmount(_testData.Subdomain);
            Assert.IsNotNull(reportData);
        }

        [Test]
        public void GetActiveSubscriptionsCount()
        {
            var reportData = _gateway.Reports.GetActiveSubscriptionsCount(_testData.Subdomain);
            Assert.IsNotNull(reportData);
        }

        [Test]
        public void GetTotalSubscribersCount()
        {
            var reportData = _gateway.Reports.GetTotalSubscribersCount(_testData.Subdomain);
            Assert.IsNotNull(reportData);
        }

        [Test]
        public void GetTodayRevenueAmount()
        {
            var reportData = _gateway.Reports.GetTodayRevenueAmount(_testData.Subdomain);
            Assert.IsNotNull(reportData);
        }

        [Test]
        public void GetTodayNewSubscribersCount()
        {
            var reportData = _gateway.Reports.GetTodayNewSubscribersCount(_testData.Subdomain);
            Assert.IsNotNull(reportData);
        }

        [Test]
        public void GetSignupsEvolution()
        {
            var reportData = _gateway.Reports.GetSignupsEvolution(_testData.Subdomain, DateTime.Now.AddDays(-10).ToString(Constants.DATE_FORMAT));
            Assert.IsNotNull(reportData);
        }

        [Test]
        public void GetRevenuesEvolution()
        {
            var reportData = _gateway.Reports.GetRevenuesEvolution(_testData.Subdomain, DateTime.Now.AddDays(-10).ToString(Constants.DATE_FORMAT));
            Assert.IsNotNull(reportData);
        }

        [Test]
        public void GetCustomersEvolution()
        {
            var reportData = _gateway.Reports.GetCustomersEvolution(_testData.Subdomain, DateTime.Now.AddDays(-10).ToString(Constants.DATE_FORMAT));
            Assert.IsNotNull(reportData);
        }

        [Test]
        public void GetSubscriptionsEvolution()
        {
            var reportData = _gateway.Reports.GetSubscriptionsEvolution(_testData.Subdomain, DateTime.Now.AddDays(-10).ToString(Constants.DATE_FORMAT));
            Assert.IsNotNull(reportData);
        }

        [Test]
        public void GetSitesSummary()
        {
            var reportData = _gateway.Reports.GetSitesSummary();
            Assert.IsNotNull(reportData);
        }
    }
}
