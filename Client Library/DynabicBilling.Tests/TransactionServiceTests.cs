using DynabicBilling.Classes;
using NUnit.Framework;

namespace DynabicBilling.Tests
{
    public class TransactionServiceTests : AssertionHelper
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
            _testData = _testsHelper.PrepareTransactionsTestData();
        }

        [TearDown]
        public void Cleanup()
        {
            _testsHelper.CleanupTestData();
        }

        [Test]
        public void GetTransactionsForCustomer()
        {
            var transactions = _gateway.Transaction.GetTransactionsForCustomer(_testData.CustomerId.ToString(), pageNumber: "1", pageSize: "10");
            Assert.IsNotNull(transactions);
        }

        [Test]
        public void GetTransactionsForSite()
        {
            var transactions = _gateway.Transaction.GetTransactionsForSite(_testData.Subdomain, pageNumber: "1", pageSize: "10");
            Assert.IsNotNull(transactions);
        }

        [Test]
        public void GetTransactionsForSubscription()
        {
            var transactions = _gateway.Transaction.GetTransactionsForSubscription(_testData.SubscriptionId.ToString());
            Assert.IsNotNull(transactions);
        }
    }
}
