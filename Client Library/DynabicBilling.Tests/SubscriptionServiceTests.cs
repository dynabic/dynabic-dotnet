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
        private TestDataValues _testData;

        [SetUp]
        public void Init()
        {
#if DEBUG
            _gateway = new BillingGateway(BillingEnvironment.DEVELOPMENT, Constants.PUBLIC_KEY, Constants.PRIVATE_KEY);
#else
            _gateway = new BillingGateway(BillingEnvironment.QA, Constants.PUBLIC_KEY, Constants.PRIVATE_KEY);
#endif
            _testsHelper = new TestsHelper(_gateway);
            _testData = _testsHelper.PrepareSubscriptionsTestData();
        }

        [TearDown]
        public void Cleanup()
        {
            _testsHelper.CleanupTestData();
        }

        [Test]
        public void GetSubscription()
        {
            var subscr = _gateway.Subscription.GetSubscription(_testData.SubscriptionId.ToString());
            Assert.IsNotNull(subscr);
        }

        [Test]
        public void GetSubscriptionsOfCustomer()
        {
            var subscriptions = _gateway.Subscription.GetSubscriptionsOfCustomer(_testData.CustomerId.ToString());
            Assert.IsNotNull(subscriptions);
        }

        [Test]
        public void GetSubscriptions()
        {
            var subscriptions = _gateway.Subscription.GetSubscriptions(_testData.Subdomain);
            Assert.IsNotNull(subscriptions);
        }

        [Test]
        public void GetSubscriptionsForStatus()
        {
            var subscriptions = _gateway.Subscription.GetSubscriptionsForStatus(_testData.Subdomain, "Active");
            Assert.IsNotNull(subscriptions);
        }

        [Test]
        public void UpdateSubscription()
        {
            var subscription = _gateway.Subscription.GetSubscription(_testData.SubscriptionId.ToString());
            Assert.IsNotNull(subscription);

            subscription.CancellationDetails = "test update";

            var updatedSubscription = _gateway.Subscription.UpdateSubscription(_testData.Subdomain, subscription.Id.ToString(), subscription);
            Assert.IsNotNull(updatedSubscription);
            Assert.AreEqual(subscription.CancellationDetails, updatedSubscription.CancellationDetails);
        }

        [Test]
        public void GetAddress()
        {
            var subscription = _gateway.Subscription.GetSubscription(_testData.SubscriptionId.ToString());
            Assert.IsNotNull(subscription);

            var address = _gateway.Subscription.GetAddress(_testData.SubscriptionId.ToString());
            Assert.IsNotNull(address);
            Assert.AreEqual(subscription.BillingAddressId, address.Id);
        }

        [Test]
        public void GetCustomersForProduct()
        {
            var subscription = _gateway.Subscription.GetSubscription(_testData.SubscriptionId.ToString());
            Assert.IsNotNull(subscription);

            var customers = _gateway.Subscription.GetCustomersForProduct(subscription.Id.ToString());
            Assert.IsNotNull(customers);
            Assert.AreEqual(1, customers.Count);
            Assert.AreEqual(subscription.CustomerId, customers[0].Id);
        }

        #region Operations

        [Test]
        public void AddChargeToSubscription()
        {
            var charge = new ChargeRequest
            {
                Amount = 100.53m,
                Memo = "test charge"
            };

            TransactionResponse tr = _gateway.Subscription.AddChargeToSubscription(_testData.SubscriptionId.ToString(), charge);
            Assert.IsNotNull(tr);
            Assert.AreEqual(charge.Amount, tr.Amount);
        }

        [Test]
        public void Refund()
        {
            var charge = new ChargeRequest
            {
                Amount = 100.50m,
                Memo = "test charge"
            };
            _gateway.Subscription.AddChargeToSubscription(_testData.SubscriptionId.ToString(), charge);

            var transactions = _gateway.Transaction.GetTransactionsForSubscription(_testData.SubscriptionId.ToString());
            Assert.IsNotNull(transactions);
            Assert.Greater(transactions.Count, 0);

            charge = new ChargeRequest
            {
                Amount = 50.25m,
                Memo = "test charge"
            };

            TransactionResponse tr = _gateway.Subscription.Refund(_testData.SubscriptionId.ToString(), charge, transactions[0].Id.ToString());
            Assert.IsNotNull(tr);
            Assert.AreEqual(charge.Amount, tr.Amount);
        }

        [Test]
        public void AdjustSubscriptionBalance()
        {
            var charge = new ChargeRequest
            {
                Amount = 0.50m,
                Memo = "test charge"
            };
            _gateway.Subscription.AdjustSubscriptionBalance(_testData.SubscriptionId.ToString(), "false", charge);
        }

        [Test]
        public void ChangeSubscriptionProduct()
        {
            var subscription = _gateway.Subscription.GetSubscription(_testData.SubscriptionId.ToString());
            Assert.IsNotNull(subscription);

            var product = _gateway.Products.GetProductById(subscription.ProductId.ToString());
            Assert.IsNotNull(product);

            _gateway.Subscription.ChangeSubscriptionProduct(subscription.Id.ToString(), product.PricingPlans[0].Id.ToString());
        }

        [Test]
        public void UpgradeDowngradeSubscriptionProduct()
        {
            var subscription = _gateway.Subscription.GetSubscription(_testData.SubscriptionId.ToString());
            Assert.IsNotNull(subscription);

            var product = _gateway.Products.GetProductById(subscription.ProductId.ToString());
            Assert.IsNotNull(product);

            var secondProduct = _testsHelper.AddProductToFamily(product.FamilyId);
            Assert.IsNotNull(secondProduct);

            _gateway.Subscription.UpgradeDowngradeSubscriptionProduct(subscription.Id.ToString(), secondProduct.PricingPlans[0].Id.ToString(), "true", "true");
        }

        [Test]
        public void CancelSubscription()
        {
            var request = new CancellationRequest
            {
                IsCancelledAtEndOfPeriod = false,
                CancelationDetails = "detaild",
            };

            _gateway.Subscription.CancelSubscription(_testData.SubscriptionId.ToString(), request);
        }

        [Test]
        public void ReactivateSubscription()
        {
            _gateway.Subscription.ReactivateSubscription(_testData.SubscriptionId.ToString());
        }

        #endregion Operations

        #region Items

        [Test]
        public void AddSubscriptionItems()
        {
            var subscription = _gateway.Subscription.GetSubscription(_testData.SubscriptionId.ToString());
            Assert.IsNotNull(subscription);

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

        [Test]
        public void UpdateSubscriptionItems()
        {
            var subscription = _gateway.Subscription.GetSubscription(_testData.SubscriptionId.ToString());
            Assert.IsNotNull(subscription);

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

        [Test]
        public void GetSubscriptionItems()
        {
            var subscription = _gateway.Subscription.GetSubscription(_testData.SubscriptionId.ToString());
            Assert.IsNotNull(subscription);

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

        [Test]
        public void ResetSubscriptionMeteredItems()
        {
            var subscription = _gateway.Subscription.GetSubscription(_testData.SubscriptionId.ToString());
            Assert.IsNotNull(subscription);

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

        #endregion
    }
}
