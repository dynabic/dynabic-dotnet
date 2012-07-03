using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using DynabicBilling.RestAPI.RestInterfaces;
using DynabicBilling.RestApiDataContract;

namespace DynabicBilling
{
    /// <summary>
    /// Provides client-side support for Webhooks notifications
    /// </summary>
    public class WebhooksChannel
    {
        #region Data members

        #region Events corresponding to Dynabic webhooks

        /* Events corresponding to webhook types */
        public event EventHandler<SubscriptionStateChangedEventArgs> SubscriptionStateChanged;
        public event EventHandler<SignupEventArgs> SignupSuccess;
        public event EventHandler<SignupEventArgs> SignupFailure;
        public event EventHandler<PaymentEventArgs> PaymentSuccess;
        public event EventHandler<PaymentEventArgs> PaymentFailure;
        public event EventHandler<BillingDateChangedEventArgs> BillingDateChanged;
        public event EventHandler<RenewalEventArgs> RenewalSuccess;
        public event EventHandler<RenewalEventArgs> RenewalFailure;
        public event EventHandler<SubscriptionProductChangedEventArgs> SubscriptionProductChanged;
        public event EventHandler<ExpiringCardEventArgs> ExpiringCard;
        public event EventHandler<SubscriptionEventArgs> SubscriptionSuccess;
        public event EventHandler<SubscriptionEventArgs> SubscriptionFailure;

        #endregion Events corresponding to Dynabic webhooks

        private string _strListenUrl = string.Empty;
        private string _strSignatureKey = string.Empty;

        private bool _bListen = true;
        private int _listenerThreadTimeoutMs = 200;

        private string _strWebhookData = string.Empty;
        private string _strWebhookEventType = string.Empty;
        private string _strWebhookHash = string.Empty;

        private HttpListener _listener = null;
        private Thread _listenerThread = null;
        private object _lockObj = new object();

        private HttpListenerContext _listenerContext = null;
        private StreamReader _listenerReader = null;
        private StreamWriter _listenerWriter = null;

        private const string EVENT_TYPE_HEADER = "EventType";
        private const string HASH_HEADER = "Hash";

        #region Webhook event type names

        private const string SUBSCRIPTION_STATE_CHANGED = "Billing.SubscriptionStateChange";
        private const string SIGNUP_SUCCESS = "Billing.SignupSuccess";
        private const string SIGNUP_FAILURE = "Billing.SignupFailure";
        private const string PAYMENT_SUCCESS = "Billing.PaymentSuccess";
        private const string PAYMENT_FAILURE = "Billing.PaymentFailure";
        private const string BILLING_DATE_CHANGED = "Billing.BillingDateChange";
        private const string RENEWAL_SUCCESS = "Billing.RenewalSuccess";
        private const string RENEWAL_FAILURE = "Billing.RenewalFailure";
        private const string SUBSCRIPTION_PRODUCT_CHANGED = "Billing.SubscriptionProductChange";
        private const string EXPIRING_CARD = "Billing.ExpiringCard";
        private const string SUBSCRIPTION_SUCCESS = "Billing.SubscriptionSuccess";
        private const string SUBSCRIPTION_FAILURE = "Billing.SubscriptionFailure";

        #endregion Webhook event type names

        #region DynabicBilling services references

        internal ICustomersService Customers { get; set; }
        internal IProductsService Products { get; set; }
        internal ISubscriptionsService Subscriptions { get; set; }

        #endregion DynabicBilling services references

        #endregion Data members

        #region Public methods

        /// <summary>
        /// Causes the channel to start listening
        /// to the specified URL
        /// </summary>
        /// <returns> True, if the operation succeeded or false, otherwise </returns>
        /// <remarks>
        /// Because of some threading issues, decided to make this method private,
        /// so it is not called by the users of the starter kit
        /// </remarks>
        internal bool StartListening()
        {
            lock (_lockObj)
            {
                if (_listener != null && _listener.IsListening)
                {
                    return true;
                }

                try
                {
                    _listener = new HttpListener();
                    _listener.Prefixes.Add(_strListenUrl);

                    _listenerThread = new Thread(ListenThreadMethod);
                    _listenerThread.Start();

                    return true;
                }
                catch
                {
                    // TODO: exception details?
                    return false;
                }
            }
        }

