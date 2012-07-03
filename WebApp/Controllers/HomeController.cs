using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApp.Classes;
using WebApp.Classes.Prices;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your quintessential app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your quintessential contact page.";

            return View();
        }

        [AllowAnonymous]
        //[OutputCache(Duration = 120)]
        public ActionResult Prices()
        {
            var model = new PriceModel();
            model.ProductFamilies = _dynabicBillingGateway.ProductFamilies.GetProductFamilies(Config.MySiteSubdomain);
            if (model.ProductFamilies.Count > 0)
            {
                model.PlanItems = LoadPriceData(model.ProductFamilies[0].Id);
            }

            return View(model);
        }

        [AllowAnonymous]
        //[OutputCache(Duration = 120, VaryByParam = "productFamilyId")]
        public PartialViewResult PriceByFamily(int productFamilyId)
        {
            var model = new PriceModel();
            model.PlanItems = LoadPriceData(productFamilyId);
            return PartialView("Partials/PricingPlan", model);
        }

        private List<PlanItem> LoadPriceData(int productFamilyId)
        {
            var planItems = new List<PlanItem>();

            var products = _dynabicBillingGateway.Products.GetProductsByFamilyId(productFamilyId.ToString());
            foreach (var product in products)
            {
                foreach (var pricingPlan in product.PricingPlans)
                {
                    foreach (var productItem in pricingPlan.ProductItemsList)
                    {
                        var planItem = planItems.FirstOrDefault(item => item.PlanName == product.Name && item.ItemName == productItem.Name);
                        if (planItem == null)
                        {
                            planItem = new PlanItem
                            {
                                PlanName = product.Name,
                                ItemName = productItem.Name,
                                ChargeModel = productItem.ChargeModel,
                                ProductItemType = productItem.ProductItemType,
                                PricingPlanPaymentSchedule = pricingPlan.PaymentScheduleList[0],
                                CurrencyCode = pricingPlan.CurrencyCode,
                            };
                            planItems.Add(planItem);
                        }
                        foreach (var meteredPrice in productItem.MeteredPriceList)
                        {
                            planItem.MeteredPrices.Add(new MeteredPrice
                            {
                                StartQuantity = meteredPrice.StartQuantity,
                                EndQuantity = meteredPrice.EndQuantity,
                                UnitPrice = meteredPrice.UnitPrice,
                                UnitName = productItem.UnitName,
                                CurrencyCode = pricingPlan.CurrencyCode,
                            });
                        }
                    }
                }
            }

            return planItems;
        }

    }
}
