using System.Web.Optimization;

namespace WebDisk.Web
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

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                    "~/Scripts/site/shared/bootstrap.min.js",
                    "~/Scripts/site/shared/ripples.min.js",
                    "~/Scripts/site/shared/material.min.js",
                    "~/Scripts/site/shared/snackbar.min.js",
                    "~/Scripts/site/shared/moment.js",
                    "~/Scripts/site/shared/jquery.nouislider.min.js",
                    "~/Scripts/site/shared/bootstrap-material-datetimepicker.js",
                    "~/Scripts/site/scripts.js"
               ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/libraries/bootstrap.min.css",
                      "~/Content/libraries/bootstrap-material-design.min.css",
                      "~/Content/libraries/ripples.min.css",
                      "~/Content/libraries/ripples.min.css",
                      "~/Content/libraries/icon.css",
                      "~/Content/libraries/bootstrap-material-datetimepicker.css",
                      "~/Content/site.min.css"));
        }
    }
}
