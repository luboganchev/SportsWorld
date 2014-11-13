using System.Web;
using System.Web.Optimization;

namespace SportsWorld.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            RegisterStyleBundles(bundles);
            RegisterScriptBundles(bundles);

            BundleTable.EnableOptimizations = false;
        }

        private static void RegisterScriptBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                        "~/Scripts/kendo/kendo.all.min.js",
                        "~/Scripts/kendo/kendo.aspnetmvc.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/Kendo/jquery.min.js"));
                //.Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryUI").Include(
                        "~/Scripts/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/fieldCreate").Include(
                        "~/Scripts/Company/FieldCreate.js"));

            bundles.Add(new ScriptBundle("~/bundles/fieldDetails").Include(
                        "~/Scripts/Company/FieldDetails.js"));

            bundles.Add(new ScriptBundle("~/bundles/companyDetails").Include(
                        "~/Scripts/Company/CompanyDetails.js"));

            bundles.Add(new ScriptBundle("~/bundles/slider").Include(
                        "~/Scripts/bootstrap-slider.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/kendo").Include(
                        "~/Content/kendo/kendo.common.min.css",
                        "~/Content/kendo/kendo.common-bootstrap.min.css",
                        "~/Content/kendo/kendo.silver.min.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/jqueryUiStyle").Include(
                        "~/Content/jquery-ui.css"));

            bundles.Add(new StyleBundle("~/Content/slider").Include(
                        "~/Content/slider.css"));
        }
    }
}
