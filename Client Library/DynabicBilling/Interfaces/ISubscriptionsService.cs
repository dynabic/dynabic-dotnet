using DynabicBilling.RestApiDataContract;

namespace DynabicBilling.RestAPI.RestInterfaces
{
    public interface ISubscriptionsService
    {
        #region GET

        /// <summary>
        /// Gets a Subscription by Id
        /// </summary>
        /// <param name="subscriptionId">ID of the Subscription to be retrieved. </param>
        /// <param name="format">The format used for the data transfer (XML or JSON). </param>
        /// <returns>A SubscriptionResponse object corresponding to the specified SubscriptionId. </returns>
        SubscriptionResponse GetSubscription(string subscriptionId, string format = "xml");

        /// <summary>
        /// Gets all Subscriptions for a Customer
        /// </summary>
        /// <param name="customerId">ID of the Customer which is subscribed to the Subscription. </param>
        /// <param name="format">The format used for the data transfer (XML or JSON).</param>
        /// <returns>A SubscriptionsList object containing requested Subscription records. </returns>
        SubscriptionsList GetSubscriptionsOfCustomer(string customerId, string format = "xml");

        /// <summary>
        /// Gets all Subscriptions for a Customer
        /// </summary>
        /// <param name="siteSubdomain">Subdomain of the Site for which the Customer is registered</param>
        /// <param name="customerReferenceId">ID of the Customer's ReferenceID which is subscribed to the Subscription. </param>
        /// <param name="format">The format used for the data transfer (XML or JSON).</param>
        /// <returns>A SubscriptionsList object containing requested Subscription records. </returns>
        SubscriptionsList GetSubscriptionsOfCustomerByReferenceId(string siteSubdomain, string customerReferenceId, string format = "xml");

        ///<summary>        
        ///Gets all Subscriptions for a Site
        ///</summary>
        ///<param name="siteSubdomain">The subdomain of the Site to which the Subscription belongs to. </param>
        ///<param name="format">The format used for the data transfer (XML or JSON). </param>
        /// <param name="pageNumber">
        /// Optional parameter to be used when a paged response is expected.
        /// Use it to specify the index of the page to be retrieved (this value has to be greater than or equal to 1).
        /// </param>
        /// <param name="pageSize">
        /// Optional parameter to be used when a paged response is expected.
        /// Use it to specify the number of records a page should contain (this value has to be greater than or equal to 1).
        /// </param>
        ///<returns>A SubscriptionsList object containing requested Subscription records. </returns>
        SubscriptionsList GetSubscriptions(string siteSubdomain, string format = "xml", string pageNumber = null, string pageSize = null);

        /// <summary>
        /// Gets Subscriptions with specified Status
        /// </summary>
        /// <param name="siteSubdomain">The subdomain of the Site to which the Subscription belongs to. </param>
        /// <param name="status">Status of the Subscriptions that shell be retrieved</param>
        /// <param name="format">The format used for the data transfer (XML or JSON). </param>
        /// <param name="pageNumber">
        /// Optional parameter to be used when a paged response is expected.
        /// Use it to specify the index of the page to be retrieved (this value has to be greater than or equal to 1).
        /// </param>
        /// <param name="pageSize">
        /// Optional parameter to be used when a paged response is expected.
        /// Use it to specify the number of records a page should contain (this value has to be greater than or equal to 1).
        /// </param>
        /// <returns>A SubscriptionsList object containing requested Subscription records. </returns>
        SubscriptionsList GetSubscriptionsForStatus(string siteSubdomain, string status, string format = "xml", string pageNumber = null, string pageSize = null);

        #endregion GET

        #region POST

        /// <summary>
        /// Adds a new Subscription
        /// </summary>
        /// <param name="siteSubdomain">The site subdomain.</param>
        /// <param name="newSubscription">A SubscriptionRequest object containing the data for the Subscription to be created.</param>
        /// <param name="format">The format used for the data transfer (XML or JSON).</param>
        /// <returns>
        /// A SubscriptionResponse object corresponding to the newly-created Subscription
        /// </returns>
        SubscriptionResponse AddSubscription(string siteSubdomain, SubscriptionRequest newSubscription, string format = "xml");

        #endregion POST

        #region PUT

        /// <summary>
        /// Updates a Subscription
        /// </summary>
        /// <param name="siteSubdomain">The site subdomain.</param>
        /// <param name="subscriptionId">The subscription id.</param>
        /// <param name="newSubscription">A SubscriptionRequest object containing the Subscription record to be updated.</param>
        /// <param name="format">The format used for the data transfer (XML or JSON).</param>
        /// <returns>
        /// A SubscriptionResponse object that corresponds to the updated Subscription
        /// </returns>
        SubscriptionResponse UpdateSubscription(string siteSubdomain, string subscriptionId, SubscriptionRequest newSubscription, string format = "xml");

        #endregion PUT

        #region DELETE

        /// <summary>
        /// Deletes a Subscription
        /// </summary>
        /// <param name="subscriptionId"> The Id of the Subscription to be deleted. </param>
        void DeleteSubscription(string subscriptionId);

        #endregion DELETE

        #region BillingAddresses

