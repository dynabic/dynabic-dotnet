using DynabicBilling.Classes;
using NUnit.Framework;

namespace DynabicBilling.Tests
{
    public class CustomerServiceTests : AssertionHelper
    {
        private BillingGateway _gateway;
        private TestsHelper _testsHelper;

        #region Hardcoded values for Country, StateProvince tests

        int _testCountryId = 13; // Australia
        string _testCountryName = "Australia";
        string _testCountryTwoLetterCode = "AU";
        string _testCountryThreeLetterCode = "AUS";

        int _testStateProvinceId = 164; // Victoria
        string _testStateProvinceName = "Victoria";

        int _testCurrencyId = 2; // AUD
        string _testCurrencyCode = "AUD";
        string _testCurrencyName = "Australian Dollar";

        #endregion

        [SetUp]
        public void Init()
        {
#if DEBUG
            //_gateway = new BillingGateway(BillingEnvironment.QA, "ab274898ca864dd6a1f3", "3b0276bc893d479d8aee");
            _gateway = new BillingGateway(BillingEnvironment.DEVELOPMENT, Constants.PUBLIC_KEY, Constants.PRIVATE_KEY);
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
            public int CustomerId { get; set; }
            public string ReferenceId { get; set; }
            public int CreditCardId { get; set; }
            public int BillingAddressId { get; set; }
        }

        private TestDataValues PrepareTestData()
        {
            var testData = new TestDataValues();

            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            testData.SiteId = site.Id;
            testData.Subdomain = site.Subdomain;

            var customer = _testsHelper.AddCustomer(site.Subdomain);
            Assert.IsNotNull(customer);
            testData.CustomerId = customer.Id;
            testData.ReferenceId = customer.ReferenceId;

            var creditCard = _testsHelper.AddCreditCard(customer.Id);
            Assert.IsNotNull(creditCard);
            testData.CreditCardId = creditCard.Id;

            var address = _testsHelper.AddAddress(customer.Id);
            Assert.IsNotNull(address);
            testData.BillingAddressId = address.Id;

            return testData;
        }

        private void GetCountryByCode(string countryCode)
        {
            var response = _gateway.Customer.GetCountryByCode(countryCode);
            Assert.IsNotNull(response);
            Assert.AreEqual(_testCountryName, response.Name);
        }

        #endregion Helpers

        #region Customers
        /*
        [Test]
        public void __GetAllCustomers()
        {
            var customers = _gateway.Customer.GetAllCustomers("Banckle", ContentFormat.JSON);
            //var customers = _gateway.Customer.GetAllCustomers("demoSubdomain", ContentFormat.JSON);
            Assert.IsNotNull(customers);
        }
        */
        [Test]
        public void GetAllCustomers()
        {
            var testData = PrepareTestData();
            try
            {
                var customers = _gateway.Customer.GetAllCustomers(testData.Subdomain);
                Assert.IsNotNull(customers);
                foreach (var customer in customers)
                {
                    _gateway.Customer.DeleteCustomer(customer.Id.ToString());
                }
            }
            finally
            {
                _testsHelper.DeleteSite(testData.SiteId);
            }
        }

        [Test]
        public void GetCustomer()
        {
            var testData = PrepareTestData();
            try
            {
                var customer = _gateway.Customer.GetCustomer(testData.CustomerId.ToString());
                Assert.IsNotNull(customer);
                _gateway.Customer.DeleteCustomer(customer.Id.ToString());
            }
            finally
            {
                _testsHelper.DeleteSite(testData.SiteId);
            }
        }

        [Test]
        public void GetCustomerByReferenceId()
        {
            var testData = PrepareTestData();
            try
            {
                var customer = _gateway.Customer.GetCustomerByReferenceId(testData.Subdomain, testData.ReferenceId);
                Assert.IsNotNull(customer);
                _gateway.Customer.DeleteCustomer(customer.Id.ToString());
            }
            finally
            {
                _testsHelper.DeleteSite(testData.SiteId);
            }
        }

        [Test]
        public void AddCustomer()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var customer = _testsHelper.AddCustomer(site.Subdomain);
                Assert.IsNotNull(customer);
                _gateway.Customer.DeleteCustomer(customer.Id.ToString());
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void UpdateCustomer()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var customer = _testsHelper.AddCustomer(site.Subdomain);
                Assert.IsNotNull(customer);
                /*
                var updateCustomer = new CustomerRequest
                {
                    Company = customer.Company + "_updated",
                    Email = customer.Email,
                    FirstName = customer.FirstName,
                    IsShippingAddressEqualToBilling = customer.IsShippingAddressEqualToBilling,
                    LastName = customer.LastName,
                    Phone = customer.Phone,
                    ReferenceId = customer.ReferenceId,
                };
                */
                customer.Company += "_updated";

