using System.Web;
using System.Web.Optimization;

namespace Predictions
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/FrontEnd/Scripts/jquery-1.11.0.js",
                        "~/FrontEnd/Scripts/jquery.unobtrusive-ajax.min.js",
                        "~/FrontEnd/Scripts/jquery.validate.js"));


            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-2.6.2"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/FrontEnd/Scripts/bootstrap.min.js",
                      "~/FrontEnd/Scripts/bootstrap-select.min.js",
                      "~/FrontEnd/Scripts/respond.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/FrontEnd/Content/bootstrap.css",
                      "~/FrontEnd/Content/flatly.bootstrap.min.css",
                      "~/FrontEnd/Content/site.css",
                      "~/FrontEnd/Content/bootstrap-select.min.css"
                      ));

            BundleTable.EnableOptimizations = true;
        }
    }
}