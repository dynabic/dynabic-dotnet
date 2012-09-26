#pragma warning disable 1591

using DynabicBilling.RestAPI.RestInterfaces;
using DynabicBilling.RestApiDataContract;
using DynabicPlatform.Classes;

namespace DynabicBilling
{
    /// <summary>
    /// Provides operations for creating, finding, updating, searching, and deleting subscriptions
    /// </summary>
    public class SubscriptionService : ISubscriptionsService
    {
        private CommunicationLayer _service;
        private readonly string _gatewayURL;

        protected internal SubscriptionService(CommunicationLayer service)
        {
            _service = service;
            _gatewayURL = service.Environment.GatewayURL + "/subscriptions";
        }

        /// <summary>
        /// Get Subscription
        /// </summary>
        /// <param name="subscriptionId">ID of the Subscription</param>
        /// <param name="format">Format of the Response</param>
        /// <returns></returns>
        public SubscriptionResponse GetSubscription(string subscriptionId, string format = ContentFormat.XML)
        {
            return _service.Get<SubscriptionResponse>(string.Format("{0}/{1}.{2}", _gatewayURL, subscriptionId, format));
        }

        /// <summary>
        /// Get All Subscriptions for a Customer
        /// </summary>
        /// <param name="customerId">ID of the Customer</param>
        /// <param name="format">Format of the Response</param>
        /// <returns>Subscription List</returns>
        public SubscriptionsList GetSubscriptionsOfCustomer(string customerId, string format = ContentFormat.XML)
        {
            return _service.Get<SubscriptionsList>(string.Format("{0}/{1}/subscriptions.{2}", _gatewayURL, customerId, format));
        }

        /// <summary>
        /// Gets all Subscriptions for a Customer
        /// </summary>
        /// <param name="siteSubdomain">Subdomain of the Site for which the Customer is registered</param>
        /// <param name="customerReferenceId">ID of the Customer's ReferenceID which is subscribed to the Subscription. </param>
        /// <param name="format">The format used for the data transfer (XML or JSON).</param>
        /// <returns></returns>
        public SubscriptionsList GetSubscriptionsOfCustomerByReferenceId(string siteSubdomain, string customerReferenceId, string format = "xml")
        {
            return _service.Get<SubscriptionsList>(string.Format("{0}/{1}/{2}/subscriptions.{3}", _gatewayURL, siteSubdomain, customerReferenceId, format));
        }

        ///<summary>        
        ///Get All Subscriptions
        ///</summary>
        ///<param name="siteSubdomain"> Name of the site </param>
        ///<param name="format">Format of the Response</param>
        /// <param name="pageNumber">The page number (Optional parameter, if is specified should be equal or greater than 1).</param>
        /// <param name="pageSize">Size of the page (Optional parameter, if is specified should be equal or greater than 1).</param>
        ///<returns>Subscription List</returns>
        public SubscriptionsList GetSubscriptions(string siteSubdomain, string format = ContentFormat.XML, string pageNumber = null, string pageSize = null)
        {
            return _service.Get<SubscriptionsList>(string.Format("{0}/bysite/{1}.{2}?pageNumber={3}&pageSize={4}", _gatewayURL, siteSubdomain, format, pageNumber, pageSize));
        }

        /// <summary>
        /// Get Subscriptions with specified Status
        /// </summary>
        /// <param name="siteSubdomain">Name of the site</param>
        /// <param name="status">Subscription Status</param>
        /// <param name="format">Format of the Response</param>
        /// <param name="pageNumber">The page number (Optional parameter, if is specified should be equal or greater than 1).</param>
        /// <param name="pageSize">Size of the page (Optional parameter, if is specified should be equal or greater than 1).</param>
        /// <returns>Subscrition List</returns>
        public SubscriptionsList GetSubscriptionsForStatus(string siteSubdomain, string status, string format = ContentFormat.XML, string pageNumber = null, string pageSize = null)
        {
            return _service.Get<SubscriptionsList>(string.Format("{0}/{1}/status={2}.{3}?pageNumber={4}&pageSize={5}", _gatewayURL, siteSubdomain, status, format, pageNumber, pageSize));
        }

        /// <summary>
        /// Adds a new Subscription
        /// </summary>
        /// <param name="siteSubdomain"> The subdomain of the Site </param>
        /// <param name="newSubscription"> The new Subscription to be added, as a SubscriptionRequest object </param>
        /// <param name="format"> The format of the Response </param>
        /// <returns> A SubscriptionResponse object corresponding to the newly-inserted Subscription </returns>
        public SubscriptionResponse AddSubscription(string siteSubdomain, SubscriptionRequest newSubscription, string format = ContentFormat.XML)
        {
            return _service.Post<SubscriptionRequest, SubscriptionResponse>(string.Format("{0}/{1}.{2}", _gatewayURL, siteSubdomain, format), newSubscription);
        }

