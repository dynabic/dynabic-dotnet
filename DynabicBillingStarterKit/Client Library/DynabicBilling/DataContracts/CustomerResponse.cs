using System;
using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// Contains all information about a Customer.
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "customer_response")]
    public class CustomerResponse : CustomerBase
    {
        #region Data Members

        /// <summary>
        /// Id of the Customer. Is generated and managed by Database
        /// </summary>
        [DataMember(Name = "id", IsRequired = false)]
        public int Id { set; get; }

        /// <summary>
        /// Id of the Site where the Customer is registered. It is auto-assigned
        /// </summary>
        [DataMember(Name = "site_id", IsRequired = false)]
        public int SiteId { set; get; }

        /// <summary>
        /// Date when the Customer was registered. It is auto-generated at the moment of registration
        /// </summary>
        [DataMember(Name = "registration_date", IsRequired = false)]
        public DateTime RegistrationDate { set; get; }

        /// <summary>
        /// Customer's shipping address. Contains all information about an Address
        /// </summary>
        [DataMember(Name = "shipping_address", IsRequired = false)]
        public AddressResponse ShippingAddress { get; set; }

        #endregion

        public CustomerResponse()
            : base()
        {
            this.RegistrationDate = DateTime.MinValue.ToUniversalTime();
        }

        public static implicit operator CustomerRequest(CustomerResponse response)
        {
            if (response == null) return null;
            return new CustomerRequest
            {
                Company = response.Company,
                Email = response.Email,
                FirstName = response.FirstName,
                IsShippingAddressEqualToBilling = response.IsShippingAddressEqualToBilling,
                LastName = response.LastName,
                Phone = response.Phone,
                ReferenceId = response.ReferenceId,
                ShippingAddress = response.ShippingAddress,
                ShippingAddressId = response.ShippingAddressId,
            };
        }

    }
}