        /// <summary>
        /// Causes the channel to stop listening
        /// to the specified URL
        /// </summary>
        public void StopListening()
        {
            lock (_lockObj)
            {
                if (IsListening)
                {
                    _bListen = false;

                    try
                    {
                        _listener.Abort();
                        _listenerThread.Abort();
                    }
                    catch
                    {
                        // TODO: anything?
                        _listenerThread.Join(_listenerThreadTimeoutMs);
                    }
                    finally
                    {
                        _listener = null;
                        _listenerThread = null;
                    }
                }
                else
                {
                    // TODO: anything?
                    if (_listener != null)
                    {
                        _listener = null;
                    }
                    if (_listenerThread != null)
                    {
                        _listenerThread = null;
                    }
                }
            }
        }

        #endregion Public methods

        #region Ctor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="listenUrl"> The URL to which to subscribe to </param>
        /// <param name="signatureKey"> The signature key used for authenticating webhooks </param>
        protected internal WebhooksChannel(string listenUrl, string signatureKey)
        {
            _strListenUrl = listenUrl;
            _strSignatureKey = signatureKey;
        }

        #endregion Ctor

        #region Properties

        /// <summary>
        /// Gets the URL to which the channel is bound
        /// </summary>
        public string ListenUrl
        {
            get
            {
                return _strListenUrl;
            }
        }

        /// <summary>
        /// Gets the listening state of the channel
        /// </summary>
        public bool IsListening
        {
            get
            {
                return (_listener != null &&
                        _listener.IsListening);
            }
        }

        #endregion Properties

        #region Private methods

        #region Event generator methods

        /* Methods to generate each type of events */

        private void OnSubscriptionStateChanged(SubscriptionResponse subscriptionResponse)
        {
            if (SubscriptionProductChanged != null)
            {
                SubscriptionStateChanged(this, new SubscriptionStateChangedEventArgs(subscriptionResponse));
            }
        }

        private void OnSignupSuccess(CustomerResponse customerResponse)
        {
            if (SignupSuccess != null)
            {
                SignupSuccess(this, new SignupEventArgs(customerResponse));
            }
        }

        private void OnSignupFailure(CustomerResponse customerResponse)
        {
            if (SignupFailure != null)
            {
                SignupFailure(this, new SignupEventArgs(customerResponse));
            }
        }

        private void OnPaymentSuccess(PaymentResponse paymentResponse)
        {
            if (PaymentSuccess != null)
            {
                PaymentSuccess(this, new PaymentEventArgs(paymentResponse));
            }
        }

        private void OnPaymentFailure(PaymentResponse paymentResponse)
        {
            if (PaymentFailure != null)
            {
                PaymentFailure(this, new PaymentEventArgs(paymentResponse));
            }
        }

        private void OnBillingDateChanged(SubscriptionResponse subscriptionResponse)
        {
            if (BillingDateChanged != null)
            {
                BillingDateChanged(this, new BillingDateChangedEventArgs(subscriptionResponse));
            }
        }

        private void OnRenewalSuccess(SubscriptionResponse subscriptionResponse)
        {
            if (RenewalSuccess != null)
            {
                RenewalSuccess(this, new RenewalEventArgs(subscriptionResponse));
            }
        }

        private void OnRenewalFailure(SubscriptionResponse subscriptionResponse)
        {
            if (RenewalFailure != null)
            {
                RenewalFailure(this, new RenewalEventArgs(subscriptionResponse));
            }
        }

        private void OnSubscriptionProductChanged(ProductResponse productResponse)
        {
            if (SubscriptionProductChanged != null)
            {
                SubscriptionProductChanged(this, new SubscriptionProductChangedEventArgs(productResponse));
            }
        }

        private void OnExpiringCard(CreditCardResponse creditCardResponse)
        {
            if (ExpiringCard != null)
            {
                ExpiringCard(this, new ExpiringCardEventArgs(creditCardResponse));
            }
        }

        private void OnSubscriptionSuccess(SubscriptionResponse subscriptionResponse)
        {
            if (SubscriptionSuccess != null)
            {
                SubscriptionSuccess(this, new SubscriptionEventArgs(subscriptionResponse));
            }
        }

        private void OnSubscriptionFailure(SubscriptionResponse subscriptionResponse)
        {
            if (SubscriptionFailure != null)
            {
                SubscriptionFailure(this, new SubscriptionEventArgs(subscriptionResponse));
            }
        }

        #endregion Event generator methods

        #region Notification helpers

        // TODO: add comments

        private void NotifySubscriptionFailure(string strWebhookData)
        {
            SubscriptionResponse sr = GetObjectFromXmlString<SubscriptionResponse>(strWebhookData);

            // TODO: should the event be triggered even for a null value?
            if (sr != null)
            {
                OnSubscriptionFailure(sr);
            }
        }

