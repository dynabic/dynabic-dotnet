using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    /// <summary>
    /// Repesents a Product. Used as responde so users requests
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "product_response")]
    public class ProductResponse : ProductBase
    {
        #region Data Members

        /// <summary>
        /// Shows if a Product is marked or not as Archieved
        /// </summary>
        [DataMember(Name = "is_archived", IsRequired = false)]
        public bool isArchived { set; get; }

        /// <summary>
        /// A list of Pricing Plans represented as a collection of PricingPlanResponse objects
        /// </summary>
        [DataMember(Name = "pricing_plans", IsRequired = true)]
        public PricingPlanResponseList PricingPlans { set; get; }

        #endregion

        public ProductResponse()
        {
            this.PricingPlans = new PricingPlanResponseList();
        }

        public static implicit operator ProductRequest(ProductResponse response)
        {
            if (response == null) return null;
            return new ProductRequest
            {
                AccountingCode = response.AccountingCode,
                ApiRef1 = response.ApiRef1,
                Description = response.Description,
                FamilyId = response.FamilyId,
                Id = response.Id,
                isBillingAddressAtSignupRequired = response.isBillingAddressAtSignupRequired,
                isCreditCardAtSignupRequired = response.isCreditCardAtSignupRequired,
                Name = response.Name,
                PricingPlans = response.PricingPlans,
            };
        }

    }
}