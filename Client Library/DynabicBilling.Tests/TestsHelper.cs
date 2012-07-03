
using System;
using DynabicBilling.Classes;
using DynabicBilling.RestApiDataContract;
using DynabicPlatform.RestApiDataContract;
using NUnit.Framework;
namespace DynabicBilling.Tests
{
    public class TestsHelper
    {
        private readonly BillingGateway _gateway;

        public TestsHelper(BillingGateway gateway)
        {
            _gateway = gateway;
        }

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

        public CustomerResponse AddCustomer(string subdomain)
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

        public SiteResponse AddSite()
        {
            // Remove test site if it exists.
            var subDomain = "demoSubdomain";
            try
            {
                var site = _gateway.Sites.GetSiteBySubdomain(subDomain);
                if (site != null)
                {
                    _gateway.Sites.DeleteSite(site.Id.ToString());
                }
            }
            catch
            {
            }

            var newSite = new SiteRequest
            {
                IsTestMode = true,
                Name = "Name",
                Subdomain = subDomain,
            };
            return _gateway.Sites.AddSite(newSite);
        }

        public void DeleteSite(int id)
        {
            _gateway.Sites.DeleteSite(id.ToString());
        }

        public ProductFamilyResponse AddProductFamily(int siteId)
        {
            var prodFamily = new ProductFamilyRequest
            {
                SiteId = siteId,
                Name = Guid.NewGuid().ToString("N"),
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

        public SubscriptionResponse AddTestSubscription(SiteResponse site)
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

        public void DeleteSubscriptionData(SubscriptionResponse subscription)
        {
            _gateway.Customer.DeleteCreditCard(subscription.CustomerId.ToString(), subscription.CreditCardId.ToString());
            _gateway.Customer.DeleteCustomer(subscription.CustomerId.ToString());
        }

    }
}
