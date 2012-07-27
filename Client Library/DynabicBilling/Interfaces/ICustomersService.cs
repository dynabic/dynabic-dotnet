using DynabicBilling.RestApiDataContract;

namespace DynabicBilling.RestAPI.RestInterfaces
{
    public interface ICustomersService
    {
        #region Customers

        /// <summary>
        /// Adds a new Customer
        /// </summary>
        /// <param name="siteSubdomain"> The subdomain of the Site to which the Customer will belong </param>
        /// <param name="newCustomer"> A CustomerRequest object containing the data for Customer to be created </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <returns> A CustomerResponse object corresponding to the newly-created Customer record </returns>                      
        CustomerResponse AddCustomer(string siteSubdomain, CustomerRequest newCustomer, string format = "xml");

        /// <summary>
        /// Gets all Customers for a Site.
        /// </summary>
        /// <param name="siteSubdomain"> The subdomain of the Site for which to retrieve all Customers.</param>
        /// <param name="format"> The format used for the data transfer (XML or JSON).</param>
        /// <param name="pageNumber"> 
        /// Optional parameter to be used when a paged response is expected.
        /// Use it to specify the index of the page to be retrieved (this value has to be greater than or equal to 1).
        /// </param>
        /// <param name="pageSize">
        /// Optional parameter to be used when a paged response is expected.
        /// Use it to specify the number of records a page should contain (this value has to be greater than or equal to 1). 
        /// </param>
        /// <returns> A CustomersList object containing the requested Customer records </returns>
        CustomersList GetAllCustomers(string siteSubdomain, string format = "xml", string pageNumber = null, string pageSize = null);

        /// <summary>
        /// Gets a Customer by Id
        /// </summary>
        /// <param name="customerId"> The Id of the Customer </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <returns> A CustomerResponse object corresponding to specified Id </returns>        
        CustomerResponse GetCustomer(string customerId, string format = "xml");

        /// <summary>
        /// Gets a Customer by ReferenceId
        /// </summary>
        /// <param name="siteSubdomain"> The subdomain of the Site for which to retrieve the Customer.</param>
        /// <param name="referenceId"> The ReferenceId of the Customer </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <returns> A CustomerResponse object corresponding to the specified ReferenceId </returns>        
        CustomerResponse GetCustomerByReferenceId(string siteSubdomain, string referenceId, string format = "xml");

        /// <summary>
        /// Updates a Customer
        /// If The Customer's Address is not specified, one is created; otherwise, the existing one is updated
        /// </summary>
        /// <param name="updatedCustomer"> A CustomerRequest object containing the updated Customer record </param>
        /// <param name="customerId"> The Id of the Customer to be updated </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <returns> A CustomerResponse object corresponding to the updated Customer record </returns>
        CustomerResponse UpdateCustomer(CustomerRequest updatedCustomer, string customerId, string format = "xml");

        /// <summary>
        /// Deletes the customer.
        /// </summary>
        /// <param name="customerId">The customer id.</param>
        void DeleteCustomer(string customerId);

        #endregion

        #region CreditCards

        /// <summary>
        /// Adds a new Credit Card
        /// </summary>
        /// <param name="customerId"> The Id of the Credit Card owner (Customer) </param>
        /// <param name="newCreditCard"> A CreditCardRequest object containing the data for the Credit Card to be created </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <returns> A CreditCardResponse object corresponding to the newly-added CreditCard </returns>
        CreditCardResponse AddCreditCard(string customerId, CreditCardRequest newCreditCard, string format = "xml");

        /// <summary>
        /// Gets a Credit Card by Id
        /// </summary>
        /// <param name="creditCardId"> The Id of the Credit Card </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <returns> A CreditCardResponse object corresponding to the specified Id </returns>
        CreditCardResponse GetCreditCard(string creditCardId, string format = "xml");

        /// <summary>
        /// Gets all Credit Cards that correspond to a specific Customer
        /// </summary>
        /// <param name="customerId"> The If of the Customer whose Credit Cards are to be retrieved </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <returns> A CreditCardsList object containing all Credit Cards that correspond to the specified Customer </returns>
        CreditCardsList GetCreditCards(string customerId, string format = "xml");

        /// <summary>
        /// Gets all Credit Cards that correspond to a specific Customer with given ReferenceID
        /// </summary>
        /// <param name="customerReferenceId"> The reference Id of the Customer whose Credit Cards are to be retrieved </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <returns> A CreditCardsList object containing all Credit Cards that correspond to the specified Customer </returns>
        CreditCardsList GetCreditCardsByReferenceId(string siteSubdomain, string customerReferenceId, string format = "xml");


