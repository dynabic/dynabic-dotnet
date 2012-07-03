using DynabicBilling.Classes;
using NUnit.Framework;

namespace DynabicBilling.Tests
{
    public class ProductServiceTests : AssertionHelper
    {
        private BillingGateway _gateway = null;
        private TestsHelper _testsHelper;

        [SetUp]
        public void Init()
        {
#if DEBUG
            _gateway = new BillingGateway(BillingEnvironment.DEVELOPMENT, Constants.PUBLIC_KEY, Constants.PRIVATE_KEY);
            //_gateway = new BillingGateway(BillingEnvironment.PRODUCTION, "f71bd4b0adda428587bc", "412f6d6f60d44b568018");
#else
            _gateway = new BillingGateway(BillingEnvironment.QA, Constants.PUBLIC_KEY, Constants.PRIVATE_KEY);
#endif
            _testsHelper = new TestsHelper(_gateway);
        }

        #region Helpers

        private class TestDataValues
        {
            public int SiteId { get; set; }
            public string Subdomain { get; set; }
            public string ProductFamilyName { get; set; }
            public int ProductFamilyId { get; set; }
            public string ProductName { get; set; }
            public int ProductId { get; set; }
            public string ApiRef { get; set; }
        }

        private TestDataValues PrepareTestData()
        {
            var testData = new TestDataValues();

            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            testData.SiteId = site.Id;
            testData.Subdomain = site.Subdomain;

            var family = _testsHelper.AddProductFamily(site.Id);
            Assert.IsNotNull(family);
            testData.ProductFamilyId = family.Id;
            testData.ProductFamilyName = family.Name;

            var product = _testsHelper.AddProductToFamily(family.Id);
            Assert.IsNotNull(product);
            testData.ProductId = product.Id;
            testData.ProductName = product.Name;
            testData.ApiRef = product.ApiRef1;
            return testData;
        }

        #endregion Helpers

        //[Test]
        public void __GetProductsBySite()
        {
            var products = _gateway.Products.GetProductsBySite("codeporting");
            Assert.IsNotNull(products);
        }

        [Test]
        public void GetProductsBySite()
        {
            var testData = PrepareTestData();
            try
            {
                var products = _gateway.Products.GetProductsBySite(testData.Subdomain);
                Assert.IsNotNull(products);
                Assert.AreEqual(1, products.Count);
            }
            finally
            {
                _testsHelper.DeleteSite(testData.SiteId);
            }
        }

        [Test]
        public void GetProductsBySiteAndFamily()
        {
            var testData = PrepareTestData();
            try
            {
                var products = _gateway.Products.GetProductsBySiteAndFamily(testData.Subdomain, testData.ProductFamilyName);
                Assert.IsNotNull(products);
                Assert.AreEqual(1, products.Count);
            }
            finally
            {
                _testsHelper.DeleteSite(testData.SiteId);
            }
        }

        [Test]
        public void GetProductsBySiteAndProductName()
        {
            var testData = PrepareTestData();
            try
            {
                var products = _gateway.Products.GetProductsBySiteAndProductName(testData.Subdomain, testData.ProductName);
                Assert.IsNotNull(products);
                Assert.AreEqual(1, products.Count);
            }
            finally
            {
                _testsHelper.DeleteSite(testData.SiteId);
            }
        }

        [Test]
        public void GetProductByApiRef()
        {
            var testData = PrepareTestData();
            try
            {
                var product = _gateway.Products.GetProductByApiRef(testData.SiteId.ToString(), testData.ApiRef);
                Assert.IsNotNull(product);
            }
            finally
            {
                _testsHelper.DeleteSite(testData.SiteId);
            }
        }

        [Test]
        public void GetProductsByFamilyId()
        {
            var testData = PrepareTestData();
            try
            {
                var products = _gateway.Products.GetProductsByFamilyId(testData.ProductFamilyId.ToString());
                Assert.IsNotNull(products);
                Assert.AreEqual(1, products.Count);
            }
            finally
            {
                _testsHelper.DeleteSite(testData.SiteId);
            }
        }

        [Test]
        public void GetProductByFamilyIdAndProductName()
        {
            var testData = PrepareTestData();
            try
            {
                var product = _gateway.Products.GetProductByFamilyIdAndProductName(testData.ProductFamilyId.ToString(), testData.ProductName);
                Assert.IsNotNull(product);
            }
            finally
            {
                _testsHelper.DeleteSite(testData.SiteId);
            }
        }

        [Test]
        public void GetProductById()
        {
            var testData = PrepareTestData();
            try
            {
                var product = _gateway.Products.GetProductById(testData.ProductId.ToString());
                Assert.IsNotNull(product);
            }
            finally
            {
                _testsHelper.DeleteSite(testData.SiteId);
            }
        }

        [Test]
        public void AddProduct()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var product = _testsHelper.AddProduct(site.Id);
                Assert.IsNotNull(product);
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void UpdateProduct()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var product = _testsHelper.AddProduct(site.Id);
                Assert.IsNotNull(product);
                product.ApiRef1 += "_updated";

                var updatedProduct = _gateway.Products.UpdateProduct(product, product.Id.ToString());
                Assert.IsNotNull(updatedProduct);
                Assert.AreEqual(product.ApiRef1, updatedProduct.ApiRef1);
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void DeleteProduct()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var product = _testsHelper.AddProduct(site.Id);
                Assert.IsNotNull(product);
                _gateway.Products.DeleteProduct(product.Id.ToString());
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void ArchiveProduct()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var product = _testsHelper.AddProduct(site.Id);
                Assert.IsNotNull(product);
                Assert.False(product.isArchived);

                _gateway.Products.ArchiveProduct(product.Id.ToString());

                var archivedProduct = _gateway.Products.GetProductById(product.Id.ToString());
                Assert.True(archivedProduct.isArchived);
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void ActivateProduct()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var product = _testsHelper.AddProduct(site.Id);
                Assert.IsNotNull(product);
                Assert.False(product.isArchived);

                // Archive product family
                _gateway.Products.ArchiveProduct(product.Id.ToString());
                var archivedProduct = _gateway.Products.GetProductById(product.Id.ToString());
                Assert.True(archivedProduct.isArchived);

                // Activate product family
                _gateway.Products.ActivateProduct(product.Id.ToString());
                var activatedProduct = _gateway.Products.GetProductById(product.Id.ToString());
                Assert.False(activatedProduct.isArchived);
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }
    }
}
