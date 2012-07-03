using System;
using System.IO;
using System.Runtime.Serialization;
using DynabicBilling.Classes;
using DynabicBilling.RestApiDataContract;
using NUnit.Framework;

namespace DynabicBilling.Tests
{
    public class SubscriptionServiceTests : AssertionHelper
    {
        private BillingGateway _gateway = null;
        private TestsHelper _testsHelper;

        [SetUp]
        public void Init()
        {
#if DEBUG
            _gateway = new BillingGateway(BillingEnvironment.DEVELOPMENT, Constants.PUBLIC_KEY, Constants.PRIVATE_KEY);
            //_gateway = new BillingGateway(BillingEnvironment.DEVELOPMENT, "5bfcf4378b994d328f1c", "4d63986979214ace87ce");
            //_gateway = new BillingGateway(BillingEnvironment.DEVELOPMENT, "37fd73add1d04ffcbfef", "422795b447204bf78a38");
            //_gateway = new BillingGateway(BillingEnvironment.PRODUCTION, "e7f24ca623a74812b36e", "3e038ba976d6436b91d5");
            //_gateway = new BillingGateway(BillingEnvironment.DEVELOPMENT, "47362a63e3a540219996", "3a09eb4cbdd1494cafdf");
#else
            _gateway = new BillingGateway(BillingEnvironment.QA, Constants.PUBLIC_KEY, Constants.PRIVATE_KEY);
#endif
            _testsHelper = new TestsHelper(_gateway);
        }

        //[Test]
        public void __GetSubscription()
        {
            var subscr = _gateway.Subscription.GetSubscriptionsOfCustomerByReferenceId("CodePorting", "41302103");
            Assert.IsNotNull(subscr);
        }

        //[Test]
        public void __UpdateSubscription()
        {
            var sr = new DataContractSerializer(typeof(SubscriptionRequest));
            var request = sr.ReadObject(File.OpenRead(@"d:\request.xml")) as SubscriptionRequest;
            var updatedSubscription = _gateway.Subscription.UpdateSubscription("groupdocs", "1372", request);
            Assert.IsNotNull(updatedSubscription);
        }

        //[Test]
        public void __UpgradeDowngradeSubscriptionProduct()
        {
            /*var subscription = (SubscriptionRequest)_gateway.Subscription.GetSubscription("1374");
            subscription.CreditCard = new CreditCardRequest
                {
                    Cvv = "123",
                    ExpirationDate = DateTime.Now.AddYears(2),
                    FirstNameOnCard = "1",
                    LastNameOnCard = "1",
                    Number = "4012888888881881",
                };
            var updateResult = _gateway.Subscription.UpdateSubscription("banckle", "1374", subscription);*/
            _gateway.Subscription.UpgradeDowngradeSubscriptionProduct("1374", "1579", "true", "true");
            //Assert.AreEqual("Success", result.Result);
        }