        private void NotifySubscriptionSuccess(string strWebhookData)
        {
            SubscriptionResponse sr = GetObjectViaRestApi<SubscriptionResponse>(strWebhookData, Subscriptions.GetSubscription);
            // TODO: should the event be triggered even for a null value?
            if (sr != null)
            {
                OnSubscriptionSuccess(sr);
            }
        }

        private void NotifyExpiringCard(string strWebhookData)
        {
            CreditCardResponse ccr = GetObjectViaRestApi<CreditCardResponse>(strWebhookData, Customers.GetCreditCard);

            // TODO: should the event be triggered even for a null value?
            if (ccr != null)
            {
                OnExpiringCard(ccr);
            }
        }

        private void NotifySubscriptionProductChanged(string strWebhookData)
        {
            ProductResponse pr = GetObjectViaRestApi<ProductResponse>(strWebhookData, Products.GetProductById);

            // TODO: should the event be triggered even for a null value?
            if (pr != null)
            {
                OnSubscriptionProductChanged(pr);
            }
        }

        private void NotifyRenewalFailure(string strWebhookData)
        {
            SubscriptionResponse sr = GetObjectViaRestApi<SubscriptionResponse>(strWebhookData, Subscriptions.GetSubscription);

            // TODO: should the event be triggered even for a null value?
            if (sr != null)
            {
                OnRenewalFailure(sr);
            }
        }

        private void NotifyRenewalSuccess(string strWebhookData)
        {
            SubscriptionResponse sr = GetObjectViaRestApi<SubscriptionResponse>(strWebhookData, Subscriptions.GetSubscription);

            // TODO: should the event be triggered even for a null value?
            if (sr != null)
            {
                OnRenewalSuccess(sr);
            }
        }

        private void NotifyBillingDateChanged(string strWebhookData)
        {
            SubscriptionResponse sr = GetObjectViaRestApi<SubscriptionResponse>(strWebhookData, Subscriptions.GetSubscription);

            // TODO: should the event be triggered even for a null value?
            if (sr != null)
            {
                OnBillingDateChanged(sr);
            }
        }

        private void NotifyPaymentFailure(string strWebhookData)
        {
            PaymentResponse pr = GetObjectFromXmlString<PaymentResponse>(strWebhookData);
            if (pr != null)
            {
                OnPaymentFailure(pr);
            }
        }

        private void NotifyPaymentSuccess(string strWebhookData)
        {
            PaymentResponse pr = GetObjectFromXmlString<PaymentResponse>(strWebhookData);
            if (pr != null)
            {
                OnPaymentSuccess(pr);
            }
        }

        private void NotifySignupSuccess(string strWebhookData)
        {
            CustomerResponse cr = GetObjectViaRestApi<CustomerResponse>(strWebhookData, Customers.GetCustomer);

            // TODO: should the event be triggered even for a null value?
            if (cr != null)
            {
                OnSignupSuccess(cr);
            }
        }

        private void NotifySignupFailure(string strWebhookData)
        {
            CustomerResponse cr = GetObjectFromXmlString<CustomerResponse>(strWebhookData);

            // TODO: should the event be triggered even for a null value?
            if (cr != null)
            {
                OnSignupFailure(cr);
            }
        }

        private void NotifySubscriptionStateChanged(string strWebhookData)
        {
            SubscriptionResponse sr = GetObjectViaRestApi<SubscriptionResponse>(strWebhookData, Subscriptions.GetSubscription);

            // TODO: should the event be triggered even for a null value?
            if (sr != null)
            {
                OnSubscriptionStateChanged(sr);
            }
        }

        #endregion Notification helpers

        #region Generic helpers

        /// <summary>
        /// Extracts the value contained in the <id></id> tag of the XML string passed as a parameter
        /// </summary>
        /// <param name="strWebhookData"> The XML string </param>
        /// <returns> The value contained in the <id></id> tag of the XML string </returns>
        private string GetEntityId(string strWebhookData)
        {
            XElement xe = null;
            string id = string.Empty;

            try
            {
                xe = XElement.Parse(strWebhookData);
                id = (from x in xe.Elements()
                      where x.Name.LocalName.ToLower().Equals("id")
                      select x.Value).FirstOrDefault();
            }
            catch
            {
                id = string.Empty;
            }

            return id;
        }

