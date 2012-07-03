using System.Collections.Generic;
using DynabicBilling.RestApiDataContract;

namespace WebApp.Classes.Prices
{
    public class PlanItem
    {
        public PlanItem()
        {
            this.MeteredPrices = new List<MeteredPrice>();
        }

        public string PlanName { get; set; }
        public string ItemName { get; set; }
        public string CurrencyCode { get; set; }
        public ProductPricingPlanPaymentScheduleResponse PricingPlanPaymentSchedule { get; set; }
        public ChargeModel ChargeModel { get; set; }
        public ProductItemType ProductItemType { get; set; }
        public List<MeteredPrice> MeteredPrices { get; set; }
    }
}