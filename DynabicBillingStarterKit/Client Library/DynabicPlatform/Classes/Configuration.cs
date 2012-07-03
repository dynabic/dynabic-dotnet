#pragma warning disable 1591

using System.Net;
using DynabicPlatform.Interfaces;
namespace DynabicPlatform.Classes
{
    public class Configuration
    {
        public IDynabicEnvironment Environment { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public Cookie AuthenticationCookie { get; set; }

        public Configuration() { }

        public Configuration(IDynabicEnvironment environment, string publicKey, string privateKey)
        {
            this.Environment = environment;
            this.PublicKey = publicKey;
            this.PrivateKey = privateKey;
        }

        public Configuration(IDynabicEnvironment environment, Cookie authenticationCookie)
        {
            this.Environment = environment;
            this.AuthenticationCookie = authenticationCookie;
        }
    }
}
