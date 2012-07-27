using DynabicPlatform.Classes;
using DynabicPlatform.RestApiDataContract;
using NUnit.Framework;

namespace DynabicPlatform.Tests
{
    public class SettingServiceTests : AssertionHelper
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
            _testData = _testsHelper.PrepareSettingsTestData();
        }

        [TearDown]
        public void Cleanup()
        {
            _testsHelper.CleanupTestData();
        }

        [Test]
        public void GetSettings()
        {
            var settings = _gateway.Settings.GetSettings(_testData.Subdomain);
            Assert.IsNotNull(settings);
        }

        [Test]
        public void GetSettingById()
        {
            var settings = _gateway.Settings.GetSettings(_testData.Subdomain);
            Assert.IsNotNull(settings);
            Assert.Greater(settings.Count, 0);

            foreach (var setting in settings)
            {
                var settingById = _gateway.Settings.GetSettingById(_testData.Subdomain, setting.SettingId.ToString());
                Assert.IsNotNull(settingById);
                Assert.AreEqual(setting.SettingId, settingById.SettingId);
            }
        }

        [Test]
        public void GetSettingByName()
        {
            var settings = _gateway.Settings.GetSettings(_testData.Subdomain);
            Assert.IsNotNull(settings);
            Assert.Greater(settings.Count, 0);

            foreach (var setting in settings)
            {
                var settingById = _gateway.Settings.GetSettingByName(_testData.Subdomain, setting.Name);
                Assert.IsNotNull(settingById);
                Assert.AreEqual(setting.Name, settingById.Name);
            }
        }

        [Test]
        public void UpdateSetting()
        {
            var settings = _gateway.Settings.GetSettings(_testData.Subdomain);
            Assert.IsNotNull(settings);
            Assert.Greater(settings.Count, 0);

            // get exists setting
            var setting = _gateway.Settings.GetSettingById(_testData.Subdomain, settings[0].SettingId.ToString());
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

            var updatedSetting = _gateway.Settings.UpdateSetting(_testData.Subdomain, setting.SettingId.ToString(), setting);
            Assert.IsNotNull(updatedSetting);
            Assert.AreEqual(updateValue, updatedSetting.Description);
        }

        [Test]
        public void UpdateSettingWithExplicitParameters()
        {
            var settings = _gateway.Settings.GetSettings(_testData.Subdomain);
            Assert.IsNotNull(settings);
            Assert.Greater(settings.Count, 0);

            // get exists setting
            var setting = _gateway.Settings.GetSettingById(_testData.Subdomain, settings[0].SettingId.ToString());
            Assert.IsNotNull(setting);

            // change description property
            var updateValue = "test setting update";
            var updatedSetting = _gateway.Settings.UpdateSettingWithExplicitParameters(_testData.Subdomain, setting.SettingId.ToString(), setting.Name, updateValue, updateValue);
            Assert.IsNotNull(updatedSetting);
            Assert.AreEqual(updateValue, updatedSetting.Description);
        }

        [Test]
        public void UpdateSettingWithExplicitParameters2()
        {
            var settings = _gateway.Settings.GetSettings(_testData.Subdomain);
            Assert.IsNotNull(settings);
            Assert.Greater(settings.Count, 0);

            // get exists setting
            var setting = _gateway.Settings.GetSettingById(_testData.Subdomain, settings[0].SettingId.ToString());
            Assert.IsNotNull(setting);

            // change description property
            var updateValue = "test setting update";
            var updatedSetting = _gateway.Settings.UpdateSettingWithExplicitParameters2(_testData.Subdomain, setting.SettingId.ToString(), updateValue);
            Assert.IsNotNull(updatedSetting);
            Assert.AreEqual(updateValue, updatedSetting.Value);
        }

        [Test]
        public void GetDefaultSetting()
        {
            var settings = _gateway.Settings.GetSettings(_testData.Subdomain);
            Assert.IsNotNull(settings);
            Assert.Greater(settings.Count, 0);

            var setting = _gateway.Settings.GetDefaultSetting(settings[0].Name);
            Assert.IsNotNull(setting);
            Assert.AreEqual(settings[0].Name, setting.Name);
        }
    }
}
