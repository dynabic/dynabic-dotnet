using DynabicBilling.Classes;
using DynabicPlatform.Classes;

namespace WebApp.Classes
{
    public class Config
    {
        public static PlatformEnvironment PlatformEnvironment = PlatformEnvironment.QA;
        public static BillingEnvironment BillingEnvironment = BillingEnvironment.QA;

        //these are the API keys for our demo account
        //replace these values with your own API keys

        public const string PublicKey = "5f0707e3a6b2455590f1";
        public const string PrivateKey = "5de505742e4040ac8dc4";

        //your Dynabic Sites subdomain
        public const string MySiteSubdomain = "demo-site";
    }
}