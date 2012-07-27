using DynabicPlatform.Classes;
using DynabicPlatform.RestApiDataContract;
using NUnit.Framework;

namespace DynabicPlatform.Tests
{
    public class SiteServiceTests : AssertionHelper
    {
        private PlatformGateway _gateway;
        private TestsHelper _testsHelper;
        private TestDataValues _testData;

        [SetUp]
        public void Init()
        {
#if DEBUG
            _gateway = new PlatformGateway(PlatformEnvironment.DEVELOPMENT, Constants.PUBLIC_KEY, Constants.PRIVATE_KEY);
#else
            _gateway = new PlatformGateway(PlatformEnvironment.QA, Constants.PUBLIC_KEY, Constants.PRIVATE_KEY);
#endif
            _testsHelper = new TestsHelper(_gateway);
            _testData = _testsHelper.PrepareSitesTestData();
        }

        [TearDown]
        public void Cleanup()
        {
            _testsHelper.CleanupTestData();
        }

        [Test]
        public void GetSites()
        {
            var sites = _gateway.Sites.GetSites();
            Assert.IsNotNull(sites);
        }

        [Test]
        public void GetSiteById()
        {
            var site = _gateway.Sites.GetSiteById(_testData.SiteId.ToString());
            Assert.IsNotNull(site);
            Assert.AreEqual(_testData.SiteId, site.Id);
        }

        [Test]
        public void GetSiteBySubdomain()
        {
            var site = _gateway.Sites.GetSiteBySubdomain(_testData.Subdomain);
            Assert.IsNotNull(site);
            Assert.AreEqual(_testData.Subdomain.ToLower(), site.Subdomain.ToLower());
        }

        [Test]
        public void GetSitesByName()
        {
            var sites = _gateway.Sites.GetSitesByName(_testData.SiteName);
            Assert.IsNotNull(sites);
            foreach (var site in sites)
            {
                Assert.AreEqual(_testData.SiteName, site.Name);
            }
        }

        [Test]
        public void UpdateSite()
        {
            // get exists site
            var site = _gateway.Sites.GetSiteById(_testData.SiteId.ToString());
            Assert.IsNotNull(site);
            Assert.AreEqual(_testData.SiteId, site.Id);

            // change name property
            var updateValue = "test site update";
            site.Name = updateValue;

            var updatedSite = _gateway.Sites.UpdateSite(site, _testData.SiteId.ToString());
            Assert.IsNotNull(updatedSite);
            Assert.AreEqual(updateValue, updatedSite.Name);
        }
    }
}
