using System;
using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// Address used as user's response. Besides basic informations, it contains additional (imutable) information.
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "address_response")]
    public class AddressResponse : AddressBase
    {
        #region Data Members

        /// <summary>
        /// Id of the Address. It is auto-generated.
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { set; get; }

        /// <summary>
        /// Id of the Customer to whom this Address is attached. It is auto-generated.
        /// </summary>
        [DataMember(Name = "customer_id")]
        public int? CustomerId { set; get; }

        /// <summary>
        /// Date when the Address was created. It is auto-generated
        /// </summary>
        [DataMember(Name = "created_on")]
        public DateTime CreatedOn { set; get; }

        /// <summary>
        /// Date when the Address was last time updated. It is auto-generated
        /// </summary>
        [DataMember(Name = "updated_on")]
        public DateTime UpdatedOn { set; get; }

        #endregion

        public AddressResponse()
            : base()
        {
            this.CreatedOn = DateTime.MinValue.ToUniversalTime();
            this.UpdatedOn = DateTime.MinValue.ToUniversalTime();
        }

        public static implicit operator AddressRequest(AddressResponse response)
        {
            if (response == null) return null;
            return new AddressRequest
            {
                Address1 = response.Address1,
                Address2 = response.Address2,
                City = response.City,
                Company = response.Company,
                Country = response.Country,
                Email = response.Email,
                FaxNumber = response.FaxNumber,
                FirstName = response.FirstName,
                LastName = response.LastName,
                PhoneNumber = response.PhoneNumber,
                StateProvince = response.StateProvince,
                ZipPostalCode = response.ZipPostalCode,
            };
        }
    }
}