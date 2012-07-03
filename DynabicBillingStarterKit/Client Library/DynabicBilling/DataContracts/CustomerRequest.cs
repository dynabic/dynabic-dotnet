using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// Represents a Customer
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "customer_request")]
    public class CustomerRequest : CustomerBase
    {
        #region Data Members

        /// <summary>
        /// Customer's shipping address. Used as user's request to Create or Update an Address record
        /// </summary>
        [DataMember(Name = "shipping_address", IsRequired = false)]
        public AddressRequest ShippingAddress { set; get; }

        #endregion

        public CustomerRequest() : base() { }
    }
}