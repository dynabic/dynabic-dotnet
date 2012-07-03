#pragma warning disable 1591

using System;
using DynabicPlatform.Interfaces;

namespace DynabicPlatform.Classes
{
    public class PlatformEnvironment : IDynabicEnvironment
    {
        public static PlatformEnvironment DEVELOPMENT = new PlatformEnvironment(EnvironmentType.Development);
        public static PlatformEnvironment QA = new PlatformEnvironment(EnvironmentType.QA);
        public static PlatformEnvironment PRODUCTION = new PlatformEnvironment(EnvironmentType.Production);

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
                        return "https://stage-api.Dynabic.com/Platform";
                    case EnvironmentType.Production:
                        return "https://api.Dynabic.com/Platform";
                    default:
                        throw new Exception("Unsupported environment.");
                }
            }
        }

        private PlatformEnvironment(EnvironmentType environmentType)
        {
            this.EnvironmentType = environmentType;
        }

        private static String DevelopmentUrl()
        {
            return "http://api.local-dynabic.com/ApiPlatform";
        }
    }
}