        /// <summary>
        /// Gets first Credit Card that belongs to a specific Customer with given Customer.Id
        /// </summary>
        /// <param name="customerId"> The Customer whose Credit Cards is to be retrieved </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <returns> A CreditCardResponse object containing first credit card that belongs to the specified Customer </returns>
        CreditCardResponse GetCustomersFirstCreditCard(string customerId, string format = "xml");

        /// <summary>
        /// Gets first Credit Card that belongs to a specific Customer with given Customer.ReferenceId
        /// </summary>
        /// <param name="customerReferenceId"> The Customer whose Credit Card is to be retrieved </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <returns> A CreditCardResponse object containing first credit card that belongs to the specified Customer </returns>
        CreditCardResponse GetFirstCreditCardForCustomerByReferenceId(string siteSubdomain, string customerReferenceId, string format = "xml");

        /// <summary>
        /// Updates a CreditCard
        /// </summary>
        /// <param name="customerId"> The Id of the Credit Card owner (Customer) </param>
        /// <param name="creditCardId"> The Id of the Credit Card to be updated </param>
        /// <param name="creditCard"> A CreditCardRequest object containing the updated CreditCard record </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <returns> A CreditCardResponse object corresponding to the updated CreditCard record </returns>
        CreditCardResponse UpdateCreditCard(string customerId, string creditCardId, CreditCardRequest creditCard, string format = "xml");

        /// <summary>
        /// Updates a CreditCard in the DB
        /// </summary>
        /// <param name="siteSubdomanin">Subdomain of the Site where the Customer is registered</param>
        /// <param name="customerReferenceId">The RefereceId of the Customer the CreditCard belongs to</param>
        /// <param name="creditCardId"> ID of the CreditCard to update</param>
        /// <param name="creditCard"> The CreditCard to be updated </param>
        /// <param name="format"> The data format used (xml/json) </param>
        /// <returns> A CreditCardResponse object representing the newly-updated CreditCard </returns>
        CreditCardResponse UpdateCreditCardByCustomerReferenceId(string siteSubdomanin, string customerReferenceId, string creditCardId, CreditCardRequest creditCard, string format = "xml");

        /// <summary>
        /// Deletes a CreditCard
        /// </summary>
        /// <param name="customerId"> The Id of the Credit Card owner (Customer) </param>
        /// <param name="creditCardId"> The Id of the CreditCard to be deleted </param>
        void DeleteCreditCard(string customerId, string creditCardId);

        #endregion

        #region BillingAddresses

        /// <summary>
        /// Gets all BillingAddresses for a Customer
        /// </summary>
        /// <param name="customerId"> The Id of the Customer </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <returns> An AddressList object containing all BillingAddresses for the specified Customer </returns>
        AddressList GetBillingAddresses(string customerId, string format = "xml");

        /// <summary>
        /// Gets a BillingAddress for a Customer
        /// </summary>
        /// <param name="customerId"> The Id of the Customer </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <returns> An AddressResponse object corresponding to the specified Id </returns>
        AddressResponse GetBillingAddress(string customerId, string format = "xml");

        /// <summary>
        /// Gets a BillingAddress for a Customer
        /// </summary>
        /// <param name="siteSubdomain">Subdomain of the Site where the Cusotmer is registered</param>
        /// <param name="customerReferenceId">Customer's referenceId</param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <returns> An AddressResponse object corresponding to the specified Id </returns>
        AddressResponse GetFirstBillingAddressForCustomerByReferenceId(string siteSubdomain, string customerReferenceId, string format = "xml");

        /// <summary>
        /// Adds a new BillingAddress for a Customer
        /// </summary>
        /// <param name="customerId"> The Id of the Customer </param>
        /// <param name="newBillingAddress"> An AddressRequest object containing the data for the BillingAddress to be created </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <returns> An AddressResponse object corresponding to the newly-added BillingAddress </returns>
        AddressResponse AddBillingAddress(string customerId, AddressRequest newBillingAddress, string format = "xml");

