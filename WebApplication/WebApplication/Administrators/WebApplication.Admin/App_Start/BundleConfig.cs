using System.Web;
using System.Web.Optimization;

namespace WebApplication.Admin
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap/css/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/font-awesome/4.3.0/css/font-awesome.min.css",
                      "~/Content/ionicons/2.0.1/css/ionicons.min.css",
                      "~/Content/dist/css/AdminLTE.min.css",
                      "~/Content/dist/css/AdminLTE.addon.css",
                      "~/Content/dist/css/skins/skin-green.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/others").Include(
                      "~/Content/dist/js/app.min.js",
                      "~/Content/respond/1.4.2/respond.min.js",
                      "~/Content/html5shiv/3.7.2/html5shiv.min.js"));
        }
    }
}
