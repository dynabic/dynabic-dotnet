using DynabicBilling.Classes;
using NUnit.Framework;

namespace DynabicBilling.Tests
{
    public class TransactionServiceTests : AssertionHelper
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
        public void GetTransactionsForCustomer()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var transactions = _gateway.Transaction.GetTransactionsForCustomer(subscription.CustomerId.ToString());
                    Assert.IsNotNull(transactions);
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
        public void GetTransactionsForSite()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var transactions = _gateway.Transaction.GetTransactionsForSite(site.Subdomain);
                    Assert.IsNotNull(transactions);
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
        public void GetTransactionsForSubscription()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var transactions = _gateway.Transaction.GetTransactionsForSubscription(subscription.Id.ToString());
                    Assert.IsNotNull(transactions);
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
