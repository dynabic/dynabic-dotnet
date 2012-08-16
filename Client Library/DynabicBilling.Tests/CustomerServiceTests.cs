using DynabicBilling.Classes;
using NUnit.Framework;

namespace DynabicBilling.Tests
{
    public class CustomerServiceTests : AssertionHelper
    {
        private BillingGateway _gateway;
        private TestsHelper _testsHelper;
        private TestDataValues _testData;

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
            _gateway = new BillingGateway(BillingEnvironment.DEVELOPMENT, Constants.PUBLIC_KEY, Constants.PRIVATE_KEY);
#else
            _gateway = new BillingGateway(BillingEnvironment.QA, Constants.PUBLIC_KEY, Constants.PRIVATE_KEY);
#endif
            _testsHelper = new TestsHelper(_gateway);
            _testData = _testsHelper.PrepareCustomersTestData();
        }

        [TearDown]
        public void Cleanup()
        {
            _testsHelper.CleanupTestData();
        }

        #region Helpers

        private void GetCountryByCode(string countryCode)
        {
            var response = _gateway.Customer.GetCountryByCode(countryCode);
            Assert.IsNotNull(response);
            Assert.AreEqual(_testCountryName, response.Name);
        }

        #endregion Helpers

        #region Customers

        [Test]
        public void GetAllCustomers()
        {
            var customers = _gateway.Customer.GetAllCustomers(_testData.Subdomain);
            Assert.IsNotNull(customers);
        }

        [Test]
        public void GetCustomer()
        {
            var customer = _gateway.Customer.GetCustomer(_testData.CustomerId.ToString());
            Assert.IsNotNull(customer);
        }

        [Test]
        public void GetCustomerByReferenceId()
        {
            var customer = _gateway.Customer.GetCustomerByReferenceId(_testData.Subdomain, _testData.ReferenceId);
            Assert.IsNotNull(customer);
        }

        [Test]
        public void AddCustomer()
        {
            var customer = _gateway.Customer.GetCustomer(_testData.CustomerId.ToString());
            Assert.IsNotNull(customer);
        }

        [Test]
        public void UpdateCustomer()
        {
            var customer = _gateway.Customer.GetCustomer(_testData.CustomerId.ToString());
            Assert.IsNotNull(customer);

            customer.Company += "_updated";

            var updatedCustomer = _gateway.Customer.UpdateCustomer(customer, customer.Id.ToString());
            Assert.IsNotNull(updatedCustomer);
            Assert.AreEqual(customer.Company, updatedCustomer.Company);
        }

        [Test]
        public void DeleteCustomer()
        {
            _gateway.Customer.DeleteCustomer(_testData.CustomerId.ToString());
        }

        #endregion Customers

        #region CreditCards

        [Test]
        public void GetCreditCard()
        {
            var creditCard = _gateway.Customer.GetCreditCard(_testData.CreditCardId.ToString());
            Assert.IsNotNull(creditCard);
        }

        [Test]
        public void GetCreditCards()
        {
            var creditCards = _gateway.Customer.GetCreditCards(_testData.CustomerId.ToString());
            Assert.IsNotNull(creditCards);
            Assert.AreEqual(1, creditCards.Count);
        }

        [Test]
        public void AddAndDeleteCreditCard()
        {
            var creditCard = _testsHelper.AddCreditCard(_testData.CustomerId);
            Assert.IsNotNull(creditCard);
            _gateway.Customer.DeleteCreditCard(_testData.CustomerId.ToString(), creditCard.Id.ToString());
        }

        [Test]
        public void UpdateCreditCard()
        {
            var creditCard = _gateway.Customer.GetCreditCard(_testData.CreditCardId.ToString());
            Assert.IsNotNull(creditCard);

            creditCard.FirstNameOnCard += "_updated";
            creditCard.Number = "4111111111111111";
            creditCard.Cvv = "1234";

            var updatedCreditCard = _gateway.Customer.UpdateCreditCard(_testData.CustomerId.ToString(), creditCard.Id.ToString(), creditCard);
            Assert.IsNotNull(updatedCreditCard);

            Assert.AreEqual(creditCard.FirstNameOnCard, updatedCreditCard.FirstNameOnCard);
        }

        #endregion CreditCards

        #region BillingAddresses

        [Test]
        public void GetBillingAddresses()
        {
            var addresses = _gateway.Customer.GetBillingAddresses(_testData.CustomerId.ToString());
            Assert.IsNotNull(addresses);
            Assert.AreEqual(1, addresses.Count);
        }

        [Test]
        public void GetBillingAddress()
        {
            var address = _gateway.Customer.GetBillingAddress(_testData.CustomerId.ToString());
            Assert.IsNotNull(address);
        }

        [Test]
        public void AddBillingAddress()
        {
            var address = _testsHelper.AddAddress(_testData.CustomerId);
            Assert.IsNotNull(address);
            _gateway.Customer.DeleteBillingAddress(_testData.CustomerId.ToString(), address.Id.ToString());
        }

        [Test]
        public void UpdateBillingAddress()
        {
            var address = _gateway.Customer.GetBillingAddress(_testData.CustomerId.ToString());
            Assert.IsNotNull(address);

            address.Address1 += "_updated";

            var updatedAddress = _gateway.Customer.UpdateBillingAddress(_testData.CustomerId.ToString(), address.Id.ToString(), address);
            Assert.IsNotNull(updatedAddress);
            Assert.AreEqual(address.Address1, updatedAddress.Address1);
        }

        [Test]
        public void UpdateBillingAddressByCustomerRefId()
        {
            var address = _gateway.Customer.GetBillingAddress(_testData.CustomerId.ToString());
            Assert.IsNotNull(address);

            address.Address1 += "_updated";

            var updatedAddress = _gateway.Customer.UpdateBillingAddressByCustomerReferenceId(_testData.Subdomain,
                _testData.ReferenceId, address.Id.ToString(), address);
            Assert.IsNotNull(updatedAddress);
            Assert.AreEqual(address.Address1, updatedAddress.Address1);
        }

        [Test]
        public void DeleteBillingAddress()
        {
            var address = _testsHelper.AddAddress(_testData.CustomerId);
            Assert.IsNotNull(address);
            _gateway.Customer.DeleteBillingAddress(_testData.CustomerId.ToString(), address.Id.ToString());
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
