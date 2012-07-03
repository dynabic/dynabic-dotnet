using System.Collections.Generic;
using DynabicBilling.RestApiDataContract;
using WebApp.Classes.Prices;

namespace WebApp.Models
{
    public class PriceModel
    {
        public PriceModel()
        {
            this.PlanItems = new List<PlanItem>();
            this.ProductFamilies = new ProductFamilyResponseList();
        }

        public ProductFamilyResponseList ProductFamilies { get; set; }
        public List<PlanItem> PlanItems { get; set; }
    }
}