                var updatedCustomer = _gateway.Customer.UpdateCustomer(customer, customer.Id.ToString());
                Assert.IsNotNull(updatedCustomer);
                Assert.AreEqual(customer.Company, updatedCustomer.Company);
                _gateway.Customer.DeleteCustomer(customer.Id.ToString());
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void DeleteCustomer()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var customer = _testsHelper.AddCustomer(site.Subdomain);
                Assert.IsNotNull(customer);
                _gateway.Customer.DeleteCustomer(customer.Id.ToString());
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        #endregion Customers

        #region CreditCards

        [Test]
        public void GetCreditCard()
        {
            var testData = PrepareTestData();
            try
            {
                var creditCard = _gateway.Customer.GetCreditCard(testData.CreditCardId.ToString());
                Assert.IsNotNull(creditCard);

                _gateway.Customer.DeleteCustomer(testData.CustomerId.ToString());
            }
            finally
            {
                _testsHelper.DeleteSite(testData.SiteId);
            }
        }

        [Test]
        public void GetCreditCards()
        {
            var testData = PrepareTestData();
            try
            {
                var creditCards = _gateway.Customer.GetCreditCards(testData.CustomerId.ToString());
                Assert.IsNotNull(creditCards);
                Assert.AreEqual(1, creditCards.Count);
                _gateway.Customer.DeleteCustomer(testData.CustomerId.ToString());
            }
            finally
            {
                _testsHelper.DeleteSite(testData.SiteId);
            }
        }

        [Test]
        public void AddAndDeleteCreditCard()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var customer = _testsHelper.AddCustomer(site.Subdomain);
                Assert.IsNotNull(customer);
                try
                {
                    var creditCard = _testsHelper.AddCreditCard(customer.Id);
                    Assert.IsNotNull(creditCard);
                }
                finally
                {
                    _gateway.Customer.DeleteCustomer(customer.Id.ToString());
                }
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void UpdateCreditCard()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var customer = _testsHelper.AddCustomer(site.Subdomain);
                Assert.IsNotNull(customer);

                var creditCard = _testsHelper.AddCreditCard(customer.Id);
                Assert.IsNotNull(creditCard);
                /*
                var updateCreditCard = new CreditCardRequest
                {
                    Cvv = creditCard.Cvv + "_updated",
                    ExpirationDate = creditCard.ExpirationDate,
                    FirstNameOnCard = creditCard.FirstNameOnCard,
                    LastNameOnCard = creditCard.LastNameOnCard,
                    Number = creditCard.Number,
                };
                */
                creditCard.FirstNameOnCard += "_updated";

                var updatedCreditCard = _gateway.Customer.UpdateCreditCard(customer.Id.ToString(), creditCard.Id.ToString(), creditCard);
                Assert.IsNotNull(updatedCreditCard);
                Assert.AreEqual(creditCard.FirstNameOnCard, updatedCreditCard.FirstNameOnCard);
                _gateway.Customer.DeleteCustomer(customer.Id.ToString());
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        #endregion CreditCards

        #region BillingAddresses

        [Test]
        public void GetBillingAddresses()
        {
            var testData = PrepareTestData();
            try
            {
                var addresses = _gateway.Customer.GetBillingAddresses(testData.CustomerId.ToString());
                Assert.IsNotNull(addresses);
                Assert.AreEqual(1, addresses.Count);
                _gateway.Customer.DeleteCustomer(testData.CustomerId.ToString());
            }
            finally
            {
                _testsHelper.DeleteSite(testData.SiteId);
            }
        }

        [Test]
        public void GetBillingAddress()
        {
            var testData = PrepareTestData();
            try
            {
                var address = _gateway.Customer.GetBillingAddress(testData.CustomerId.ToString());
                Assert.IsNotNull(address);
                _gateway.Customer.DeleteCustomer(testData.CustomerId.ToString());
            }
            finally
            {
                _testsHelper.DeleteSite(testData.SiteId);
            }
        }

        [Test]
        public void AddBillingAddress()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var customer = _testsHelper.AddCustomer(site.Subdomain);
                Assert.IsNotNull(customer);

                var address = _testsHelper.AddAddress(customer.Id);
                Assert.IsNotNull(address);
                _gateway.Customer.DeleteCustomer(customer.Id.ToString());
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void UpdateBillingAddress()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var customer = _testsHelper.AddCustomer(site.Subdomain);
                Assert.IsNotNull(customer);

                var address = _testsHelper.AddAddress(customer.Id);
                Assert.IsNotNull(address);
                address.Address1 += "_updated";

