#pragma warning disable 1591

using System;

namespace DynabicPlatform.Exceptions
{
    [Serializable]
    public class AuthorizationException : DynabicBillingException
    {
        public AuthorizationException(String message) : base(message) { }
    }
}
