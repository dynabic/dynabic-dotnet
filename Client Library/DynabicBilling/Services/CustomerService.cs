#pragma warning disable 1591

using DynabicBilling.RestAPI.RestInterfaces;
using DynabicBilling.RestApiDataContract;
using DynabicPlatform.Classes;
using DynabicPlatform.Exceptions;

namespace DynabicBilling
{
    /// <summary>
    /// Provides operations for creating, finding, updating, and deleting customers
    /// </summary>
    public class CustomerService : ICustomersService
    {
        private CommunicationLayer _service;
        private readonly string _gatewayURL;

        protected internal CustomerService(CommunicationLayer service)
        {
            _service = service;
            _gatewayURL = service.Environment.GatewayURL + "/customers";
        }

        #region Customers

        #region GET

        /// <summary>
        /// Get All Customers
        /// </summary>
        /// <param name="siteSubdomain">Name of the site</param>
        /// <param name="format">Format of the Response</param>
        /// <param name="pageNumber">The page number (Optional parameter, if is specified should be equal or greater than 1).</param>
        /// <param name="pageSize">Size of the page (Optional parameter, if is specified should be equal or greater than 1).</param>
        /// <returns>
        /// Customers List
        /// </returns>        
        public CustomersList GetAllCustomers(string siteSubdomain, string format = ContentFormat.XML, string pageNumber = null, string pageSize = null)
        {
            //return _service.Get<CustomersList>(string.Format("{0}/{1}.{2}", _gatewayURL, siteSubdomain, format));
            return _service.Get<CustomersList>(string.Format("{0}/{1}.{2}?pageNumber={3}&pageSize={4}", _gatewayURL, siteSubdomain, format, pageNumber, pageSize));
        }

        /// <summary>
        /// Get a Customer
        /// </summary>
        /// <param name="customerId">ID of the Customer</param>
        /// <param name="format">Format of the Response</param>
        /// <returns>CustomerResponseInfo</returns>        
        public CustomerResponse GetCustomer(string customerId, string format = ContentFormat.XML)
        {
            if (string.IsNullOrEmpty(customerId) || string.IsNullOrEmpty(format))
                throw new NotFoundException();

            return _service.Get<CustomerResponse>(string.Format("{0}/byid/{1}.{2}", _gatewayURL, customerId, format));
        }

        /// <summary>
        /// Gets a Customer by reference Id
        /// </summary>
        /// <param name="siteSubdomain"> The subdomain of the Site that the Customer belongs to </param>
        /// <param name="referenceId"> The Id of the Company whose Customer entry is to be retrieved </param>
        /// <param name="format"> Format of the Response </param>
        /// <returns> A CustomerResponse object corresponding to the requested Customer </returns>        
        public CustomerResponse GetCustomerByReferenceId(string siteSubdomain, string referenceId, string format = ContentFormat.XML)
        {
            return _service.Get<CustomerResponse>(string.Format("{0}/{1}/reference-id/{2}.{3}", _gatewayURL, siteSubdomain, referenceId, format));
        }

        #endregion GET

        #region POST

        /// <summary>
        /// Create and Add a new Customer
        /// </summary>
        /// <param name="siteSubdomain"> The name of the Site to which the Customer will belong </param>
        /// <param name="newCustomer">Reference to a new Customer to create</param>
        /// <param name="format">Format of the passed Customer (XML or JSON)</param>
        /// <returns>ID of the new Customer created, otherwise Error</returns>                      
        public CustomerResponse AddCustomer(string siteSubdomain, CustomerRequest newCustomer, string format = ContentFormat.XML)
        {
            return _service.Post<CustomerRequest, CustomerResponse>(string.Format("{0}/{1}.{2}", _gatewayURL, siteSubdomain, format), newCustomer);
        }

        #endregion POST

        #region PUT

        /// <summary>
        /// Updates info about a Customer. If The Customer's Address is not specified, one is created. Otherwise, it is updated
        /// </summary>
        /// <param name="updatedCustomer">Reference to a new info about the Customer</param>
        /// <param name="customerId">ID of the Customer to Update</param>
        /// <param name="format">Format of the Response</param>
        /// <returns></returns>        
        public CustomerResponse UpdateCustomer(CustomerRequest updatedCustomer, string customerId, string format = ContentFormat.XML)
        {
            return _service.Put<CustomerRequest, CustomerResponse>(string.Format("{0}/{1}.{2}", _gatewayURL, customerId, format), updatedCustomer);
        }

        #endregion PUT

        #region DELETE

