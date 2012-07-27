using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DynabicPlatform.Classes;
using DynabicPlatform.RestApiDataContract;
using NUnit.Framework;

namespace DynabicPlatform.Tests
{
    internal class TestsHelper
    {
        private const string TEST_SITE = "demoSubdomain";
        private const string USER_EMAIL = "sergey.slavin@dynabic.com";
        private readonly PlatformGateway _gateway;

        public TestsHelper(PlatformGateway gateway)
        {
            _gateway = gateway;
            CleanupTestData();
        }

        public void CleanupTestData()
        {
            try
            {
                var site = _gateway.Sites.GetSiteBySubdomain(TEST_SITE);
                if (site != null)
                {
                    //try
                    //{
                    //    var customers = _gateway.Customer.GetAllCustomers(site.Subdomain);
                    //    if (customers != null)
                    //    {
                    //        foreach (var customer in customers)
                    //        {
                    //            try
                    //            {
                    //                var cards = _gateway.Customer.GetCreditCards(customer.Id.ToString());
                    //                if (cards != null)
                    //                {
                    //                    foreach (var card in cards)
                    //                    {
                    //                        _gateway.Customer.DeleteCreditCard(customer.Id.ToString(), card.Id.ToString());
                    //                    }
                    //                }
                    //            }
                    //            catch { }
                    //            _gateway.Customer.DeleteCustomer(customer.Id.ToString());
                    //        }
                    //    }
                    //}
                    //catch { }
                    _gateway.Sites.DeleteSite(site.Id.ToString());
                }
            }
            catch { }

            try
            {
                var user = _gateway.Users.GetUserByUserName(USER_EMAIL);
                if (user != null)
                {
                    _gateway.Users.DeleteUser(user.Id.ToString());
                }
            }
            catch { }

        }

        private SiteResponse AddSite()
        {
            var newSite = new SiteRequest
            {
                IsTestMode = true,
                Name = TEST_SITE,
                Subdomain = TEST_SITE,
            };
            return _gateway.Sites.AddSite(newSite);
        }

        private UserResponse AddUser()
        {
            var newUser = new UserRequest();
            newUser.Email = USER_EMAIL;
            newUser.FacebookId = "FacebookId";
            newUser.GoogleAppsUserName = "GoogleAppsUserName";
            newUser.FirstName = "FirstName";
            newUser.LastName = "LastName";
            newUser.YahooUserName = "YahooUserName";
            newUser.PasswordHash = "test123";
            newUser.Active = true;
            newUser.Deleted = false;

            return _gateway.Users.AddUser(newUser);
        }

        public TestDataValues PrepareSettingsTestData()
        {
            var testData = new TestDataValues();
            var site = AddSite();
            testData.SiteId = site.Id;
            testData.Subdomain = site.Subdomain;
            testData.SiteName = site.Name;

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

            return testData;
        }

        internal TestDataValues PrepareSitesTestData()
        {
            var testData = new TestDataValues();
            var site = AddSite();
            testData.SiteId = site.Id;
            testData.Subdomain = site.Subdomain;
            testData.SiteName = site.Name;

            return testData;
        }

        internal TestDataValues PrepareUsersTestData()
        {
            var testData = new TestDataValues();
            var user = AddUser();
            testData.UserId = user.Id;
            testData.UserEmail = user.Email;

            return testData;
        }
    }

    internal class TestDataValues
    {
        public int SiteId { get; set; }
        public string Subdomain { get; set; }
        public string SiteName { get; set; }
        public int UserId { get; set; }
        public string UserEmail { get; set; }
    }

}
