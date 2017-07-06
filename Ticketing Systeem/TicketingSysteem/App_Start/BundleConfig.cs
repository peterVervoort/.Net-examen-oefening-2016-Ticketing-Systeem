using System.Web.Optimization;

namespace TicketingSysteem
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/app/app.js",
                "~/app/user/*.js",
                "~/app/directives/*.js",
                "~/app/services/*.js",
                "~/app/login/*.js",
                "~/app/annulatiereden/*.js",
                "~/app/topbar/*.js",
                "~/app/issue/*.js",
                "~/app/issuestatus/*.js",
                "~/app/extrainfo/*.js",
                "~/app/oplossing/*.js"
            ));


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-1.10.2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/angular.js",
                "~/Scripts/angular-ui-router.js",
                "~/Scripts/angular-animate.min.js",
                "~/Scripts/angular-mocks.js",
                "~/Scripts/angular-cookies.min.js",
                "~/Scripts/angular-translate.min.js",
                "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js",
                "~/Scripts/toaster.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));



            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/respond.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/bootstrap/css/bootstrap.min.css",
                 "~/Content/css/waves.min.css",
                 "~/Content/css/nanoscroller.css",
                 "~/Content/css/morris-0.4.3.min.css",
                 "~/Content/css/menu-light.css",
                 "~/Content/css/style.css",
                 "~/Content/font-awesome/css/font-awesome.min.css",
                 "~/Content/css/app.min.1.css",
                 "~/Content/css/fullcalendar.min.css",
                 "~/Content/css/themify-icons.css",
                 "~/Content/css/color.css",
                 "~/Content/toaster.min.css",
                 "~/Content/Site.css"));
        }
    }
}
