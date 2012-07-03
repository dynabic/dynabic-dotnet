using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// Represents a Customer. Has all the information to what user has access to
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "customer")]
    public class CustomerBase
    {
        #region Data Members

        /// <summary>
        /// A name by which a Customer can be refferred to in a friendly manner,
        /// we will simply pass this value back in our REST interface.
        /// The user may, for example, wish to make it the KEY/ID/HANDLE of the 
        /// Customer within their own website
        /// </summary>
        [DataMember(Name = "reference", IsRequired = false)]
        public string ReferenceId { set; get; }

        /// <summary>
        /// Id of the Shipping Address
        /// </summary>
        [DataMember(Name = "shipping_address_id", IsRequired = false)]
        public int? ShippingAddressId { set; get; }

        /// <summary>
        /// First Name of the Customer
        /// </summary>
        [DataMember(Name = "first_name", IsRequired = true)]
        public string FirstName { set; get; }

        /// <summary>
        /// Last Name of the Customer
        /// </summary>
        [DataMember(Name = "last_name", IsRequired = true)]
        public string LastName { set; get; }

        /// <summary>
        /// Customer's email address
        /// </summary>
        [DataMember(Name = "email", IsRequired = true)]
        public string Email { set; get; }

        /// <summary>
        /// Customer's phone number
        /// </summary>
        [DataMember(Name = "phone", IsRequired = false)]
        public string Phone { set; get; }

        /// <summary>
        /// Customer's company name
        /// </summary>
        [DataMember(Name = "company", IsRequired = false)]
        public string Company { set; get; }

        /// <summary>
        /// Shows if the shipping address is the same to billing address.
        /// </summary>
        [DataMember(Name = "is_shipping_address_same_to_billing", IsRequired = false)]
        public bool IsShippingAddressEqualToBilling { set; get; }

        #endregion

        protected CustomerBase()
        {
            this.FirstName = null;
            this.LastName = null;
            this.Email = null;
            this.Company = null;
            this.ReferenceId = null;
        }
    }
}