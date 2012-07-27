using System;
using System.Linq;
using DynabicBilling.Classes;
using DynabicBilling.RestApiDataContract;
using DynabicPlatform.RestApiDataContract;
using NUnit.Framework;

namespace DynabicBilling.Tests
{
    internal class TestsHelper
    {
        private const string TEST_SITE = "demoSubdomain";
        private readonly BillingGateway _gateway;

        public TestsHelper(BillingGateway gateway)
        {
            _gateway = gateway;
            CleanupTestData();
        }

        #region Helpers

        private AddressRequest CreateDafaultAddress()
        {
            return new AddressRequest()
            {
                Address1 = "St. King",
                Address2 = "George Street",
                City = "London",
                Company = "TEST & Co.",
                Country = "UK",
                Email = "a@a.com",
                FaxNumber = "1111111111",
                FirstName = "John",
                LastName = "Wallace",
                PhoneNumber = "3333333333",
                StateProvince = "London",
                ZipPostalCode = "2222"
            };
        }

        private CreditCardRequest CreateDefaultCreditCard()
        {
            return new CreditCardRequest()
            {
                Cvv = "123",
                ExpirationDate = DateTime.Now.AddYears(2),
                FirstNameOnCard = "John B.",
                LastNameOnCard = "Wallace",
                Number = "4111111111111111"
            };
        }

        private Currency CreateDefaultCurrency()
        {
            return new Currency()
            {
                Code = "USD",
                Id = 1,
                Name = "US Dollars",
            };
        }

        private CustomerRequest CreateDefaultCustomer()
        {
            return new CustomerRequest()
            {
                Company = "TEST & Co.",
                Email = "a@a.com",
                FirstName = "John",
                IsShippingAddressEqualToBilling = false,
                LastName = "Wallace",
                Phone = "3333333333",
                ReferenceId = "123456",
                ShippingAddress = CreateDafaultAddress()
            };
        }

        private CustomerResponse AddCustomer(string subdomain)
        {
            var request = CreateDefaultCustomer();
            return _gateway.Customer.AddCustomer(subdomain, request);
        }

        public CreditCardResponse AddCreditCard(int customerId)
        {
            var request = CreateDefaultCreditCard();
            return _gateway.Customer.AddCreditCard(customerId.ToString(), request);
        }

