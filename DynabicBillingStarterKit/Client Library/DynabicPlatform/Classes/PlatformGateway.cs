#pragma warning disable 1591

using System;
using System.Net;
using DynabicPlatform.Interfaces;
using DynabicPlatform.RestAPI.RestInterfaces;
using DynabicPlatform.Services;

namespace DynabicPlatform.Classes
{
    /// <summary>
    /// This is the primary interface to the DynabicPlatform Gateway.
    /// </summary>
    /// <remarks>
    /// This class interact with:
    /// <ul>
    /// <li><see cref="ApplicationService">Application</see></li>
    /// <li><see cref="CompanyInfoService">CompanyInfo</see></li>
    /// <li><see cref="EventService">Events</see></li>
    /// <li><see cref="SettingService">Settings</see></li>
    /// <li><see cref="SiteService"></see>Sites</li>
    /// <li><see cref="UserService"></see>Users</li>
    /// </ul>  
    /// </remarks>
    /// <example>
    /// Quick Start Example:
    /// <code>
    /// using System;
    /// using DynabicPlatform;
    ///
    /// namespace DynabicPlatformExample
    /// {
    ///     class Program
    ///     {
    ///         static void Main(string[] args)
    ///         {
    ///             var gateway = new PlatformGateway
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
    public class PlatformGateway
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

        public PlatformGateway()
        {
            Configuration = new Configuration();
        }

        public PlatformGateway(PlatformEnvironment environment, string publicKey, string privateKey)
        {
            Configuration = new Configuration(environment, publicKey, privateKey);
        }

        public PlatformGateway(PlatformEnvironment environment, Cookie authenticationCookie)
        {
            Configuration = new Configuration(environment, authenticationCookie);
        }

        public virtual IApplicationsService Application
        {
            get { return new ApplicationService(new CommunicationLayer(this.Configuration)); }
        }

        public virtual ICompanyInfoService CompanyInfo
        {
            get { return new CompanyInfoService(new CommunicationLayer(Configuration)); }
        }

        public virtual IEventsService Events
        {
            get { return new EventService(new CommunicationLayer(Configuration)); }
        }

        public virtual ISettingsService Settings
        {
            get { return new SettingService(new CommunicationLayer(Configuration)); }
        }

        public virtual ISitesService Sites
        {
            get { return new SiteService(new CommunicationLayer(Configuration)); }
        }

        public virtual IUsersService Users
        {
            get { return new UserService(new CommunicationLayer(Configuration)); }
        }
    }
}