        [Test]
        public void GetSubscription()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var subscr = _gateway.Subscription.GetSubscription(subscription.Id.ToString());
                    Assert.IsNotNull(subscr);
                }
                finally
                {
                    _testsHelper.DeleteSubscriptionData(subscription);
                }
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void GetSubscriptionsOfCustomer()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var subscriptions = _gateway.Subscription.GetSubscriptionsOfCustomer(subscription.CustomerId.ToString());
                    Assert.IsNotNull(subscriptions);
                }
                finally
                {
                    _testsHelper.DeleteSubscriptionData(subscription);
                }
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void GetSubscriptions()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var subscriptions = _gateway.Subscription.GetSubscriptions(site.Subdomain);
                    Assert.IsNotNull(subscriptions);
                }
                finally
                {
                    _testsHelper.DeleteSubscriptionData(subscription);
                }
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void GetSubscriptionsForStatus()
        {
            var site = _testsHelper.AddSite();
            Assert.IsNotNull(site);
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var subscriptions = _gateway.Subscription.GetSubscriptionsForStatus(site.Subdomain, "Active");
                    Assert.IsNotNull(subscriptions);
                }
                finally
                {
                    _testsHelper.DeleteSubscriptionData(subscription);
                }
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        //[Test]
        public void __AddSubscription2()
        {
            var subscriptionRequest = new SubscriptionRequest()
            {
                BillingAddressId = 30490,
                CreditCardId = 2195,
                Currency = new Currency { Code = "USD", },
                CustomerId = 2405,
                ProductPricingPlanId = 6802,
                ProductId = 6029,
                //Status = SubscriptionStatus.Trialing,
            };
            var addSubscriptionResponse = _gateway.Subscription.AddSubscription("banckle", subscriptionRequest);
            Assert.IsNotNull(addSubscriptionResponse);
        }

        //[Test]
        public void __AddSubscription()
        {
            //var subscriptionItems = new SubscriptionItemRequestList();
            //subscriptionItems.Add(new SubscriptionItemRequest
            //{
            //    ProductItemId = 943,
            //    Quantity = 5,
            //    UpdateDescription = "Add new subscription",
            //});

            var subscriptionRequest = new SubscriptionRequest()
            {
                BillingAddress = new AddressRequest()
                    {
                        Address1 = "St. King",
                        Address2 = "George Street",
                        City = "London",
                        Company = "TEST & Co.",
                        Country = "UK",
                        Email = "test_email@google.com",
                        FaxNumber = "1111111111",
                        FirstName = "John",
                        LastName = "Wallace",
                        PhoneNumber = "3333333333",
                        StateProvince = "London",
                        ZipPostalCode = "2222"
                    },
                CreditCard = new CreditCardRequest()
                    {
                        Cvv = "123",
                        ExpirationDate = DateTime.Now.AddYears(2),
                        FirstNameOnCard = "John B.",
                        LastNameOnCard = "Wallace",
                        Number = "1111111111111111"
                    },
                Currency = new Currency()
                    {
                        Code = "USD",
                        Id = 1,
                        Name = "US Dollars",
                    },
                Customer = new CustomerRequest()
                    {
                        Company = "TEST & Co.",
                        Email = "test_email@google.com",
                        FirstName = "John",
                        IsShippingAddressEqualToBilling = true,
                        LastName = "Wallace",
                        Phone = "3333333333",
                        ReferenceId = "123456",
                    },
                NextProduct = null,
                NextProductPricingPlan = null,
                ProductId = 458,
                ProductPricingPlanId = 356,
                StartDate = DateTime.Now,
                //SubscriptionItems = subscriptionItems,
            };
            //var addSubscriptionResponse = _gateway.Subscription.AddSubscription("groupdocs", subscriptionRequest);
            var addSubscriptionResponse = _gateway.Subscription.AddSubscription("banckle", subscriptionRequest);
            Assert.IsNotNull(addSubscriptionResponse);
        }

        [Test]
        public void AddAndDeleteSubscription()
        {
            var site = _testsHelper.AddSite();
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                _testsHelper.DeleteSubscriptionData(subscription);
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void UpdateSubscription()
        {
            var site = _testsHelper.AddSite();
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var updatedSubscription = _gateway.Subscription.UpdateSubscription(site.Subdomain, subscription.Id.ToString(), subscription);
                    Assert.IsNotNull(updatedSubscription);
                }
                finally
                {
                    _testsHelper.DeleteSubscriptionData(subscription);
                }
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void GetAddress()
        {
            var site = _testsHelper.AddSite();
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var address = _gateway.Subscription.GetAddress(subscription.Id.ToString());
                    Assert.IsNotNull(address);
                    Assert.AreEqual(subscription.BillingAddressId, address.Id);
                }
                finally
                {
                    _testsHelper.DeleteSubscriptionData(subscription);
                }
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void GetCustomersForProduct()
        {
            var site = _testsHelper.AddSite();
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var customers = _gateway.Subscription.GetCustomersForProduct(subscription.Id.ToString());
                    Assert.IsNotNull(customers);
                    Assert.AreEqual(1, customers.Count);
                    Assert.AreEqual(subscription.CustomerId, customers[0].Id);
                }
                finally
                {
                    _testsHelper.DeleteSubscriptionData(subscription);
                }
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        #region Operations

        [Test]
        public void AddChargeToSubscription()
        {
            var site = _testsHelper.AddSite();
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var charge = new ChargeRequest
                    {
                        Amount = 100.53m,
                        Memo = "test charge"
                    };

                    TransactionResponse tr = _gateway.Subscription.AddChargeToSubscription(subscription.Id.ToString(), charge);
                    Assert.IsNotNull(tr);
                    Assert.AreEqual(charge.Amount, tr.Amount);
                }
                finally
                {
                    _testsHelper.DeleteSubscriptionData(subscription);
                }
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void Refund()
        {
            var site = _testsHelper.AddSite();
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var charge = new ChargeRequest
                    {
                        Amount = 100.50m,
                        Memo = "test charge"
                    };
                    _gateway.Subscription.AddChargeToSubscription(subscription.Id.ToString(), charge);

                    var transactions = _gateway.Transaction.GetTransactionsForSubscription(subscription.Id.ToString());
                    Assert.IsNotNull(transactions);
                    Assert.Greater(transactions.Count, 0);

                    charge = new ChargeRequest
                    {
                        Amount = 50.25m,
                        Memo = "test charge"
                    };

                    TransactionResponse tr = _gateway.Subscription.Refund(subscription.Id.ToString(), charge, transactions[0].Id.ToString());
                    Assert.IsNotNull(tr);
                    Assert.AreEqual(charge.Amount, tr.Amount);
                }
                finally
                {
                    _testsHelper.DeleteSubscriptionData(subscription);
                }
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void AdjustSubscriptionBalance()
        {
            var site = _testsHelper.AddSite();
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var charge = new ChargeRequest
                    {
                        Amount = 0.50m,
                        Memo = "test charge"
                    };
                    _gateway.Subscription.AdjustSubscriptionBalance(subscription.Id.ToString(), "false", charge);
                }
                finally
                {
                    _testsHelper.DeleteSubscriptionData(subscription);
                }
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void ChangeSubscriptionProduct()
        {
            var site = _testsHelper.AddSite();
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var product = _gateway.Products.GetProductById(subscription.ProductId.ToString());
                    Assert.IsNotNull(product);

                    _gateway.Subscription.ChangeSubscriptionProduct(subscription.Id.ToString(), product.PricingPlans[0].Id.ToString());
                }
                finally
                {
                    _testsHelper.DeleteSubscriptionData(subscription);
                }
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void UpgradeDowngradeSubscriptionProduct()
        {
            var site = _testsHelper.AddSite();
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var product = _gateway.Products.GetProductById(subscription.ProductId.ToString());
                    Assert.IsNotNull(product);

                    var secondProduct = _testsHelper.AddProductToFamily(product.FamilyId);
                    Assert.IsNotNull(secondProduct);

                    //var result = 
                    _gateway.Subscription.UpgradeDowngradeSubscriptionProduct(subscription.Id.ToString(), secondProduct.PricingPlans[0].Id.ToString(), "true", "true");
                    //Assert.AreEqual("Success", result.Result);
                }
                finally
                {
                    _testsHelper.DeleteSubscriptionData(subscription);
                }
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void CancelSubscription()
        {
            var site = _testsHelper.AddSite();
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var request = new CancellationRequest
                    {
                        IsCancelledAtEndOfPeriod = false,
                        CancelationDetails = "detaild",
                    };

                    _gateway.Subscription.CancelSubscription(subscription.Id.ToString(), request);
                }
                finally
                {
                    _testsHelper.DeleteSubscriptionData(subscription);
                }
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void ReactivateSubscription()
        {
            var site = _testsHelper.AddSite();
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    _gateway.Subscription.ReactivateSubscription(subscription.Id.ToString());
                }
                finally
                {
                    _testsHelper.DeleteSubscriptionData(subscription);
                }
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        #endregion Operations

        #region Items

        [Test]
        public void AddSubscriptionItems()
        {
            var site = _testsHelper.AddSite();
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var product = _gateway.Products.GetProductById(subscription.ProductId.ToString());

                    var request = new SubscriptionItemRequestList();
                    request.Add(new SubscriptionItemRequest
                    {
                        ProductItemId = product.PricingPlans[0].ProductItemsList[0].Id,
                        Quantity = 100,
                        SubscriptionId = subscription.Id,
                    });

                    _gateway.Subscription.AddSubscriptionItems(request);
                }
                finally
                {
                    _testsHelper.DeleteSubscriptionData(subscription);
                }
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void UpdateSubscriptionItems()
        {
            var site = _testsHelper.AddSite();
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var product = _gateway.Products.GetProductById(subscription.ProductId.ToString());

                    var request = new SubscriptionItemRequestList();
                    request.Add(new SubscriptionItemRequest
                    {
                        ProductItemId = product.PricingPlans[0].ProductItemsList[0].Id,
                        Quantity = 100,
                        SubscriptionId = subscription.Id,
                    });

                    _gateway.Subscription.UpdateSubscriptionItems(request);
                }
                finally
                {
                    _testsHelper.DeleteSubscriptionData(subscription);
                }
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void GetSubscriptionItems()
        {
            var site = _testsHelper.AddSite();
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var product = _gateway.Products.GetProductById(subscription.ProductId.ToString());

                    var request = new SubscriptionItemRequestList();
                    request.Add(new SubscriptionItemRequest
                    {
                        ProductItemId = product.PricingPlans[0].ProductItemsList[0].Id,
                        Quantity = 100,
                        SubscriptionId = subscription.Id,
                    });

                    _gateway.Subscription.UpdateSubscriptionItems(request);

                    request = new SubscriptionItemRequestList();
                    request.Add(new SubscriptionItemRequest
                    {
                        ProductItemId = product.PricingPlans[0].ProductItemsList[0].Id,
                        Quantity = 50,
                        SubscriptionId = subscription.Id,
                    });

                    _gateway.Subscription.UpdateSubscriptionItems(request);

                    var components = _gateway.Subscription.GetSubscriptionItems(subscription.Id.ToString());
                    Assert.AreEqual(2, components.Count);
                    Assert.AreEqual(50, components[0].Quantity);
                    Assert.AreEqual(3, components[0].ChangesHistory.Count); // 2 from this method + 1 from Add item operation
                }
                finally
                {
                    _testsHelper.DeleteSubscriptionData(subscription);
                }
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        [Test]
        public void ResetSubscriptionMeteredItems()
        {
            var site = _testsHelper.AddSite();
            try
            {
                var subscription = _testsHelper.AddTestSubscription(site);
                Assert.IsNotNull(subscription);
                try
                {
                    var product = _gateway.Products.GetProductById(subscription.ProductId.ToString());

                    var request = new SubscriptionItemRequestList();
                    request.Add(new SubscriptionItemRequest
                    {
                        ProductItemId = product.PricingPlans[0].ProductItemsList[0].Id,
                        Quantity = 100,
                        SubscriptionId = subscription.Id,
                    });

                    _gateway.Subscription.UpdateSubscriptionItems(request);

                    request = new SubscriptionItemRequestList();
                    request.Add(new SubscriptionItemRequest
                    {
                        ProductItemId = product.PricingPlans[0].ProductItemsList[0].Id,
                        Quantity = 50,
                        SubscriptionId = subscription.Id,
                    });

                    _gateway.Subscription.UpdateSubscriptionItems(request);

                    var components = _gateway.Subscription.GetSubscriptionItems(subscription.Id.ToString());
                    Assert.AreEqual(2, components.Count);
                    Assert.AreEqual(3, components[0].ChangesHistory.Count); // 2 from this method + 1 from Add item operation

                    _gateway.Subscription.ResetSubscriptionMeteredItems(subscription.Id.ToString());

                    components = _gateway.Subscription.GetSubscriptionItems(subscription.Id.ToString());
                    Assert.AreEqual(2, components.Count);
                    Assert.AreEqual(0, components[0].Quantity);
                    Assert.AreEqual(0, components[0].ChangesHistory.Count);
                }
                finally
                {
                    _testsHelper.DeleteSubscriptionData(subscription);
                }
            }
            finally
            {
                _testsHelper.DeleteSite(site.Id);
            }
        }

        #endregion
        /*
        [Test]
        public void __AddSubscriptionItems()
        {
            var request = new SubscriptionItemRequestList();
            request.Add(new SubscriptionItemRequest
            {
                ProductItemId = 943,
                Quantity = 3,
                SubscriptionId = 2866,
            });
            request.Add(new SubscriptionItemRequest
            {
                ProductItemId = 2228,
                Quantity = 10000000654,
                SubscriptionId = 2866,
            });

            _gateway.Subscription.UpdateSubscriptionItems(request);
        }
        */
    }
}