        public AddressResponse AddAddress(int customerId)
        {
            var request = CreateDafaultAddress();
            return _gateway.Customer.AddBillingAddress(customerId.ToString(), request);
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

        public ProductFamilyResponse AddProductFamily(int siteId)
        {
            var prodFamily = new ProductFamilyRequest
            {
                SiteId = siteId,
                Name = "StarterKit v1.0 " + Guid.NewGuid().ToString("N"),
                Description = "Description",
            };
            return _gateway.ProductFamilies.AddProductFamily(prodFamily);
        }

        public ProductResponse AddProduct(int siteId)
        {
            var family = AddProductFamily(siteId);
            Assert.IsNotNull(family);
            return AddProductToFamily(family.Id);
        }

        public ProductResponse AddProductToFamily(int familyId)
        {
            var productReq = new ProductRequest
            {
                FamilyId = familyId,
                AccountingCode = "AccountingCode",
                ApiRef1 = Guid.NewGuid().ToString("N"),
                Description = "Description",
                isBillingAddressAtSignupRequired = BoolOptional.Yes,
                isCreditCardAtSignupRequired = BoolOptional.Yes,
                Name = "Product - " + Guid.NewGuid().ToString("N"),
            };

            var pricingPlanReq = new PricingPlanRequest
            {
                CurrencyCode = CreateDefaultCurrency().Code,
                Name = "TestPricingPlan",
                TrialPeriodCharge = 100,
                TrialPeriodDurationDays = 3,
                UpfrontCharge = 200,
            };
            productReq.PricingPlans.Add(pricingPlanReq);

            var paymentSchedule = new ProductPricingPlanPaymentScheduleRequest
            {
                FrequencyInterval = 1,
                FrequencyRecurrenceFactor = 1,
                FrequencyRelativeInterval = FrequencyOccurrence.Fifth,
                FrequencyType = RecurringCyclePeriod.Daily,
                Name = string.Format("{0}_PaymentSchedule", pricingPlanReq.Name),
            };
            pricingPlanReq.PaymentScheduleList.Add(paymentSchedule);

            var meteredPriceList = new ProductMeteredPriceRequestList();
            meteredPriceList.Add(new ProductMeteredPriceRequest
            {
                StartQuantity = 1,
                EndQuantity = 10,
                UnitPrice = 5,
            });

            pricingPlanReq.ProductItemsList.Add(new ProductItemRequest
            {
                ChargeModel = ChargeModel.PerUnit,
                Name = "test product item 1",
                Description = "",
                ProductItemType = ProductItemType.Metered,
                IsVisibleOnHostedPage = true,
                MeteredPriceList = meteredPriceList,
            });

            meteredPriceList = new ProductMeteredPriceRequestList();
            meteredPriceList.Add(new ProductMeteredPriceRequest
            {
                StartQuantity = 1,
                UnitPrice = 10,
            });
            pricingPlanReq.ProductItemsList.Add(new ProductItemRequest
            {
                ChargeModel = ChargeModel.PerUnit,
                Name = "test product item 2",
                Description = "",
                ProductItemType = ProductItemType.OnOff,
                IsVisibleOnHostedPage = true,
                MeteredPriceList = meteredPriceList,
            });

            var product = _gateway.Products.AddProduct(productReq);
            Assert.IsNotNull(product);
            return product;
        }

        private SubscriptionResponse AddTestSubscription(SiteResponse site)
        {
            var product = AddProduct(site.Id);

            var subscriptionItems = new SubscriptionItemRequestList();
            foreach (var item in product.PricingPlans[0].ProductItemsList)
            {
                subscriptionItems.Add(new SubscriptionItemRequest
                {
                    ProductItemId = item.Id,
                    Quantity = 5,
                    UpdateDescription = "Add new subscription",
                });
            }

            var subscriptionRequest = new SubscriptionRequest()
            {
                BillingAddress = CreateDafaultAddress(),
                CreditCard = CreateDefaultCreditCard(),
                Currency = CreateDefaultCurrency(),
                CurrentBallance = 100,
                Customer = CreateDefaultCustomer(),
                NextAssesment = DateTime.Now.AddDays(31),
                NextProduct = null,
                NextProductPricingPlan = null,
                ProductId = product.Id,
                ProductPricingPlanId = product.PricingPlans[0].Id,
                StartDate = DateTime.Now,
                SubscriptionItems = subscriptionItems,
            };
            return _gateway.Subscription.AddSubscription(site.Subdomain, subscriptionRequest);
        }

        public void CleanupTestData()
        {
            try
            {
                var site = _gateway.Sites.GetSiteBySubdomain(TEST_SITE);
                if (site != null)
                {
                    try
                    {
                        var customers = _gateway.Customer.GetAllCustomers(site.Subdomain);
                        if (customers != null)
                        {
                            foreach (var customer in customers)
                            {
                                try
                                {
                                    var cards = _gateway.Customer.GetCreditCards(customer.Id.ToString());
                                    if (cards != null)
                                    {
                                        foreach (var card in cards)
                                        {
                                            _gateway.Customer.DeleteCreditCard(customer.Id.ToString(), card.Id.ToString());
                                        }
                                    }
                                }
                                catch { }
                                _gateway.Customer.DeleteCustomer(customer.Id.ToString());
                            }
                        }
                    }
                    catch { }
                    _gateway.Sites.DeleteSite(site.Id.ToString());
                }
            }
            catch { }
        }

        #endregion

        #region Tests

        public TestDataValues PrepareCustomersTestData()
        {
            var testData = new TestDataValues();

            var site = AddSite();
            Assert.IsNotNull(site);
            testData.SiteId = site.Id;
            testData.Subdomain = site.Subdomain;

            var customer = AddCustomer(site.Subdomain);
            Assert.IsNotNull(customer);
            testData.CustomerId = customer.Id;
            testData.ReferenceId = customer.ReferenceId;

            var creditCard = AddCreditCard(customer.Id);
            Assert.IsNotNull(creditCard);
            testData.CreditCardId = creditCard.Id;

            var address = AddAddress(customer.Id);
            Assert.IsNotNull(address);
            testData.BillingAddressId = address.Id;

            return testData;
        }

        public TestDataValues PrepareEventsTestData()
        {
            var testData = new TestDataValues();
            var site = AddSite();
            Assert.IsNotNull(site);
            testData.SiteId = site.Id;
            testData.Subdomain = site.Subdomain;

            var subscription = AddTestSubscription(site);
            Assert.IsNotNull(subscription);
            testData.SubscriptionId = subscription.Id;

            return testData;
        }

        public TestDataValues PrepareProductFamiliesTestData()
        {
            var testData = new TestDataValues();
            var site = AddSite();
            Assert.IsNotNull(site);
            testData.SiteId = site.Id;
            testData.Subdomain = site.Subdomain;

            var prodFamily = AddProductFamily(site.Id);
            Assert.IsNotNull(prodFamily);
            testData.ProductFamilyId = prodFamily.Id;
            testData.ProductFamilyName = prodFamily.Name;
            AddProductFamily(site.Id);
            AddProductFamily(site.Id);

            var subscription = AddTestSubscription(site);
            Assert.IsNotNull(subscription);
            testData.SubscriptionId = subscription.Id;
            testData.CustomerId = subscription.CustomerId;
            testData.CreditCardId = subscription.CreditCardId.GetValueOrDefault();

            return testData;
        }

        public TestDataValues PrepareProductsTestData()
        {
            var testData = new TestDataValues();

            var site = AddSite();
            Assert.IsNotNull(site);
            testData.SiteId = site.Id;
            testData.Subdomain = site.Subdomain;

            var family = AddProductFamily(site.Id);
            Assert.IsNotNull(family);
            testData.ProductFamilyId = family.Id;
            testData.ProductFamilyName = family.Name;

            var product = AddProductToFamily(family.Id);
            Assert.IsNotNull(product);
            testData.ProductId = product.Id;
            testData.ProductName = product.Name;
            testData.ReferenceId = product.ApiRef1;
            return testData;
        }

        public TestDataValues PrepareReportsTestData()
        {
            return PrepareProductFamiliesTestData();
        }

        internal TestDataValues PrepareStatementsTestData()
        {
            return PrepareProductFamiliesTestData();
        }

        internal TestDataValues PrepareSubscriptionsTestData()
        {
            return PrepareProductFamiliesTestData();
        }

        internal TestDataValues PrepareTransactionsTestData()
        {
            return PrepareProductFamiliesTestData();
        }

        #endregion
    }

    internal class TestDataValues
    {
        public int SiteId { get; set; }
        public string Subdomain { get; set; }
        public int CustomerId { get; set; }
        public string ReferenceId { get; set; }
        public int CreditCardId { get; set; }
        public int BillingAddressId { get; set; }
        public int SubscriptionId { get; set; }
        public int ProductFamilyId { get; set; }
        public string ProductFamilyName { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
    }


}
