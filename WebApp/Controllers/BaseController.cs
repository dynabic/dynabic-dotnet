using System.Web.Mvc;
using DynabicBilling.Classes;
using DynabicPlatform.Classes;
using WebApp.Classes;

namespace WebApp.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        //allocate the reference for DynabicPlatform and DynabicBilling Gateways
        protected PlatformGateway _dynabicPlatformGateway;
        protected BillingGateway _dynabicBillingGateway;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            //Instantiate DynabicPlatform and DynabicBilling Gateways with the provided configuration.
            //Modify these configuration variables in Config class.
            _dynabicPlatformGateway = new PlatformGateway(
                Config.PlatformEnvironment,
                Config.PublicKey,
                Config.PrivateKey);

            _dynabicBillingGateway = new BillingGateway(
                Config.BillingEnvironment,
                Config.PublicKey,
                Config.PrivateKey);
        }
    }
}