        /// <summary>
        /// Deletes the customer.
        /// </summary>
        /// <param name="customerId">The customer id.</param>
        public void DeleteCustomer(string customerId)
        {
            _service.Delete(string.Format("{0}/{1}", _gatewayURL, customerId));
        }

        #endregion DELETE

        #endregion

        #region CreditCards

        #region GET

        /// <summary>
        /// Gets a Credit Card
        /// </summary>
        /// <param name="creditCardId"> The Id of the desired CreditCard </param>
        /// <param name="format"> The desired response format (xml/json) </param>
        /// <returns> A CreditCard object </returns>
        public CreditCardResponse GetCreditCard(string creditCardId, string format = ContentFormat.XML)
        {
            return _service.Get<CreditCardResponse>(string.Format("{0}/credit-card/{1}.{2}", _gatewayURL, creditCardId, format));
        }

        /// <summary>
        /// Gets all Credit Cards that correspond to a specific Customer
        /// </summary>
        /// <param name="customerId"> The Customer whose Credit Cards are to be retrieved </param>
        /// <param name="format"> The desired response format (xml/json) </param>
        /// <returns> A CreditCardsList object containing all credit cards that correspond to the specified Customer </returns>
        public CreditCardsList GetCreditCards(string customerId, string format = ContentFormat.XML)
        {
            return _service.Get<CreditCardsList>(string.Format("{0}/{1}/credit-cards.{2}", _gatewayURL, customerId, format));
        }

        /// <summary>
        /// Gets all Credit Cards that belong to a specific Customer with a specific ReferenceID
        /// </summary>
        /// <param name="customerReferenceId">Customer's ReferenceID</param>
        /// <param name="format"> The desired response format (xml/json) </param>
        /// <returns> A CreditCardsList object containing all credit cards that correspond to the specified Customer </returns>
        public CreditCardsList GetCreditCardsByReferenceId(string siteSubdomain, string customerReferenceId, string format = ContentFormat.XML)
        {
            return _service.Get<CreditCardsList>(string.Format("{0}/{1}/reference-id/{2}/credit-cards.{3}", _gatewayURL, siteSubdomain, customerReferenceId, format));
        }

        /// <summary>
        /// Gets first Credit Card that belongs to a specific Customer with given CustomerId
        /// </summary>
        /// <param name="customerId"> The Customer whose Credit Cards is to be retrieved </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <returns> A CreditCardResponse object containing first credit card that belongs to the specified Customer </returns>
        public CreditCardResponse GetCustomersFirstCreditCard(string customerId, string format = ContentFormat.XML)
        {
            return _service.Get<CreditCardResponse>(string.Format("{0}/{1}/credit-card.{2}", _gatewayURL, customerId, format));
        }

        /// <summary>
        /// Gets first Credit Card that belongs to a specific Customer with given ReferenceId
        /// </summary>
        /// <param name="customerReferenceId"> The Customer whose Credit Card is to be retrieved </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <returns> A CreditCardResponse object containing first credit card that belongs to the specified Customer </returns>
        public CreditCardResponse GetFirstCreditCardForCustomerByReferenceId(string siteSubdomain, string customerReferenceId, string format = ContentFormat.XML)
        {
            return _service.Get<CreditCardResponse>(string.Format("{0}/{1}/reference-id/{2}/credit-card.{3}", _gatewayURL, siteSubdomain, customerReferenceId, format));
        }

        #endregion GET

        #region POST

        /// <summary>
        /// Inserts a new Credit Card in the DB
        /// </summary>
        /// <param name="customerId"> The Id of the Customer the new CreditCard will belong to </param>
        /// <param name="newCreditCard"> The CreditCard to be iserted </param>
        /// <param name="format"> The desired data format (xml/json) </param>
        /// <returns> A CreditCardResponse representing the newly-added CreditCard </returns>
        public CreditCardResponse AddCreditCard(string customerId, CreditCardRequest newCreditCard, string format = ContentFormat.XML)
        {
            return _service.Post<CreditCardRequest, CreditCardResponse>(string.Format("{0}/{1}/credit-card.{2}", _gatewayURL, customerId, format), newCreditCard);
        }

        #endregion POST

        #region PUT