        /// <summary>
        /// Updates a Subscription
        /// </summary>
        /// <param name="siteSubdomain">The subdomain of the Site</param>
        /// <param name="subscriptionId">The subscription id.</param>
        /// <param name="newSubscription">The Subscription to be updated, as a SubscriptionRequest object</param>
        /// <param name="format">The format of the Response</param>
        /// <returns>
        /// A SubscriptionResponse object that corresponds to the newly-updated Subscription
        /// </returns>
        public SubscriptionResponse UpdateSubscription(string siteSubdomain, string subscriptionId, SubscriptionRequest newSubscription, string format = ContentFormat.XML)
        {
            return _service.Put<SubscriptionRequest, SubscriptionResponse>(string.Format("{0}/{1}/{2}.{3}", _gatewayURL, siteSubdomain, subscriptionId, format), newSubscription);
        }

        /// <summary>
        /// Deletes a Subscription
        /// </summary>
        /// <param name="subscriptionId"> The Id of the Subscription </param>
        public void DeleteSubscription(string subscriptionId)
        {
            _service.Delete(string.Format("{0}/{1}", _gatewayURL, subscriptionId));
        }

        #region BillingAddresses

        /// <summary>
        /// Gets a Billing Address from a Subscription
        /// </summary>
        /// <param name="subscriptionId">ID of Subscription</param>
        /// <param name="format">Format of the response: XML or JSON</param>
        /// <returns></returns>
        public AddressResponse GetAddress(string subscriptionId, string format = ContentFormat.XML)
        {
            return _service.Get<AddressResponse>(string.Format("{0}/{1}/billing-address.{2}", _gatewayURL, subscriptionId, format));
        }

        #endregion BillingAddresses

        #region Customers

        /// <summary>
        /// Gets all customers for a product from a given Subscription
        /// </summary>
        /// <param name="subscriptionId">ID of the Subscription</param>
        /// <param name="format">Format of the response</param>
        /// <returns>A List of Customers</returns>
        public CustomersList GetCustomersForProduct(string subscriptionId, string format = ContentFormat.XML)
        {
            return _service.Get<CustomersList>(string.Format("{0}/{1}/customers.{2}", _gatewayURL, subscriptionId, format));
        }

        #endregion Customers

        #region Operations

        /// <summary>
        /// Adding a charge to a subscription causes the customer's credit card to be charged immediately
        /// for the amount you enter.
        /// If the charging of the credit card fails, we'll let you know and the customer's balance will not be affected.
        /// (But, you will be able to see a failed payment in the history).
        /// Note: This will not use any existing balance credits.
        /// </summary>
        public TransactionResponse AddChargeToSubscription(string subscriptionId, ChargeRequest charge)
        {
            return _service.Put<ChargeRequest, TransactionResponse>(string.Format("{0}/charge/{1}", _gatewayURL, subscriptionId), charge);
        }

        /// <summary>
        /// Refunds a transaction
        /// </summary>
        public TransactionResponse Refund(string subscriptionId, ChargeRequest charge, string transactionId)
        {
            return _service.Put<ChargeRequest, TransactionResponse>(string.Format("{0}/refund/{1}/{2}", _gatewayURL, subscriptionId, transactionId), charge);
        }

        /// <summary>
        /// Adding an adjustment to a subscription will either increase or decrease the balance of the subscription.
        /// The customer's credit card will not be affected.
        /// </summary>
        /// <param name="subscriptionId">The subscription id.</param>
        /// <param name="isAmountAbsolute">if true then the subscription balance will be set to the amount passed as parameter
        /// if false then the subscription balance will be increased or decreased with the given amount(positive or negative)</param>
        /// <param name="charge">The charge.</param>
        public void AdjustSubscriptionBalance(string subscriptionId, string isAmountAbsolute, ChargeRequest charge)
        {
            _service.Put<ChargeRequest>(string.Format("{0}/adjustbalance/{1}/{2}", _gatewayURL, subscriptionId, isAmountAbsolute), charge);
        }

