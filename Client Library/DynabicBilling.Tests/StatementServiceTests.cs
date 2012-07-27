using DynabicBilling.Classes;
using NUnit.Framework;

namespace DynabicBilling.Tests
{
    public class StatementServiceTests : AssertionHelper
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
            _testData = _testsHelper.PrepareStatementsTestData();
        }

        [TearDown]
        public void Cleanup()
        {
            _testsHelper.CleanupTestData();
        }

        [Test]
        public void GetStatementsForSubscription()
        {
            var statements = _gateway.Statements.GetStatementsForSubscription(_testData.SubscriptionId.ToString());
            Assert.IsNotNull(statements);
        }
    }
}
