using DynabicPlatform.Classes;
using DynabicPlatform.RestApiDataContract;
using NUnit.Framework;

namespace DynabicPlatform.Tests
{
    public class SettingServiceTests : AssertionHelper
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

        private void DeleteSite(int id)
        {
            _gateway.Sites.DeleteSite(id.ToString());
        }

        private SiteResponse PrepareTestData()
        {
            // create new site
            var newSite = new SiteRequest
            {
                IsTestMode = true,
                Name = "Name",
                Subdomain = "demoSubdomain",
            };
            var site = _gateway.Sites.AddSite(newSite);
            Assert.IsNotNull(site);
            try
            {
                var settings = _gateway.Settings.GetSettings(site.Subdomain);
                Assert.IsNotNull(settings);
                Assert.Greater(settings.Count, 2);

                // add some settings
                var newSetting = new SettingRequest
                {
                    SiteId = site.Id,
                    Description = settings[0].Description,
                    Name = settings[0].Name,
                    Value = settings[0].Value,
                };

                var setting = _gateway.Settings.UpdateSetting(site.Subdomain, settings[0].SettingId.ToString(), newSetting);
                Assert.IsNotNull(setting);

                newSetting = new SettingRequest
                {
                    SiteId = site.Id,
                    Description = settings[1].Description,
                    Name = settings[1].Name,
                    Value = settings[1].Value,
                };

                setting = _gateway.Settings.UpdateSetting(site.Subdomain, settings[0].SettingId.ToString(), newSetting);
                Assert.IsNotNull(setting);
            }
            catch
            {
                DeleteSite(site.Id);
                throw;
            }
            return site;
        }

        #endregion Helpers

        [Test]
        public void GetSettings()
        {
            var site = PrepareTestData();
            try
            {
                var settings = _gateway.Settings.GetSettings(site.Subdomain);
                Assert.IsNotNull(settings);
            }
            finally
            {
                DeleteSite(site.Id);
            }
        }

        [Test]
        public void GetSettingById()
        {
            var site = PrepareTestData();
            try
            {
                var settings = _gateway.Settings.GetSettings(site.Subdomain);
                Assert.IsNotNull(settings);
                Assert.Greater(settings.Count, 0);

                foreach (var setting in settings)
                {
                    var settingById = _gateway.Settings.GetSettingById(site.Subdomain, setting.SettingId.ToString());
                    Assert.IsNotNull(settingById);
                    Assert.AreEqual(setting.SettingId, settingById.SettingId);
                }
            }
            finally
            {
                DeleteSite(site.Id);
            }
        }

        [Test]
        public void GetSettingByName()
        {
            var site = PrepareTestData();
            try
            {
                var settings = _gateway.Settings.GetSettings(site.Subdomain);
                Assert.IsNotNull(settings);
                Assert.Greater(settings.Count, 0);

                foreach (var setting in settings)
                {
                    var settingById = _gateway.Settings.GetSettingByName(site.Subdomain, setting.Name);
                    Assert.IsNotNull(settingById);
                    Assert.AreEqual(setting.Name, settingById.Name);
                }
            }
            finally
            {
                DeleteSite(site.Id);
            }
        }

        [Test]
        public void UpdateSetting()
        {
            var site = PrepareTestData();
            try
            {
                var settings = _gateway.Settings.GetSettings(site.Subdomain);
                Assert.IsNotNull(settings);
                Assert.Greater(settings.Count, 0);

                // get exists setting
                var setting = _gateway.Settings.GetSettingById(site.Subdomain, settings[0].SettingId.ToString());
                Assert.IsNotNull(setting);

                // change description property
                var updateValue = "test setting update";
                /*
                var updateSetting = new SettingRequest
                {
                    Description = updateValue,
                    Name = setting.Name,
                    SiteId = setting.SiteId,
                    Value = setting.Value,
                };
                */
                setting.Description = updateValue;

                var updatedSetting = _gateway.Settings.UpdateSetting(site.Subdomain, setting.SettingId.ToString(), setting);
                Assert.IsNotNull(updatedSetting);
                Assert.AreEqual(updateValue, updatedSetting.Description);
            }
            finally
            {
                DeleteSite(site.Id);
            }
        }

        [Test]
        public void UpdateSettingWithExplicitParameters()
        {
            var site = PrepareTestData();
            try
            {
                var settings = _gateway.Settings.GetSettings(site.Subdomain);
                Assert.IsNotNull(settings);
                Assert.Greater(settings.Count, 0);

                // get exists setting
                var setting = _gateway.Settings.GetSettingById(site.Subdomain, settings[0].SettingId.ToString());
                Assert.IsNotNull(setting);

                // change description property
                var updateValue = "test setting update";
                var updatedSetting = _gateway.Settings.UpdateSettingWithExplicitParameters(site.Subdomain, setting.SettingId.ToString(), setting.Name, updateValue, updateValue);
                Assert.IsNotNull(updatedSetting);
                Assert.AreEqual(updateValue, updatedSetting.Description);
            }
            finally
            {
                DeleteSite(site.Id);
            }
        }

        [Test]
        public void UpdateSettingWithExplicitParameters2()
        {
            var site = PrepareTestData();
            try
            {
                var settings = _gateway.Settings.GetSettings(site.Subdomain);
                Assert.IsNotNull(settings);
                Assert.Greater(settings.Count, 0);

                // get exists setting
                var setting = _gateway.Settings.GetSettingById(site.Subdomain, settings[0].SettingId.ToString());
                Assert.IsNotNull(setting);

                // change description property
                var updateValue = "test setting update";
                var updatedSetting = _gateway.Settings.UpdateSettingWithExplicitParameters2(site.Subdomain, setting.SettingId.ToString(), updateValue);
                Assert.IsNotNull(updatedSetting);
                Assert.AreEqual(updateValue, updatedSetting.Value);
            }
            finally
            {
                DeleteSite(site.Id);
            }
        }

        [Test]
        public void GetDefaultSetting()
        {
            var site = PrepareTestData();
            try
            {
                var settings = _gateway.Settings.GetSettings(site.Subdomain);
                Assert.IsNotNull(settings);
                Assert.Greater(settings.Count, 0);

                var setting = _gateway.Settings.GetDefaultSetting(settings[0].Name);
                Assert.IsNotNull(setting);
                Assert.AreEqual(settings[0].Name, setting.Name);
            }
            finally
            {
                DeleteSite(site.Id);
            }
        }
    }
}