        /// <summary>
        /// Changing the product has the following effects:
        /// No proration is done - your customer is not charged or credited at this time (if you wish to have proration,
        /// then perform an upgrade/downgrade migration)
        /// The selected product immediately becomes the current product listed in the Billing API and UI
        /// The period will not be affected
        /// The new product's charges will be assessed at the start of the next period
        /// </summary>
        /// <param name="subscriptionId">The Id of the Subscription whose Product needs to be changed</param>
        /// <param name="newProductPricingPlanId">The ProductPricingPlanId of the new Product for the Subscription</param>
        /// <param name="setNextBillingToNow">True or false; if 'true', the subscription will be assessed and its billing cycle will start now</param>
        /// <param name="format"></param>
        /// <returns></returns>
        public ServiceResult ChangeSubscriptionProduct(string subscriptionId, string newProductPricingPlanId, string setNextBillingToNow = "false", string format = ContentFormat.XML)
        {
            return _service.Put<ServiceResult>(string.Format("{0}/changeproduct/{1}/{2}/{3}.{4}", _gatewayURL, subscriptionId, newProductPricingPlanId, setNextBillingToNow, format));
        }

        /// <summary>
        /// An upgrade/downgrade "migration" allows you to move a subscriber to a new product using basic proration rules.
        /// If you prefer to move a subscriber to a new product without performing any proration, you may simply change
        /// the product for this subscription.
        /// Upgrading/Downgrading will have the following effects:
        /// A prorated credit of approximately ˆx.xx will be applied for the current product
        /// A charge for the full amount of the new product will be applied
        /// The period start date will be reset to today's date
        /// </summary>
        public ServiceResult UpgradeDowngradeSubscriptionProduct(string subscriptionId, string newProductPricingPlanId, string includeTrial, string includeUpfrontCharge, string format = ContentFormat.XML)
        {
            return _service.Put<ServiceResult>(string.Format("{0}/upgradedowngrade/{1}/{2}/{3}/{4}.{5}", _gatewayURL, subscriptionId, newProductPricingPlanId, includeTrial, includeUpfrontCharge, format));
        }

        /// <summary>
        /// Cancels a subscription. The effects are:
        /// The customer's credit card will no longer be charged after cancellation takes effect
        /// Cancellation takes effect immediately if you set isCancelledAtEndOfPeriod = false
        /// Cancellation takes effect at the subscription next assesment if you set isCancelledAtEndOfPeriod = true
        /// No refunds or prorations will be made.
        /// No email is sent to the customer.
        /// Canceled subscription can be reactivated at a later date
        /// </summary>
        /// <param name="subscriptionId">The Id of the Subscription</param>
        /// <param name="request">The request.</param>
        /// <param name="format"></param>
        /// <returns></returns>
        public ServiceResult CancelSubscription(string subscriptionId, CancellationRequest request, string format = ContentFormat.XML)
        {
            return _service.Put<CancellationRequest, ServiceResult>(string.Format("{0}/cancel/{1}.{2}", _gatewayURL, subscriptionId, format), request);
        }

        /// <summary>
        /// Reactivates a cancelled subscription
        /// This subscription will be immediately activated.
        /// The customer will be charged ˆ22.00 for (Anunturi) Anunturi premium
        /// The customer's billing date is reset to today.
        /// No email is sent to your customer.
        /// There is no "undo"
        /// </summary>
        public ServiceResult ReactivateSubscription(string subscriptionId)
        {
            return _service.Put<ServiceResult>(string.Format("{0}/reactivate/{1}", _gatewayURL, subscriptionId));
        }

        #endregion Operations

        #region Items

        /// <summary>
        /// Adds the subscription items.
        /// </summary>
        /// <param name="request">The request.</param>
        public void AddSubscriptionItems(SubscriptionItemRequestList request)
        {
            _service.Post<SubscriptionItemRequestList>(string.Format("{0}/additems", _gatewayURL), request);
        }

        /// <summary>
        /// Updates the subscription item.
        /// </summary>
        /// <param name="request">The request.</param>
        public void UpdateSubscriptionItems(SubscriptionItemRequestList request)
        {
            _service.Put<SubscriptionItemRequestList>(string.Format("{0}/updateitems", _gatewayURL), request);
        }

        /// <summary>
        /// Gets the subscription items.
        /// </summary>
        /// <param name="subscriptionId">The subscription id.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        public SubscriptionItemResponseList GetSubscriptionItems(string subscriptionId, string format = "xml")
        {
            return _service.Get<SubscriptionItemResponseList>(string.Format("{0}/items/{1}.{2}", _gatewayURL, subscriptionId, format));
        }

        /// <summary>
        /// Resets the values of the Metered Items
        /// </summary>
        /// <param name="subscriptionId">The subscription id.</param>
        public void ResetSubscriptionMeteredItems(string subscriptionId)
        {
            _service.Delete(string.Format("{0}/resetmetered/{1}", _gatewayURL, subscriptionId));
        }

        #endregion
    }
}