                var updatedAddress = _gateway.Customer.UpdateBillingAddress(customer.Id.ToString(), address.Id.ToString(), address);
                Assert.IsNotNull(updatedAddress);
                Assert.AreEqual(address.Address1, updatedAddress.Address1);
                _gateway.Customer.DeleteCustomer(customer.Id.ToString());
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void UpdateBillingAddressByCustomerRefId()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);

            try
            {
                var customer = _testsHelper.AddCustomer(site.Subdomain);
                Assert.IsNotNull(customer);

                var address = _testsHelper.AddAddress(customer.Id);
                Assert.IsNotNull(address);
                address.Address1 += "_updated";

                var updatedAddress = _gateway.Customer.UpdateBillingAddressByCustomerReferenceId(site.Subdomain,
                    customer.ReferenceId, address.Id.ToString(), address);
                Assert.IsNotNull(updatedAddress);
                Assert.AreEqual(address.Address1, updatedAddress.Address1);
                _gateway.Customer.DeleteCustomer(customer.Id.ToString());
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void DeleteBillingAddress()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var customer = _testsHelper.AddCustomer(site.Subdomain);
                Assert.IsNotNull(customer);

                var address = _testsHelper.AddAddress(customer.Id);
                Assert.IsNotNull(address);
                _gateway.Customer.DeleteCustomer(customer.Id.ToString());
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        #endregion BillingAddresses

        #region Country

        [Test]
        public void Test_GetAllCountries()
        {
            var response = _gateway.Customer.GetCountries();
            Assert.IsNotNull(response);
            Assert.Greater(response.Count, 200);
        }

        [Test]
        public void Test_GetCountryById()
        {
            var response = _gateway.Customer.GetCountry(_testCountryId.ToString());
            Assert.IsNotNull(response);
            Assert.AreEqual(_testCountryName, response.Name);
        }

        [Test]
        public void Test_GetCountryByCode()
        {
            GetCountryByCode(_testCountryTwoLetterCode);
            GetCountryByCode(_testCountryThreeLetterCode);
        }

        [Test]
        public void Test_GetCountryByName()
        {
            var response = _gateway.Customer.GetCountryByName(_testCountryName);
            Assert.IsNotNull(response);
            Assert.AreEqual(_testCountryName, response.Name);
        }

        #endregion Country

        #region StateProvince

        [Test]
        public void Test_GetStateProvincesByCountryId()
        {
            var response = _gateway.Customer.GetStateProvinces(_testCountryId.ToString());
            Assert.IsNotNull(response);
            Assert.Greater(response.Count, 1);
        }

        [Test]
        public void Test_GetStateProvincesByCountryCode()
        {
            var response = _gateway.Customer.GetStateProvincesByCountryCode(_testCountryTwoLetterCode.ToString());
            Assert.IsNotNull(response);
            Assert.Greater(response.Count, 1);
        }

        [Test]
        public void Test_GetStateProvinceById()
        {
            var response = _gateway.Customer.GetStateProvince(_testStateProvinceId.ToString());
            Assert.IsNotNull(response);
            Assert.AreEqual(_testStateProvinceName, response.Name);
        }

        [Test]
        public void Test_GetStateProvinceByName()
        {
            var response = _gateway.Customer.GetStateProvinceByName(_testStateProvinceName);
            Assert.IsNotNull(response);
            Assert.AreEqual(_testStateProvinceName, response.Name);
        }

        #endregion StateProvince

        #region Currency

        [Test]
        public void Test_GetAllCurrencies()
        {
            var response = _gateway.Customer.GetCurrencies();
            Assert.IsNotNull(response);
            Assert.Greater(response.Count, 150);
        }

        [Test]
        public void Test_GetCurrencyById()
        {
            var response = _gateway.Customer.GetCurrency(_testCurrencyId.ToString());
            Assert.IsNotNull(response);

            Assert.AreEqual(_testCurrencyCode, response.Code);
            Assert.AreEqual(_testCurrencyName, response.Name);
        }

        [Test]
        public void Test_GetCurrencyByCode()
        {
            var response = _gateway.Customer.GetCurrencyByCode(_testCurrencyCode);
            Assert.IsNotNull(response);

            Assert.AreEqual(_testCurrencyId, response.Id);
            Assert.AreEqual(_testCurrencyName, response.Name);
        }

        [Test]
        public void Test_GetCurrencyForCountry()
        {
            var response = _gateway.Customer.GetCurrencyForCountryThreeIsoCode(_testCountryThreeLetterCode);
            Assert.IsNotNull(response);

            Assert.AreEqual(_testCurrencyId, response.Id);
            Assert.AreEqual(_testCurrencyName, response.Name);
            Assert.AreEqual(_testCurrencyCode, response.Code);
        }

        #endregion Currency
    }
}