        /// <summary>
        /// Gets a Billing Address from a Subscription
        /// </summary>
        /// <param name="subscriptionId">ID of the Subscription to which Billing Address is attached. </param>
        /// <param name="format">The format used for the data transfer (XML or JSON). </param>
        /// <returns>A AddressResponse object containing requested Address record. </returns>
        AddressResponse GetAddress(string subscriptionId, string format = "xml");

        #endregion

        #region Customers
        /// <summary>
        /// Gets all Customers that are subscribed for a Product from a specific Subscription
        /// </summary>
        /// <param name="subscriptionId">ID of the Subscription for which the Product belongs to. </param>
        /// <param name="format">The format used for the data transfer (XML or JSON). </param>
        /// <returns>A CustomersList object containing requested Customer records. </returns>
        CustomersList GetCustomersForProduct(string subscriptionId, string format = "xml");
        #endregion

        #region Operations

        /// <summary>
        /// Adding a charge to a subscription causes the customer's credit card to be charged immediately 
        /// for the amount you enter. 
        /// If the charging of the credit card fails, we'll let you know and the customer's balance will not be affected. 
        /// (But, you will be able to see a failed payment in the history).
        /// 
        /// Note: This will not use any existing balance credits.
        /// </summary>
        TransactionResponse AddChargeToSubscription(string subscriptionId, ChargeRequest charge);

        /// <summary>
        /// Refunds a transaction
        /// </summary>
        TransactionResponse Refund(string subscriptionId, ChargeRequest charge, string transactionId);

        /// <summary>
        /// Adding an adjustment to a subscription will either increase or decrease the balance of the subscription.
        /// The customer's credit card will not be affected.
        /// </summary>
        /// <param name="subscriptionId">The subscription id.</param>
        /// <param name="isAmountAbsolute">if true then the subscription balance will be set to the amount passed as parameter
        /// if false then the subscription balance will be increased or decreased with the given amount(positive or negative)</param>
        /// <param name="charge">The charge request.</param>
        void AdjustSubscriptionBalance(string subscriptionId, string isAmountAbsolute, ChargeRequest charge);

        /// <summary>
        /// Changing the product has the following effects:
        ///   No proration is done - your customer is not charged or credited at this time (if you wish to have proration, 
        ///   then perform an upgrade/downgrade migration)
        ///   The selected product immediately becomes the current product listed in the Billing API and UI
        ///   The period will not be affected
        ///   The new product's charges will be assessed at the start of the next period
        /// </summary>
        /// <param name="subscriptionId"> The Id of the Subscription whose Product needs to be changed </param>
        /// <param name="newProductPricingPlanId"> The ProductPricingPlanId of the new Product for the Subscription </param>
        void ChangeSubscriptionProduct(string subscriptionId, string newProductPricingPlanId);

        /// <summary>
        /// An upgrade/downgrade "migration" allows you to move a subscriber to a new product using basic proration rules. 
        /// If you prefer to move a subscriber to a new product without performing any proration, you may simply change 
        /// the product for this subscription. 
        /// Upgrading/Downgrading will have the following effects:
        /// A prorated credit of approximately €x.xx will be applied for the current product
        /// A charge for the full amount of the new product will be applied
        /// The period start date will be reset to today's date
        /// </summary>
        void UpgradeDowngradeSubscriptionProduct(string subscriptionId, string newProductPricingPlanId, string includeTrial, string includeUpfrontCharge/*, string format = "xml"*/);

        /// <summary>
        /// Cancels a subscription. The effects are:
        ///     The customer's credit card will no longer be charged after cancellation takes effect
        ///     Cancellation takes effect immediately if you set isCancelledAtEndOfPeriod = false
        ///     Cancellation takes effect at the subscription next assesment if you set isCancelledAtEndOfPeriod = true
        ///     No refunds or prorations will be made.
        ///     No email is sent to the customer.
        ///     Canceled subscription can be reactivated at a later date
        /// </summary>
        /// <param name="subscriptionId"> The Id of the Subscription </param>
        /// <param name="request">The cancellation request.</param>
        void CancelSubscription(string subscriptionId, CancellationRequest request);

        /// <summary>
        /// Reactivates a cancelled subscription
        ///     This subscription will be immediately activated.
        ///     The customer will be charged €22.00 for (Anunturi) Anunturi premium
        ///     The customer's billing date is reset to today.
        ///     No email is sent to your customer. 
        ///     There is no "undo"
        /// </summary>
        ServiceResult ReactivateSubscription(string subscriptionId);

        #endregion Operations

        #region Items

        /// <summary>
        /// Adds the subscription items.
        /// </summary>
        /// <param name="request">The request.</param>
        void AddSubscriptionItems(SubscriptionItemRequestList request);

        /// <summary>
        /// Updates the subscription items.
        /// </summary>
        /// <param name="request">The request.</param>
        void UpdateSubscriptionItems(SubscriptionItemRequestList request);

        /// <summary>
        /// Gets the subscription items.
        /// </summary>
        /// <param name="subscriptionId">The subscription id.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        SubscriptionItemResponseList GetSubscriptionItems(string subscriptionId, string format = "xml");

        /// <summary>
        /// Resets the values of the Metered Items
        /// </summary>
        /// <param name="subscriptionId">The subscription id.</param>
        void ResetSubscriptionMeteredItems(string subscriptionId);

        #endregion
    }
}
