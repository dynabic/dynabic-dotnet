#pragma warning disable 1591

using System;
using System.Net;
using DynabicBilling.RestAPI.RestInterfaces;
using DynabicBilling.RestApiDataContract.RestInterfaces;
using DynabicPlatform.Classes;
using DynabicPlatform.Interfaces;
using DynabicPlatform.RestAPI.RestInterfaces;
using DynabicPlatform.Services;

namespace DynabicBilling.Classes
{
    /// <summary>
    /// This is the primary interface to the DynabicBilling Gateway.
    /// </summary>
    /// <remarks>
    /// This class interacts with:
    /// <ul>
    /// <li><see cref="SubscriptionService">Subscriptions</see></li>
    /// <li><see cref="TransactionService">Transactions</see></li>
    /// </ul>  
    /// </remarks>
    /// <example>
    /// Quick Start Example:
    /// <code>
    /// using System;
    /// using DynabicBilling;
    ///
    /// namespace DynabicBillingExample
    /// {
    ///     class Program
    ///     {
    ///         static void Main(string[] args)
    ///         {
    ///             var gateway = new BillingGateway
    ///             {
    ///                 Environment = Environment.PRODUCTION,
    ///                 PublicKey = "a_public_key",
    ///                 PrivateKey = "a_private_key"
    ///             };
    ///
    ///             var request = new SubscriptionRequest
    ///             {
    ///                 ProductId = 11,
    ///                 ...
    ///                 CustomerRequest = new CustomerRequest
    ///                 {
    ///                     Name = "John Doe",
    ///                     ...
    ///                 }
    ///             };
    ///
    ///             SubscriptionResponse subscription = gateway.Subscription.Create(request);
    ///
    ///             Console.WriteLine(String.Format("Subscription ID: {0}", subscription.Id));
    ///             Console.WriteLine(String.Format("Status: {0}", subscription.Status));
    ///         }
    ///     }
    /// }
    /// </code>
    /// </example>
    public class BillingGateway
    {
        public IDynabicEnvironment Environment
        {
            get { return Configuration.Environment; }
            set { Configuration.Environment = value; }
        }

        public String PublicKey
        {
            get { return Configuration.PublicKey; }
            set { Configuration.PublicKey = value; }
        }

        public String PrivateKey
        {
            get { return Configuration.PrivateKey; }
            set { Configuration.PrivateKey = value; }
        }

        public Configuration Configuration { get; set; }

        public BillingGateway()
        {
            Configuration = new Configuration();
        }

        public BillingGateway(BillingEnvironment environment, string publicKey, string privateKey)
        {
            Configuration = new Configuration(environment, publicKey, privateKey);

            // Prepare Platform Configuration
            PlatformEnvironment platformEnvironment = null;
            switch (environment.EnvironmentType)
            {
                case EnvironmentType.Development:
                    platformEnvironment = PlatformEnvironment.DEVELOPMENT;
                    break;
                case EnvironmentType.QA:
                    platformEnvironment = PlatformEnvironment.QA;
                    break;
                case EnvironmentType.Production:
                    platformEnvironment = PlatformEnvironment.PRODUCTION;
                    break;
            }
            _platformConfiguration = new Configuration(platformEnvironment, publicKey, privateKey);
        }

        public BillingGateway(BillingEnvironment environment, Cookie authenticationCookie)
        {
            Configuration = new Configuration(environment, authenticationCookie);
            // Prepare Platform Configuration
            PlatformEnvironment platformEnvironment = null;
            switch (environment.EnvironmentType)
            {
                case EnvironmentType.Development:
                    platformEnvironment = PlatformEnvironment.DEVELOPMENT;
                    break;
                case EnvironmentType.QA:
                    platformEnvironment = PlatformEnvironment.QA;
                    break;
                case EnvironmentType.Production:
                    platformEnvironment = PlatformEnvironment.PRODUCTION;
                    break;
            }
            _platformConfiguration = new Configuration(platformEnvironment, authenticationCookie);
        }

        public virtual CustomerService Customer
        {
            get { return new CustomerService(new CommunicationLayer(this.Configuration)); }
        }

        public virtual ISubscriptionsService Subscription
        {
            get { return new SubscriptionService(new CommunicationLayer(Configuration)); }
        }

        public virtual ITransactionsService Transaction
        {
            get { return new TransactionService(new CommunicationLayer(Configuration)); }
        }

        public virtual IProductFamiliesService ProductFamilies
        {
            get { return new ProductFamiliesService(new CommunicationLayer(Configuration)); }
        }

        public virtual IProductsService Products
        {
            get { return new ProductsService(new CommunicationLayer(Configuration)); }
        }

        public virtual IReportsService Reports
        {
            get { return new ReportsService(new CommunicationLayer(Configuration)); }
        }

        public virtual IStatementsService Statements
        {
            get { return new StatementsService(new CommunicationLayer(Configuration)); }
        }

        /// <summary>
        /// This method allows you to subscribe for webhooks notifications.
        /// 
        /// You should pass in the Webhook URL and Signatyre Key that you defined via Billing Web App (WebhookSettings screen @ dynabic.com/billing/settings/webhooks)
        /// and retrieve a WebhooksChannel object.
        /// 
        /// The WebhooksChannel object allows you to register for all Dynabic Webhooks and write your own .NET event handlers for them.
        /// 
        /// Please refer to the code below for a quickstart example.
        /// </summary>
        /// <param name="listenUrl"> 
        /// The URL to which to listen for webhook notifications.
        /// This should be the Webhook URL defined in the WebhooksSettings screen of the Billing Web App (dynabic.com/billing/settings/webhooks)
        /// </param>
        /// <param name="signatureKey"></param>
        /// <returns></returns>
        /// <example>
        /// <code>
        ///    var gateway = new BillingGateway
        ///    {
        ///        Environment = BillingEnvironment.QA,
        ///        PrivateKey = "19c7a0d97d2d4413aba5",
        ///        PublicKey = "19c7a0d97d2d4413aba5"
        ///    };
        ///
        ///    var channel = gateway.Subscribe("http://localhost/WebhookListener/", "dynabic");
        ///
        ///    channel.SignupSuccess += new EventHandler(channel_SignupSuccess);
        ///    
        ///    [...]
        ///    
        ///    void channel_SignupSuccess(object sender, SignupEventArgs e)
        ///    {
        ///       if (e.Customer == null)
        ///       {
        ///          return;
        ///       }
        ///       
        ///       Console.WriteLine("Signup successful for customer with e-mail: " + e.Customer.Email);
        ///    }
        ///    
        ///    [...]
        ///    // to unsubscribe, simply call:
        ///    channel.StopListening();
        /// </code>
        /// </example>
        public WebhooksChannel Subscribe(string listenUrl, string signatureKey)
        {
            WebhooksChannel channel = new WebhooksChannel(listenUrl, signatureKey)
            {
                Customers = Customer,
                Products = Products,
                Subscriptions = Subscription
            };

            if (!channel.StartListening())
            {
                channel = null;
            }

            return channel;
        }

        #region Platform

        private Configuration _platformConfiguration;

        public virtual ISitesService Sites
        {
            get { return new SiteService(new CommunicationLayer(_platformConfiguration)); }
        }

        public virtual IEventsService Events
        {
            get { return new EventService(new CommunicationLayer(_platformConfiguration)); }
        }

        #endregion Platform
    }
}
