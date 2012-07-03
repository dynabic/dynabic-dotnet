using DynabicBilling.Classes;
using DynabicPlatform.Classes;
using NUnit.Framework;

namespace DynabicBilling.Tests
{
    public class ProductFamiliesServiceTests : AssertionHelper
    {
        private BillingGateway _gateway = null;
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
        public void GetProductFamilies()
        {
            var site = _testsHelper.AddSite();
            try
            {
                _testsHelper.AddProductFamily(site.Id);
                _testsHelper.AddProductFamily(site.Id);
                _testsHelper.AddProductFamily(site.Id);
                var families = _gateway.ProductFamilies.GetProductFamilies(site.Subdomain);
                Assert.IsNotNull(families);
                Assert.AreEqual(3, families.Count);
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void GetProductFamilyById()
        {
            var site = _testsHelper.AddSite();
            try
            {
                var newFamily = _testsHelper.AddProductFamily(site.Id);
                Assert.IsNotNull(newFamily);

                var family = _gateway.ProductFamilies.GetProductFamilyById(newFamily.Id.ToString());
                Assert.IsNotNull(family);
                Assert.AreEqual(newFamily.Id, family.Id);
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void GetProductFamilyByName()
        {
            var site = _testsHelper.AddSite();
            try
            {
                var newFamily = _testsHelper.AddProductFamily(site.Id);
                Assert.IsNotNull(newFamily);

                var family = _gateway.ProductFamilies.GetProductFamilyByName(site.Subdomain, newFamily.Name);
                Assert.IsNotNull(family);
                Assert.AreEqual(newFamily.Name, family.Name);
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void AddProductFamily()
        {
            var site = _testsHelper.AddSite();
            try
            {
                var newFamily = _testsHelper.AddProductFamily(site.Id);
                Assert.IsNotNull(newFamily);
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void UpdateProductFamily()
        {
            var site = _testsHelper.AddSite();
            try
            {
                var newFamily = _testsHelper.AddProductFamily(site.Id);
                Assert.IsNotNull(newFamily);
                newFamily.Name += "_updated";

                var updatedFamily = _gateway.ProductFamilies.UpdateProductFamily(newFamily, newFamily.Id.ToString());
                Assert.IsNotNull(updatedFamily);
                Assert.AreEqual(newFamily.Name, updatedFamily.Name);
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void DeleteProductFamily()
        {
            var site = _testsHelper.AddSite();
            try
            {
                var newFamily = _testsHelper.AddProductFamily(site.Id);
                Assert.IsNotNull(newFamily);

                var families = _gateway.ProductFamilies.GetProductFamilies(site.Subdomain);
                Assert.IsNotNull(families);
                Assert.AreEqual(1, families.Count);
                Assert.AreEqual(newFamily.Id, families[0].Id);

                _gateway.ProductFamilies.DeleteProductFamily(newFamily.Id.ToString());

                families = _gateway.ProductFamilies.GetProductFamilies(site.Subdomain);
                Assert.IsNotNull(families);
                Assert.AreEqual(0, families.Count);
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void ArchiveProductFamily()
        {
            var site = _testsHelper.AddSite();
            try
            {
                var newFamily = _testsHelper.AddProductFamily(site.Id);
                Assert.IsNotNull(newFamily);

                var families = _gateway.ProductFamilies.GetProductFamilies(site.Subdomain);
                Assert.IsNotNull(families);
                Assert.AreEqual(1, families.Count);
                Assert.False(families[0].isArchived);

                _gateway.ProductFamilies.ArchiveProductFamily(newFamily.Id.ToString());

                families = _gateway.ProductFamilies.GetProductFamilies(site.Subdomain, ContentFormat.XML, "true");
                Assert.IsNotNull(families);
                Assert.AreEqual(1, families.Count);
                Assert.True(families[0].isArchived);
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void ActivateProductFamily()
        {
            var site = _testsHelper.AddSite();
            try
            {
                var newFamily = _testsHelper.AddProductFamily(site.Id);
                Assert.IsNotNull(newFamily);

                var families = _gateway.ProductFamilies.GetProductFamilies(site.Subdomain);
                Assert.IsNotNull(families);
                Assert.AreEqual(1, families.Count);
                Assert.False(families[0].isArchived);

                // Archive product family
                _gateway.ProductFamilies.ArchiveProductFamily(newFamily.Id.ToString());

                families = _gateway.ProductFamilies.GetProductFamilies(site.Subdomain, ContentFormat.XML, "true");
                Assert.IsNotNull(families);
                Assert.AreEqual(1, families.Count);
                Assert.True(families[0].isArchived);

                // Activate product family
                _gateway.ProductFamilies.ActivateProductFamily(newFamily.Id.ToString());

                families = _gateway.ProductFamilies.GetProductFamilies(site.Subdomain);
                Assert.IsNotNull(families);
                Assert.AreEqual(1, families.Count);
                Assert.False(families[0].isArchived);
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }
    }
}
