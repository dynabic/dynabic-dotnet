using DynabicBilling.Classes;
using DynabicPlatform.Classes;
using NUnit.Framework;

namespace DynabicBilling.Tests
{
    public class ProductFamiliesServiceTests : AssertionHelper
    {
        private BillingGateway _gateway = null;
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
            _testData = _testsHelper.PrepareProductFamiliesTestData();
        }

        [TearDown]
        public void Cleanup()
        {
            _testsHelper.CleanupTestData();
        }

        [Test]
        public void GetProductFamilies()
        {
            var families = _gateway.ProductFamilies.GetProductFamilies(_testData.Subdomain);
            Assert.IsNotNull(families);
            Assert.AreEqual(4, families.Count);
        }

        [Test]
        public void GetProductFamilyById()
        {
            var family = _gateway.ProductFamilies.GetProductFamilyById(_testData.ProductFamilyId.ToString());
            Assert.IsNotNull(family);
            Assert.AreEqual(_testData.ProductFamilyId, family.Id);
        }

        [Test]
        public void GetProductFamilyByName()
        {
            var family = _gateway.ProductFamilies.GetProductFamilyByName(_testData.Subdomain, _testData.ProductFamilyName);
            Assert.IsNotNull(family);
            Assert.AreEqual(_testData.ProductFamilyName, family.Name);
        }

        [Test]
        public void AddProductFamily()
        {
            var newFamily = _testsHelper.AddProductFamily(_testData.SiteId);
            Assert.IsNotNull(newFamily);
            _gateway.ProductFamilies.DeleteProductFamily(newFamily.Id.ToString());
        }

        [Test]
        public void UpdateProductFamily()
        {
            var newFamily = _gateway.ProductFamilies.GetProductFamilyById(_testData.ProductFamilyId.ToString());
            Assert.IsNotNull(newFamily);

            newFamily.Name += "_updated";

            var updatedFamily = _gateway.ProductFamilies.UpdateProductFamily(newFamily, newFamily.Id.ToString());
            Assert.IsNotNull(updatedFamily);
            Assert.AreEqual(newFamily.Name, updatedFamily.Name);
        }

        [Test]
        public void DeleteProductFamily()
        {
            var newFamily = _testsHelper.AddProductFamily(_testData.SiteId);
            Assert.IsNotNull(newFamily);
            _gateway.ProductFamilies.DeleteProductFamily(newFamily.Id.ToString());
        }

        [Test]
        public void ArchiveProductFamily()
        {
            var family = _gateway.ProductFamilies.GetProductFamilyById(_testData.ProductFamilyId.ToString());
            Assert.IsNotNull(family);
            Assert.False(family.isArchived);

            _gateway.ProductFamilies.ArchiveProductFamily(family.Id.ToString());

            family = _gateway.ProductFamilies.GetProductFamilyById(_testData.ProductFamilyId.ToString());
            Assert.IsNotNull(family);
            Assert.True(family.isArchived);
        }

        [Test]
        public void ActivateProductFamily()
        {
            var family = _gateway.ProductFamilies.GetProductFamilyById(_testData.ProductFamilyId.ToString());
            Assert.IsNotNull(family);

            // Archive product family
            _gateway.ProductFamilies.ArchiveProductFamily(family.Id.ToString());

            family = _gateway.ProductFamilies.GetProductFamilyById(_testData.ProductFamilyId.ToString());
            Assert.IsNotNull(family);
            Assert.True(family.isArchived);

            // Activate product family
            _gateway.ProductFamilies.ActivateProductFamily(family.Id.ToString());

            family = _gateway.ProductFamilies.GetProductFamilyById(_testData.ProductFamilyId.ToString());
            Assert.IsNotNull(family);
            Assert.False(family.isArchived);
        }
    }
}