        /// <summary>
        /// Gets an object via REST Api, by Id
        /// 
        /// The Id is retrieved from a XML string via GetEntityId(string) method
        /// </summary>
        /// <typeparam name="T"> The type of the object to be retrieved via REST Api </typeparam>
        /// <param name="xmlString"> A string containing the XML data from which to extract the Id of the object </param>
        /// <param name="restMethodToCall"> 
        /// A reference to the REST method to call in order to obtain the desired object;
        /// this method has to be a Get[entity]ById one, taking the Id as a string parameter
        /// </param>
        /// <returns> An object of type T, obtained by calling the specified REST method with an argument (id) obtained from xmlString </returns>
        private T GetObjectViaRestApi<T>(string xmlString, Func<string, string, T> restMethodToCall) where T : class, new()
        {
            string objectId = GetEntityId(xmlString);
            if (string.IsNullOrEmpty(objectId))
            {
                // we don't have an ID, so there's no point in continuing
                return null;
            }

            T objectFromRestApi = null;

            try
            {
                objectFromRestApi = restMethodToCall(objectId, "xml");
            }
            catch
            {
                objectFromRestApi = null;
            }

            return objectFromRestApi;
        }

        /// <summary>
        /// Gets an object of type T from a XML string
        /// 
        /// <remarks>
        /// Used to create PaymentResponse objects from XML data;
        /// also used for Subscription/CustomerResponse objects in case of Failure events,
        /// when the corresponding objects can't be fetched from the DB via REST (because they don't exist in the DB)
        /// </remarks>
        /// </summary>
        /// <typeparam name="T"> The Type of the requested object </typeparam>
        /// <param name="xmlString"> A string containing the XML data from which to extract the object of type T </param>
        /// <returns> An object of type T, obtained from the XML string passed as a parameter </returns>
        private T GetObjectFromXmlString<T>(string xmlString) where T : class, new()
        {
            XElement xe = null;
            T objectFromXml = new T();

            try
            {
                xe = XElement.Parse(xmlString);
            }
            catch
            {
                // if anything goes wrong here, there's no point in continuing
                return null;
            }

            try
            {
                System.Collections.Generic.Dictionary<string, string> dict = xe.Elements().ToDictionary(x => x.Name.LocalName, x => x.Value);
                System.Reflection.PropertyInfo[] piList = objectFromXml.GetType().GetProperties();

                foreach (var pi in piList)
                {
                    if (pi.CanWrite)
                    {
                        if (dict.ContainsKey(pi.Name) && dict[pi.Name] != null)
                        {
                            string dictValue = dict[pi.Name];

                            if (!string.IsNullOrEmpty(dictValue))
                            {
                                Type piType = pi.PropertyType;

                                if (piType == typeof(string))
                                {
                                    pi.SetValue(objectFromXml, dictValue, null);
                                }
                                else if (piType == typeof(bool))
                                {
                                    bool bVal = false;
                                    if (bool.TryParse(dictValue, out bVal))
                                    {
                                        pi.SetValue(objectFromXml, bVal, null);
                                    }
                                }
                                else if (piType == typeof(int))
                                {
                                    int nVal = -1;
                                    if (int.TryParse(dictValue, out nVal))
                                    {
                                        pi.SetValue(objectFromXml, nVal, null);
                                    }
                                }
                                else if (piType == typeof(int?))
                                {
                                    int nVal = -1;
                                    bool bResult = int.TryParse(dictValue, out nVal);

                                    pi.SetValue(objectFromXml, bResult ? (int?)nVal : null, null);
                                }
                                else if (piType == typeof(decimal))
                                {
                                    decimal mVal = -1;
                                    if (decimal.TryParse(dictValue, out mVal))
                                    {
                                        pi.SetValue(objectFromXml, mVal, null);
                                    }
                                }
                                else if (piType == typeof(decimal?))
                                {
                                    decimal mVal = -1;
                                    bool bResult = decimal.TryParse(dictValue, out mVal);

                                    pi.SetValue(objectFromXml, bResult ? (decimal?)mVal : null, null);
                                }
                                else if (piType == typeof(DateTime))
                                {
                                    DateTime date = DateTime.MinValue;
                                    if (DateTime.TryParse(dictValue, out date))
                                    {
                                        pi.SetValue(objectFromXml, date, null);
                                    }
                                }
                                else if (piType == typeof(DateTime?))
                                {
                                    DateTime date = DateTime.MinValue;
                                    bool bResult = DateTime.TryParse(dictValue, out date);

                                    pi.SetValue(objectFromXml, bResult ? (DateTime?)date : null, null);
                                }
                            }
                        }
                    }
                }

                return objectFromXml;
            }
            catch
            {
                // if anything goes wrong here, just return null
                return null;
            }
        }