        /// <summary>
        /// Updates a CreditCard in the DB
        /// </summary>
        /// <param name="customerId"> The Id of the Customer the CreditCard belongs to </param>
        /// <param name="creditCardId"> ID of the CreditCard to update</param>
        /// <param name="creditCard"> The CreditCard to be updated </param>
        /// <param name="format"> The data format used (xml/json) </param>
        /// <returns> A CreditCardResponse object representing the newly-updated CreditCard </returns>
        public CreditCardResponse UpdateCreditCard(string customerId, string creditCardId, CreditCardRequest creditCard, string format = ContentFormat.XML)
        {
            return _service.Put<CreditCardRequest, CreditCardResponse>(string.Format("{0}/{1}/credit-card/{2}.{3}", _gatewayURL, customerId, creditCardId, format), creditCard);
        }

        /// <summary>
        /// Updates a CreditCard in the DB
        /// </summary>
        /// <param name="siteSubdomanin">Subdomain of the Site where the Customer is registered</param>
        /// <param name="customerReferenceId">The RefereceId of the Customer the CreditCard belongs to</param>
        /// <param name="creditCardId"> ID of the CreditCard to update</param>
        /// <param name="creditCard"> The CreditCard to be updated </param>
        /// <param name="format"> The data format used (xml/json) </param>
        /// <returns> A CreditCardResponse object representing the newly-updated CreditCard </returns>
        public CreditCardResponse UpdateCreditCardByCustomerReferenceId(string siteSubdomanin, string customerReferenceId, string creditCardId, CreditCardRequest creditCard, string format = ContentFormat.XML)
        {
            return _service.Put<CreditCardRequest, CreditCardResponse>(string.Format("{0}/{1}/reference-id/{2}/credit-card/{3}.{4}", _gatewayURL, siteSubdomanin, customerReferenceId, creditCardId, format), creditCard);
        }

        #endregion PUT

        #region DELETE

        // TODO: later on
        ///// <summary>
        ///// Deletes all CreditCards that belong to a specific Customer
        ///// </summary>
        ///// <param name="customerId"> The Id of the Customer whose CreditCards are to be deleted </param>
        ///// <param name="format"> The data format used (xml/json) </param>
        //void DeleteCreditCards(string customerId, string format= ContentFormat.XML);

        /// <summary>
        /// Deletes a CreditCard
        /// </summary>
        /// <param name="customerId"> The Id of the Customer the CreditCard belongs to </param>
        /// <param name="creditCardId"> The Id of the CreditCard to be deleted </param>
        public void DeleteCreditCard(string customerId, string creditCardId)
        {
            _service.Delete(string.Format("{0}/{1}/credit-card/{2}", _gatewayURL, customerId, creditCardId));
        }

        #endregion DELETE

        #endregion

        #region BillingAddresses

        #region GET

        /// <summary>
        /// Gets all BillingAddresses for Customer
        /// </summary>
        /// <param name="customerId"> The Id of the Customer </param>
        /// <param name="format"> The format of the Response </param>
        /// <returns> An AddressList object containing all BillingAddresses for the specified Customer </returns>
        public AddressList GetBillingAddresses(string customerId, string format = ContentFormat.XML)
        {
            return _service.Get<AddressList>(string.Format("{0}/{1}/billing-addresses.{2}", _gatewayURL, customerId, format));
        }

        /// <summary>
        /// Gets a BillingAddress for a Customer
        /// </summary>
        /// <param name="customerId"> The Id of the Customer </param>
        /// <param name="format"> The format of the Response </param>
        /// <returns> An AddressResponse object containing the requested BillingAddress </returns>
        public AddressResponse GetBillingAddress(string customerId, string format = ContentFormat.XML)
        {
            return _service.Get<AddressResponse>(string.Format("{0}/{1}/first-billing-address.{2}", _gatewayURL, customerId, format));
        }

        /// <summary>
        /// Gets a BillingAddress for a Customer
        /// </summary>
        /// <param name="siteSubdomain">Subdomain of the Site where the Cusotmer is registered</param>
        /// <param name="customerReferenceId">Customer's referenceId</param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <returns> An AddressResponse object corresponding to the specified Id </returns>
        public AddressResponse GetFirstBillingAddressForCustomerByReferenceId(string siteSubdomain, string customerReferenceId, string format = "xml")
        {
            return _service.Get<AddressResponse>(string.Format("{0}/{1}/reference-id/{2}/first-billing-address.{3}", _gatewayURL, siteSubdomain, customerReferenceId, format));
        }

        #endregion GET

        #region POST

