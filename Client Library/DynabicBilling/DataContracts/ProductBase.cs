using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// Represents a Product.
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "product")]
    public class ProductBase
    {
        #region Data Members

        /// <summary>
        /// Product's unique identifier
        /// </summary>
        [DataMember(Name = "id", IsRequired = true)]
        public int Id { set; get; }

        /// <summary>
        /// Id of the ProductFamily to which the Product belongs to
        /// </summary>
        [DataMember(Name = "product_family_id", IsRequired = true)]
        public int FamilyId { set; get; }

        /// <summary>
        /// Name of the Product
        /// </summary>
        [DataMember(Name = "name", IsRequired = true)]
        public string Name { set; get; }

        /// <summary>
        /// Product's description
        /// </summary>
        [DataMember(Name = "description", IsRequired = false)]
        public string Description { set; get; }

        /// <summary>
        /// Specifies if the user will be requested to provide credit card information when signing up for this product
        /// </summary>
        [DataMember(Name = "is_credit_card_at_signup_required", IsRequired = false)]
        public BoolOptional isCreditCardAtSignupRequired { set; get; }

        /// <summary>
        /// Will the user be requested to provide the billing address when signing up for this product?
        /// </summary>
        [DataMember(Name = "is_billing_address_at_signup_required", IsRequired = false)]
        public BoolOptional isBillingAddressAtSignupRequired { set; get; }

        /// <summary>
        /// Api Ref 1 - A name by which this product can be refferred to in API use,
        /// we will simply pass this value back in our REST interface.
        /// The user may, for example, wish to make it the KEY/ID/HANDLE of the 
        /// product within their own website.
        /// </summary>
        [DataMember(Name = "api_reference1", IsRequired = false)]
        public string ApiRef1 { set; get; }

        /// <summary>
        /// A key to the user accounting system
        /// </summary>
        [DataMember(Name = "accounting_code", IsRequired = false)]
        public string AccountingCode { set; get; }

        #endregion

        protected ProductBase()
        {
        }
    }
}