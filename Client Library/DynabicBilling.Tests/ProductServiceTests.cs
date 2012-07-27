using DynabicBilling.Classes;
using NUnit.Framework;

namespace DynabicBilling.Tests
{
    public class ProductServiceTests : AssertionHelper
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
            _testData = _testsHelper.PrepareProductsTestData();
        }

        [TearDown]
        public void Cleanup()
        {
            _testsHelper.CleanupTestData();
        }

        [Test]
        public void GetProductsBySite()
        {
            var products = _gateway.Products.GetProductsBySite(_testData.Subdomain);
            Assert.IsNotNull(products);
            Assert.AreEqual(1, products.Count);
        }

        [Test]
        public void GetProductsBySiteAndFamily()
        {
            var products = _gateway.Products.GetProductsBySiteAndFamily(_testData.Subdomain, _testData.ProductFamilyName);
            Assert.IsNotNull(products);
            Assert.AreEqual(1, products.Count);
        }

        [Test]
        public void GetProductsBySiteAndProductName()
        {
            var products = _gateway.Products.GetProductsBySiteAndProductName(_testData.Subdomain, _testData.ProductName);
            Assert.IsNotNull(products);
            Assert.AreEqual(1, products.Count);
        }

        [Test]
        public void GetProductByApiRef()
        {
            var product = _gateway.Products.GetProductByApiRef(_testData.SiteId.ToString(), _testData.ReferenceId);
            Assert.IsNotNull(product);
        }

        [Test]
        public void GetProductsByFamilyId()
        {
            var products = _gateway.Products.GetProductsByFamilyId(_testData.ProductFamilyId.ToString());
            Assert.IsNotNull(products);
            Assert.AreEqual(1, products.Count);
        }

        [Test]
        public void GetProductByFamilyIdAndProductName()
        {
            var product = _gateway.Products.GetProductByFamilyIdAndProductName(_testData.ProductFamilyId.ToString(), _testData.ProductName);
            Assert.IsNotNull(product);
        }

        [Test]
        public void GetProductById()
        {
            var product = _gateway.Products.GetProductById(_testData.ProductId.ToString());
            Assert.IsNotNull(product);
        }

        [Test]
        public void AddProduct()
        {
            var product = _testsHelper.AddProduct(_testData.SiteId);
            Assert.IsNotNull(product);
            _gateway.Products.DeleteProduct(product.Id.ToString());
        }

        [Test]
        public void UpdateProduct()
        {
            var product = _gateway.Products.GetProductById(_testData.ProductId.ToString());
            Assert.IsNotNull(product);

            product.ApiRef1 += "_updated";

            var updatedProduct = _gateway.Products.UpdateProduct(product, product.Id.ToString());
            Assert.IsNotNull(updatedProduct);
            Assert.AreEqual(product.ApiRef1, updatedProduct.ApiRef1);
        }

        [Test]
        public void DeleteProduct()
        {
            var product = _testsHelper.AddProduct(_testData.SiteId);
            Assert.IsNotNull(product);
            _gateway.Products.DeleteProduct(product.Id.ToString());
        }

        [Test]
        public void ArchiveProduct()
        {
            var product = _gateway.Products.GetProductById(_testData.ProductId.ToString());
            Assert.IsNotNull(product);
            Assert.False(product.isArchived);

            _gateway.Products.ArchiveProduct(product.Id.ToString());

            var archivedProduct = _gateway.Products.GetProductById(product.Id.ToString());
            Assert.True(archivedProduct.isArchived);
        }

        [Test]
        public void ActivateProduct()
        {
            var product = _gateway.Products.GetProductById(_testData.ProductId.ToString());
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
    }
}