        /// <summary>
        /// Adds a new BillingAddress for a Customer
        /// </summary>
        /// <param name="customerId"> The Id of the Customer </param>
        /// <param name="newBillingAddress"> The new BillingAddress </param>
        /// <param name="format"> The format of the Response </param>
        /// <returns> An AddressResponse object corresponding to the newly-inserted BillingAddress </returns>
        public AddressResponse AddBillingAddress(string customerId, AddressRequest newBillingAddress, string format = ContentFormat.XML)
        {
            return _service.Post<AddressRequest, AddressResponse>(string.Format("{0}/{1}/billing-address.{2}", _gatewayURL, customerId, format), newBillingAddress);
        }

        #endregion POST

        #region PUT

        /// <summary>
        /// Updates an existing BillingAddress for a Customer
        /// </summary>
        /// <param name="customerId"> The Id of the Customer </param>
        /// <param name="billingAddressId"> The Id of the BillingAddress </param>
        /// <param name="updatedBillingAddress"> The updated BillingAddress </param>
        /// <param name="format"> The format of the Response </param>
        /// <returns> An AddressResponse object corresponding to the newly-updated BillingAddress </returns>
        public AddressResponse UpdateBillingAddress(string customerId, string billingAddressId, AddressRequest updatedBillingAddress, string format = ContentFormat.XML)
        {
            return _service.Put<AddressRequest, AddressResponse>(string.Format("{0}/{1}/billing-address/{2}.{3}", _gatewayURL, customerId, billingAddressId, format), updatedBillingAddress);
        }

        /// <summary>
        /// Updates an existing BillingAddress for a Customer
        /// </summary>
        /// <param name="siteSubdomain"> Subdomain of the Site where the Customer is registered </param>
        /// <param name="customerReferenceId"> The Customer's ReferenceID </param>
        /// <param name="billingAddressId"> The Id of the BillingAddress </param>
        /// <param name="updatedBillingAddress"> An AddressRequest object containing the updated BillingAddress record </param>
        /// <param name="format"> The format used for the data transfer (XML or JSON) </param>
        /// <returns> An AddressResponse object corresponding to the newly-updated BillingAddress </returns>
        public AddressResponse UpdateBillingAddressByCustomerReferenceId(string siteSubdomain, string customerReferenceId, string billingAddressId, AddressRequest updatedBillingAddress, string format = "xml")
        {
            return _service.Put<AddressRequest, AddressResponse>(string.Format("{0}/{1}/reference-id/{2}/billing-address/{3}.{4}", _gatewayURL, siteSubdomain, customerReferenceId, billingAddressId, format), updatedBillingAddress);
        }

        #endregion PUT

        #region DELETE

        // TODO: later on!
        ///// <summary>
        ///// Deletes all BillingAddress for a Customer
        ///// </summary>
        ///// <param name="customerId"> The Id of the Customer </param>
        ///// <param name="format"> The format of the Response </param>
        //void DeleteBillingAddresses(string customerId, string format= ContentFormat.XML);

        /// <summary>
        /// Deletes a BillingAddress for a Customer
        /// </summary>
        /// <param name="customerId"> The Id of the Customer </param>
        /// <param name="billingAddressId"> The Id of the BillingAddress </param>
        public void DeleteBillingAddress(string customerId, string billingAddressId)
        {
            _service.Delete(string.Format("{0}/{1}/billing-address/{2}", _gatewayURL, customerId, billingAddressId));
        }

        #endregion DELETE

        #endregion

        #region Countries

        /// <summary>
        /// Gets all Countries
        /// </summary>
        /// <returns>Countries collection</returns>
        public CountryList GetCountries(string format = "xml")
        {
            return _service.Get<CountryList>(string.Format("{0}/country.{1}", _gatewayURL, format));
        }

        /// <summary>
        /// Gets a Country by Id
        /// </summary>
        /// <param name="countryId"> The Id of the Country </param>
        /// <param name="format"> Data transfer format </param>
        /// <returns>Country</returns>
        public Country GetCountry(string countryId, string format = "xml")
        {
            return _service.Get<Country>(string.Format("{0}/country/by-id/{1}.{2}", _gatewayURL, countryId, format));
        }

        /// <summary>
        /// Gets a Country by ISO code (either 2 or 3 letter codes)
        /// </summary>
        /// <param name="countryIsoCode"> The Country ISO code, either 2 or 3 letters </param>
        /// <param name="format"> Data transfer format </param>
        /// <returns>Country</returns>
        public Country GetCountryByCode(string countryIsoCode, string format = "xml")
        {
            return _service.Get<Country>(string.Format("{0}/country/by-code/{1}.{2}", _gatewayURL, countryIsoCode, format));
        }

