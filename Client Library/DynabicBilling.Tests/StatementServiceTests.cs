using DynabicBilling.Classes;
using NUnit.Framework;

namespace DynabicBilling.Tests
{
    public class StatementServiceTests : AssertionHelper
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
        public void GetStatementsForSubscription()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var statements = _gateway.Statements.GetStatementsForSubscription(subscription.Id.ToString());
                    Assert.IsNotNull(statements);
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
