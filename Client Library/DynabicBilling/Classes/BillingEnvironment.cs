#pragma warning disable 1591

using System;
using DynabicPlatform.Classes;
using DynabicPlatform.Interfaces;

namespace DynabicBilling.Classes
{
    public class BillingEnvironment : IDynabicEnvironment
    {
        public static BillingEnvironment DEVELOPMENT = new BillingEnvironment(EnvironmentType.Development);
        public static BillingEnvironment QA = new BillingEnvironment(EnvironmentType.QA);
        public static BillingEnvironment PRODUCTION = new BillingEnvironment(EnvironmentType.Production);

        public EnvironmentType EnvironmentType { get; private set; }

        public String GatewayURL
        {
            get
            {
                switch (this.EnvironmentType)
                {
                    case EnvironmentType.Development:
                        return DevelopmentUrl();
                    case EnvironmentType.QA:
                        return "https://stage-api.Dynabic.com/Billing";
                    case EnvironmentType.Production:
                        return "https://api.Dynabic.com/Billing";
                    default:
                        throw new Exception("Unsupported environment.");
                }
            }
        }

        private BillingEnvironment(EnvironmentType environmentType)
        {
            this.EnvironmentType = environmentType;
        }

        private static String DevelopmentUrl()
        {
            // Access environment variables lazily to avoid issues on servers where access to environment variables is restricted
            var host = System.Environment.GetEnvironmentVariable("GATEWAY_HOST") ?? "localhost";
            var port = System.Environment.GetEnvironmentVariable("GATEWAY_PORT") ?? "3000";

            //return String.Format("http://{0}:{1}", host, port);
            return "http://api.local-dynabic.com/ApiBilling";
        }
    }
}
