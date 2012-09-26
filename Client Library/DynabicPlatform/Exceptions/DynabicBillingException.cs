#pragma warning disable 1591

using System;

namespace DynabicPlatform.Exceptions
{
    [Serializable]
    public class DynabicBillingException : Exception
    {
        public DynabicBillingException(String message) : base(message) { }
        public DynabicBillingException() : base() { }
    }
}