        #endregion Generic helpers

        #region Listen thread

        private void ListenThreadMethod()
        {
            try
            {
                _bListen = true;
                _listener.Start();

                while (_bListen)
                {
                    ProcessIncomingWebhook();
                }

                _listener.Stop();
                _listener = null;
            }
            catch
            {
                // TODO: anything?
            }
        }

        private void ProcessIncomingWebhook()
        {
            try
            {
                _listenerContext = _listener.GetContext();

                _listenerReader = new StreamReader(_listenerContext.Request.InputStream, Encoding.Unicode);
                _listenerWriter = new StreamWriter(_listenerContext.Response.OutputStream);

                // get the data
                _strWebhookEventType = (string)_listenerContext.Request.Headers[EVENT_TYPE_HEADER];
                _strWebhookHash = (string)_listenerContext.Request.Headers[HASH_HEADER];
                _strWebhookData = _listenerReader.ReadToEnd();

                if (VerifyMd5Hash(string.Concat(_strSignatureKey, _strWebhookData), _strWebhookHash))
                {
                    // Webhook authentication success
                    NotifyWebhook(_strWebhookEventType, _strWebhookData);
                    _listenerContext.Response.StatusCode = (int)HttpStatusCode.OK;
                }
                else
                {
                    // Webhook authentication error
                    _listenerContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                }

                // send a response back
                _listenerContext.Response.Close();
            }
            catch
            {
                // send a response back
                _listenerContext.Response.Close();

                // TODO: anything else?
            }
        }

        private void NotifyWebhook(string _strWebhookEventType, string _strWebhookData)
        {
            /* The notification (object extraction via REST API and event generation)
             * is purposely done on a separate thread, so as not to block the listener */
            new Thread(() =>
            {
                switch (_strWebhookEventType)
                {
                    case SUBSCRIPTION_STATE_CHANGED:
                        NotifySubscriptionStateChanged(_strWebhookData);
                        break;

                    case SIGNUP_SUCCESS:
                        NotifySignupSuccess(_strWebhookData);
                        break;

                    case SIGNUP_FAILURE:
                        NotifySignupFailure(_strWebhookData);
                        break;

                    case PAYMENT_SUCCESS:
                        NotifyPaymentSuccess(_strWebhookData);
                        break;

                    case PAYMENT_FAILURE:
                        NotifyPaymentFailure(_strWebhookData);
                        break;

                    case BILLING_DATE_CHANGED:
                        NotifyBillingDateChanged(_strWebhookData);
                        break;

                    case RENEWAL_SUCCESS:
                        NotifyRenewalSuccess(_strWebhookData);
                        break;

                    case RENEWAL_FAILURE:
                        NotifyRenewalFailure(_strWebhookData);
                        break;

                    case SUBSCRIPTION_PRODUCT_CHANGED:
                        NotifySubscriptionProductChanged(_strWebhookData);
                        break;

                    case EXPIRING_CARD:
                        NotifyExpiringCard(_strWebhookData);
                        break;

                    case SUBSCRIPTION_SUCCESS:
                        NotifySubscriptionSuccess(_strWebhookData);
                        break;

                    case SUBSCRIPTION_FAILURE:
                        NotifySubscriptionFailure(_strWebhookData);
                        break;

                    default:
                        break;
                }
            }).Start();
        }

        #endregion

        #region Webhooks authentication

        /// <summary>
        /// Checks whether an MD5 hash corresponds
        /// to a string or not
        /// </summary>
        /// <param name="input"> The string </param>
        /// <param name="hash"> The MD5 hash </param>
        /// <returns> True, if the hash corresponds to the input string or false, otherwise </returns>
        private bool VerifyMd5Hash(string input, string hash)
        {
            using (var md5Hash = MD5.Create())
            {
                // Hash the input.
                string hashOfInput = GetMd5Hash(md5Hash, input);

                // Create a StringComparer an compare the hashes.
                StringComparer comparer = StringComparer.OrdinalIgnoreCase;

                if (0 == comparer.Compare(hashOfInput, hash))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Computes the MD5 hash of a string
        /// and returns it as a HEX string
        /// </summary>
        /// <param name="md5Hash"> The MD5 object used to compute the hash </param>
        /// <param name="input"> The string whose hash is computed </param>
        /// <returns> A HEX string containing the MD5 has of the input string </returns>
        private string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        #endregion

        #endregion Private methods
    }
}