        /// <summary>
        /// Updates an existing BillingAddress for a Customer
        /// </summary>
        /// <param name="customerId"> The Id of the Customer </param>
        /// <param name="billingAddressId"> The Id of the BillingAddress </param>
        /// <param name="updatedBillingAddress"> An AddressRequest object containing the updated BillingAddress record </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <returns> An AddressResponse object corresponding to the newly-updated BillingAddress </returns>
        AddressResponse UpdateBillingAddress(string customerId, string billingAddressId, AddressRequest updatedBillingAddress, string format = "xml");

        /// <summary>
        /// Updates an existing BillingAddress for a Customer
        /// </summary>
        /// <param name="siteSubdomain"> Subdomain of the Site where the Customer is registered </param>
        /// <param name="customerReferenceId"> The Customer's ReferenceID </param>
        /// <param name="billingAddressId"> The Id of the BillingAddress </param>
        /// <param name="updatedBillingAddress"> An AddressRequest object containing the updated BillingAddress record </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <returns> An AddressResponse object corresponding to the newly-updated BillingAddress </returns>
        AddressResponse UpdateBillingAddressByCustomerReferenceId(string siteSubdomain, string customerReferenceId, string billingAddressId, AddressRequest updatedBillingAddress, string format = "xml");

        /// <summary>
        /// Deletes a BillingAddress for a Customer
        /// </summary>
        /// <param name="customerId"> The Id of the Customer </param>
        /// <param name="billingAddressId"> The Id of the BillingAddress </param>
        void DeleteBillingAddress(string customerId, string billingAddressId);

        #endregion

        #region Countries

        /// <summary>
        /// Gets all Countries
        /// </summary>
        /// <returns>Countries collection</returns>
        CountryList GetCountries(string format);

        /// <summary>
        /// Gets a Country by Id
        /// </summary>
        /// <returns>Country</returns>
        Country GetCountry(string countryId, string format);

        /// <summary>
        /// Gets a Country by ISO code (either 2 or 3 letter codes)
        /// </summary>
        /// <returns>Country</returns>
        Country GetCountryByCode(string countryIsoCode, string format);

        /// <summary>
        /// Gets a Contry by Name
        /// </summary>
        /// <param name="countryName"> The Name of the Country </param>
        /// <returns> A Country object corresponding to the requested Country </returns>
        Country GetCountryByName(string countryName, string format);

        #endregion Countries

        #region StateProvince

        /// <summary>
        /// Gets all StatesProvinces by Country Id
        /// </summary>
        /// <returns>StatesProvinces collection</returns>
        StateProvinceList GetStateProvinces(string countryId, string format);

        /// <summary>
        /// Gets all StatesProvinces by Country ISO code (2 letters)
        /// </summary>
        /// <returns>StatesProvinces collection</returns>
        StateProvinceList GetStateProvincesByCountryCode(string countryTwoLetterISOCode, string format);

        /// <summary>
        /// Gets a StateProvince by Id
        /// </summary>
        /// <returns>StateProvince</returns>
        StateProvince GetStateProvince(string stateProvinceId, string format);

        /// <summary>
        /// Gets a StateProvince by Name
        /// </summary>
        /// <param name="stateProvinceName"> The Name of the StateProvince </param>
        /// <returns> A StateProvince object corresponding to the specified Name </returns>
        StateProvince GetStateProvinceByName(string stateProvinceName, string format);

        #endregion StateProvince

        #region Currency

        /// <summary>
        /// Gets all Currencies
        /// </summary>
        /// <param name="format"> Data transfer format (XML/JSON) </param>
        /// <returns>CurrencyList</returns>
        CurrencyList GetCurrencies(string format);

        /// <summary>
        /// Gets a Currency by Id
        /// </summary>
        /// <param name="currencyId"> The Id of the Currency </param>
        /// <param name="format"> Data transfer format (XML/JSON) </param>
        /// <returns>Currency</returns>
        Currency GetCurrency(string currencyId, string format);

        /// <summary>
        /// Gets a Currency by Code
        /// </summary>
        /// <param name="currencyCode"> The Currency code (e.g. USD, EUR) </param>
        /// <param name="format"> Data transfer format (XML/JSON) </param>
        /// <returns>Currency</returns>
        Currency GetCurrencyByCode(string currencyCode, string format);

        /// <summary>
        /// Gets the currency for a Country
        /// </summary>
        /// <param name="countryThreeISOCode"> The 3-letter Country code </param>
        /// <param name="format"> Data transfer format (XML/JSON) </param>
        /// <returns>Currency</returns>
        Currency GetCurrencyForCountryThreeIsoCode(string countryThreeISOCode, string format);

        #endregion Currency
    }
}
