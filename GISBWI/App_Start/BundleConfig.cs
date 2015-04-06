using System.Web;
using System.Web.Optimization;

namespace GISBWI
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.UseCdn = true;

            //add link to jquery on the CDN
            var jqueryCdnPath = "http://ajax.googleapis.com/ajax/libs/";

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            /*CUSTOM ADMIN CSS*/


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap-datetimepicker.css",
                        "~/Content/bootstrap.css",
                        "~/Content/font-awesome.css",
                        "~/Content/fullcalendar.css",
                        "~/Content/jquery-ui.css",
                        "~/Content/jquery.cleditor.css",
                        "~/Content/jquery.dataTables.css",
                        "~/Content/jquery.onoff.css",
                        "~/Content/prettyPhoto.css",
                        "~/Content/rateit.css",
                        "~/Content/style.css",
                        "~/Content/widgets.css",
                         "~/Content/fullcalendar.css",
                          "~/Content/prettyPhoto.css",
                          "~/Content/rateit.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new StyleBundle("~/Content/FrontEnd/css").Include(                    
                      "~/Content/FrontEnd/bootstrap.css",
                     "~/Content/font-awesome.css",
                      "~/Content/FrontEnd/animate.css",
                      "~/Content/FrontEnd/yamm.css",
                      "~/Content/FrontEnd/jquery.bootstrap-touchspin.css",
                      "~/Content/FrontEnd/owl.theme.css",
                      "~/Content/FrontEnd/owl.transitions.css",
                      "~/Content/FrontEnd/owl.carousel.css",
                      "~/Content/FrontEnd/magnific-popup.css",
                      "~/Content/FrontEnd/creative-brands.css",
                      "~/Content/FrontEnd/color-switcher.css",
                      "~/Content/FrontEnd/color.css",
                      "~/Content/FrontEnd/custom.css"
                     ));

            //Css Bootstrap
            bundles.Add(new StyleBundle("~/Content/Bootstrap/css").Include(
                "~/Content/Bootstrap/bootstrap.css",
                "~/Content/Bootstrap/bootstrap-responsive.css",
                "~/Content/Bootstrap/bootstrap-image-gallery.css"));

            bundles.Add(new StyleBundle("~/FileUpload/css").Include(
                "~/Content/FileUpload/jquery.fileupload-ui.css"));

            //Old Script
            bundles.Add(new ScriptBundle("~/Scripts/googleCDN", jqueryCdnPath).Include(
                "~/Scripts/jquery/1.7.1/jquery.min.js",
                "~/Scripts/jqueryui/1.8.18/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/Bootstrap").Include(
                "~/Scripts/Bootstrap/bootstrap.js"));
            //"~/Scripts/Bootstrap/bootstrap-image-gallery.js"));

            bundles.Add(new ScriptBundle("~/Scripts/FileUpload").Include(
                "~/Scripts/FileUpload/vendor/jquery.ui.widget.js",
                "~/Scripts/FileUpload/tmpl.js",
                "~/Scripts/FileUpload/load-image.js",
                "~/Scripts/FileUpload/canvas-to-blob.js",
                "~/Scripts/FileUpload/jquery.iframe-transport.js",
                "~/Scripts/FileUpload/jquery.fileupload.js",
                "~/Scripts/FileUpload/jquery.fileupload-ip.js",
                "~/Scripts/FileUpload/jquery.fileupload-ui.js",
                "~/Scripts/FileUpload/locale.js",
                "~/Scripts/FileUpload/main.js"));
        }
    }
}