        /// <summary>
        /// Gets a Contry by Name
        /// </summary>
        /// <param name="countryName"> The Name of the Country </param>
        /// <param name="format"> Data transfer format </param>
        /// <returns> A Country object corresponding to the requested Country </returns>
        public Country GetCountryByName(string countryName, string format = "xml")
        {
            return _service.Get<Country>(string.Format("{0}/country/by-name/{1}.{2}", _gatewayURL, countryName, format));
        }

        #endregion Countries

        #region StateProvince

        /// <summary>
        /// Gets all StatesProvinces by Country Id
        /// </summary>
        /// <param name="countryId"> The Country Id </param>
        /// <param name="format"> Data transfer format </param>
        /// <returns>StatesProvinces collection</returns>
        public StateProvinceList GetStateProvinces(string countryId, string format = "xml")
        {
            return _service.Get<StateProvinceList>(string.Format("{0}/stateprovince/by-country-id/{1}.{2}", _gatewayURL, countryId, format));
        }

        /// <summary>
        /// Gets all StatesProvinces by Country ISO code (2 letters)
        /// </summary>
        /// <param name="countryTwoLetterISOCode"> The two-letter ISO country code </param>
        /// <param name="format"> Data transfer format </param>
        /// <returns>StatesProvinces collection</returns>
        public StateProvinceList GetStateProvincesByCountryCode(string countryTwoLetterISOCode, string format = "xml")
        {
            return _service.Get<StateProvinceList>(string.Format("{0}/stateprovince/by-country-code/{1}.{2}", _gatewayURL, countryTwoLetterISOCode, format));
        }

        /// <summary>
        /// Gets a StateProvince by Id
        /// </summary>
        /// <param name="stateProvinceId"> The Id of the StateProvince </param>
        /// <param name="format"> Data transfer format </param>
        /// <returns>StateProvince</returns>
        public StateProvince GetStateProvince(string stateProvinceId, string format = "xml")
        {
            return _service.Get<StateProvince>(string.Format("{0}/stateprovince/by-id/{1}.{2}", _gatewayURL, stateProvinceId, format));
        }

        /// <summary>
        /// Gets a StateProvince by Name
        /// </summary>
        /// <param name="stateProvinceName"> The Name of the StateProvince </param>
        /// <param name="format"> Data transfer format </param>
        /// <returns> A StateProvince object corresponding to the specified Name </returns>
        public StateProvince GetStateProvinceByName(string stateProvinceName, string format = "xml")
        {
            return _service.Get<StateProvince>(string.Format("{0}/stateprovince/by-name/{1}.{2}", _gatewayURL, stateProvinceName, format));
        }

        #endregion StateProvince

        #region Currency

        /// <summary>
        /// Gets all Currencies
        /// </summary>
        /// <param name="format"> Data transfer format (XML/JSON) </param>
        /// <returns>CurrencyList</returns>
        public CurrencyList GetCurrencies(string format = "xml")
        {
            return _service.Get<CurrencyList>(string.Format("{0}/currency.{1}", _gatewayURL, format));
        }

        /// <summary>
        /// Gets a Currency by Id
        /// </summary>
        /// <param name="currencyId"> The Id of the Currency </param>
        /// <param name="format"> Data transfer format (XML/JSON) </param>
        /// <returns>Currency</returns>
        public Currency GetCurrency(string currencyId, string format = "xml")
        {
            return _service.Get<Currency>(string.Format("{0}/currency/by-id/{1}.{2}", _gatewayURL, currencyId, format));
        }

        /// <summary>
        /// Gets a Currency by Code
        /// </summary>
        /// <param name="currencyCode"> The Currency code (e.g. USD, EUR) </param>
        /// <param name="format"> Data transfer format (XML/JSON) </param>
        /// <returns>Currency</returns>
        public Currency GetCurrencyByCode(string currencyCode, string format = "xml")
        {
            return _service.Get<Currency>(string.Format("{0}/currency/by-code/{1}.{2}", _gatewayURL, currencyCode, format));
        }

        /// <summary>
        /// Gets the currency for a Country
        /// </summary>
        /// <param name="countryThreeISOCode"> The 3-letter Country code </param>
        /// <param name="format"> Data transfer format (XML/JSON) </param>
        /// <returns>Currency</returns>
        public Currency GetCurrencyForCountryThreeIsoCode(string countryThreeISOCode, string format = "xml")
        {
            return _service.Get<Currency>(string.Format("{0}/currency/by-country-code/{1}.{2}", _gatewayURL, countryThreeISOCode, format));
        }

        #endregion Currency
    }
}
