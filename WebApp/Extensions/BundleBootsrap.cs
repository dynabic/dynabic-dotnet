using System.Web.Optimization;

namespace WebApp.Extensions
{
    public static class DefaultBundle
    {
        public static void SetDefaultBundle(this BundleCollection bundles)
        {
            #region CSS Bundle
            var defaultCSSs = new Bundle("~/Content/themes/base/css/css", new CssMinify());
            //main CSS
            defaultCSSs.AddFile("~/Content/themes/base/css/main.css");
            //bootstarp CSS files
            defaultCSSs.AddFile("~/Content/themes/base/css/bootstrap.css");
            defaultCSSs.AddFile("~/Content/themes/base/css/bootstrap-responsive.css");
            //CSS file for bootstrap-datepicker
            defaultCSSs.AddFile("~/Content/themes/base/css/datepicker.css");
            bundles.Add(defaultCSSs);
            #endregion

            #region JavaScript Bundle
            var defaultJSs = new Bundle("~/Content/themes/base/js/js", new JsMinify());
            //load jQuery library
            defaultJSs.AddFile("~/Content/themes/base/js/jquery-1.7.2.min.js");
            //bootstrap JS files
            defaultJSs.AddFile("~/Content/themes/base/js/bootstrap.js");
            //bootstrap-datepicker
            defaultJSs.AddFile("~/Content/themes/base/js/bootstrap-datepicker.js");
            bundles.Add(defaultJSs);
            #endregion
        }
    }
}