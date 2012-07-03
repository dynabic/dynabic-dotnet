using DynabicPlatform.Classes;
using DynabicPlatform.RestApiDataContract;
using NUnit.Framework;

namespace DynabicPlatform.Tests
{
    public class SiteServiceTests : AssertionHelper
    {
        private PlatformGateway _gateway;

        [SetUp]
        public void Init()
        {
#if DEBUG
            _gateway = new PlatformGateway(PlatformEnvironment.DEVELOPMENT, Constants.PUBLIC_KEY, Constants.PRIVATE_KEY);
#else
            _gateway = new PlatformGateway(PlatformEnvironment.QA, Constants.PUBLIC_KEY, Constants.PRIVATE_KEY);
#endif
        }

        #region Helpers

        private SiteResponse AddSite()
        {
            var newSite = new SiteRequest
            {
                IsTestMode = true,
                Name = "Name",
                Subdomain = "demoSubdomain",
            };
            return _gateway.Sites.AddSite(newSite);
        }

        private void DeleteSite(int id)
        {
            _gateway.Sites.DeleteSite(id.ToString());
        }

        #endregion Helpers

        [Test]
        public void GetSites()
        {
            var sites = _gateway.Sites.GetSites();
            Assert.IsNotNull(sites);
        }

        [Test]
        public void GetSiteById()
        {
            var newSite = AddSite();
            Assert.IsNotNull(newSite);
            try
            {
                var site = _gateway.Sites.GetSiteById(newSite.Id.ToString());
                Assert.IsNotNull(site);
                Assert.AreEqual(newSite.Id, site.Id);
            }
            finally
            {
                DeleteSite(newSite.Id);
            }
        }

        [Test]
        public void GetSiteBySubdomain()
        {
            var newSite = AddSite();
            Assert.IsNotNull(newSite);
            try
            {
                var site = _gateway.Sites.GetSiteBySubdomain(newSite.Subdomain);
                Assert.IsNotNull(site);
                Assert.AreEqual(newSite.Subdomain.ToLower(), site.Subdomain.ToLower());
            }
            finally
            {
                DeleteSite(newSite.Id);
            }
        }

        [Test]
        public void GetSitesByName()
        {
            var newSite = AddSite();
            Assert.IsNotNull(newSite);
            try
            {
                var sites = _gateway.Sites.GetSitesByName(newSite.Name);
                Assert.IsNotNull(sites);
                foreach (var site in sites)
                {
                    Assert.AreEqual(newSite.Name, site.Name);
                }
            }
            finally
            {
                DeleteSite(newSite.Id);
            }
        }

        [Test]
        public void AddAndDeleteSite()
        {
            var newSite = AddSite();
            Assert.IsNotNull(newSite);
            DeleteSite(newSite.Id);
        }

        [Test]
        public void UpdateSite()
        {
            var newSite = AddSite();
            Assert.IsNotNull(newSite);
            try
            {
                // get exists site
                var site = _gateway.Sites.GetSiteById(newSite.Id.ToString());
                Assert.IsNotNull(site);
                Assert.AreEqual(newSite.Id, site.Id);

                // change name property
                var updateValue = "test site update";
                site.Name = updateValue;

                var updatedSite = _gateway.Sites.UpdateSite(site, newSite.Id.ToString());
                Assert.IsNotNull(updatedSite);
                Assert.AreEqual(updateValue, updatedSite.Name);
            }
            finally
            {
                DeleteSite(newSite.Id);
            }
        }
    }
}
