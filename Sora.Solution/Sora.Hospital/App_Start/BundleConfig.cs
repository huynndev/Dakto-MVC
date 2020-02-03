using System.Web;
using System.Web.Optimization;

namespace Sora.Hospital
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/Adminty/bower_components/jquery/js/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/Adminty/bower_components/popper.js/js/popper.min.js",
                      "~/Content/Adminty/bower_components/bootstrap/js/bootstrap.min.js",
                      "~/Scripts/respond.js",
                      "~/Content/assets/lib/perfect-scrollbar/js/perfect-scrollbar.jquery.min.js",
                      "~/Content/Adminty/assets/sweetalert/sweetalert/js/sweetalert.js"
                      )); ;

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/assets/lib/perfect-scrollbar/css/perfect-scrollbar.min.css"));

            bundles.Add(new StyleBundle("~/Content/FormValidationJS").Include(
                      "~/Content/assets/lib/form-validation/css/formValidation.min.css"));

            //Adminty Admin CSS
            bundles.Add(new StyleBundle("~/Content/SimpleAdminCSS").Include(
                        "~/Content/Adminty/bower_components/bootstrap/css/bootstrap.min.css",
                        "~/Content/Adminty/assets/icon/themify-icons/themify-icons.css",
                        "~/Content/Adminty/assets/icon/icofont/css/icofont.css",
                        "~/Content/Adminty/assets/icon/feather/css/feather.css",
                        "~/Content/Adminty/assets/icon/typicons-icons/css/typicons.min.css",
                        "~/Content/Adminty/assets/css/style.css",
                        "~/Content/Adminty/assets/pages/prism/prism.css",
                        "~/Content/Adminty/assets/css/jquery.mCustomScrollbar.css",
                        "~/Content/Adminty/assets/sweetalert/sweetalert/css/sweetalert.min.css"
                        ));
            bundles.Add(new StyleBundle("~/Content/SimpleAdminDatatableCSS").Include(
                        "~/Content/Adminty/bower_components/datatables.net-bs4/css/dataTables.bootstrap4.min.css",
                        "~/Content/Adminty/assets/pages/data-table/css/buttons.dataTables.min.css",
                        "~/Content/Adminty/ower_components/datatables.net-responsive-bs4/css/responsive.bootstrap4.min.css"
                        ));



            bundles.Add(new ScriptBundle("~/Content/SimpleAdminScript").Include(
                    "~/Content/Adminty/bower_components/jquery-ui/js/jquery-ui.min.js",
                    "~/Content/Adminty/bower_components/popper.js/js/popper.min.js",
                    "~/Content/Adminty/bower_components/jquery-slimscroll/js/jquery.slimscroll.js",
                    "~/Content/Adminty/bower_components/modernizr/js/modernizr.js",
                    "~/Content/Adminty/bower_components/modernizr/js/css-scrollbars.js",
                    "~/Content/Adminty/assets/pages/prism/custom-prism.js",
                    "~/Content/Adminty/bower_components/i18next/js/i18next.min.js",
                    "~/Content/Adminty/bower_components//i18next-xhr-backend/js/i18nextXHRBackend.min.js",
                    "~/Content/Adminty/bower_components/i18next-browser-languagedetector/js/i18nextBrowserLanguageDetector.min.js",
                    "~/Content/Adminty/bower_components/jquery-i18next/js/jquery-i18next.min.js",
                    "~/Content/Adminty/assets/js/pcoded.min.js",
                    "~/Content/Adminty/assets/js/vartical-layout.min.js",
                    "~/Content/Adminty/assets/js/jquery.mCustomScrollbar.concat.min.js",
                    "~/Content/assets/js/bootbox.min.js",
                    "~/Content/Adminty/assets/js/script.js"
                    ));

            bundles.Add(new ScriptBundle("~/Content/SimpleAdminDatatableScript").Include(
                  "~/Content/Adminty/bower_components/datatables.net/js/jquery.dataTables.min.js",
                  "~/Content/Adminty/bower_components/datatables.net-buttons/js/dataTables.buttons.min.js",
                  "~/Content/Adminty/assets/pages/data-table/js/jszip.min.js",
                  "~/Content/Adminty/assets/pages/data-table/js/pdfmake.min.js",
                  "~/Content/Adminty/pages/data-table/js/vfs_fonts.js",
                  "~/Content/Adminty/bower_components/datatables.net-buttons/js/buttons.print.min.js",
                  "~/Content/Adminty/bower_components/datatables.net-buttons/js/buttons.html5.min.js",
                  "~/Content/Adminty/bower_components/datatables.net-bs4/js/dataTables.bootstrap4.min.js",
                  "~/Content/Adminty/bower_components/datatables.net-responsive/js/dataTables.responsive.min.js",
                  "~/Content/Adminty/bower_components/datatables.net-responsive-bs4/js/responsive.bootstrap4.min.js",
                  "~/Content/Adminty/pages/data-table/js/data-table-custom.js"
                  ));



            //manager
            /*-----style--------*/
            bundles.Add(new StyleBundle("~/Content/ManagerCSSCommon").Include(
          "~/Content/assets/lib/boostrap/css/bootstrap.min.css",
          "~/Content/assets/lib/perfect-scrollbar/css/perfect-scrollbar.min.css",
          "~/Content/assets/lib/material-design-icons/css/material-design-iconic-font.min.css",
          "~/Content/assets/lib/datetimepicker/css/bootstrap-datetimepicker.min.css",
          "~/Content/assets/lib/daterangepicker/css/daterangepicker.css",
          "~/Content/assets/lib/datatables/css/dataTables.bootstrap.min.css",
          "~/Content/assets/lib/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css",
          "~/Content/assets/css/style.css",
          "~/Content/assets/css/manager.css"
          ));
            bundles.Add(new ScriptBundle("~/Content/ManagerCSS/jqvmap").Include(
                    "~/Content/assets/lib/jquery.vectormap/jquery-jvectormap-1.2.2.css",
                    "~/Content/assets/lib/jqvmap/jqvmap.min.css"
                ));

            /*----Script-------*/
            bundles.Add(new ScriptBundle("~/Content/ManagerJSCommon1").Include(
                      "~/Content/assets/lib/jquery/jquery.min.js",
                      "~/Content/assets/lib/perfect-scrollbar/js/perfect-scrollbar.jquery.min.js",
                      "~/Content/assets/lib/boostrap/js/bootstrap.min.js",
                      "~/Content/assets/js/bootbox.min.js",
                      "~/Content/assets/js/manager.js"
                      ));
            bundles.Add(new ScriptBundle("~/Content/ManagerJS/datatables").Include(
                      "~/Content/assets/lib/datatables/js/jquery.dataTables.min.js",
                      "~/Content/assets/lib/datatables/js/dataTables.bootstrap.min.js",
                      "~/Content/assets/lib/datatables/plugins/buttons/js/dataTables.buttons.js",
                      "~/Content/assets/lib/datatables/plugins/buttons/js/buttons.html5.js",
                      "~/Content/assets/lib/datatables/plugins/buttons/js/buttons.flash.js",
                      "~/Content/assets/lib/datatables/plugins/buttons/js/buttons.print.js",
                      "~/Content/assets/lib/datatables/plugins/buttons/js/buttons.colVis.js",
                      "~/Content/assets/lib/datatables/plugins/buttons/js/buttons.bootstrap.js",
                      "~/Content/assets/js/app-tables-datatables.js"
                     ));

            bundles.Add(new ScriptBundle("~/Content/ManagerJSCommon").Include(
                      "~/Content/assets/js/main.js"));

            bundles.Add(new ScriptBundle("~/Content/ManagerJS/jqueryflot").Include(
                     "~/Content/assets/lib/jquery-flot/jquery.flot.js",
                     "~/Content/assets/lib/jquery-flot/jquery.flot.pie.js",
                     "~/Content/assets/lib/jquery-flot/jquery.flot.resize.js",
                     "~/Content/assets/lib/jquery-flot/plugins/jquery.flot.orderBars.js",
                     "~/Content/assets/lib/jquery-flot/plugins/curvedLines.js"));

            bundles.Add(new ScriptBundle("~/Content/ManagerJS/jqvmap").Include(
                    "~/Content/assets/lib/jqvmap/jquery.vmap.min.js",
                    "~/Content/assets/lib/jqvmap/maps/jquery.vmap.world.js"));

            bundles.Add(new ScriptBundle("~/Content/ManagerJS/countup").Include(
                    "~/Content/assets/lib/countup/countUp.min.js"));

            bundles.Add(new ScriptBundle("~/Content/ManagerJS/sparkline").Include(
                    "~/Content/assets/lib/jquery.sparkline/jquery.sparkline.min.js"));

            bundles.Add(new ScriptBundle("~/Content/ManagerJS/jqueryUi").Include(
                   "~/Content/assets/lib/jquery-ui/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/Content/ManagerJS/dashboard").Include(
                    "~/Content/assets/js/app-dashboard.js"));

            bundles.Add(new ScriptBundle("~/Content/ManagerJS/Check").Include(
                    "~/Content/assets/js/jsCheckInput.js"));

            bundles.Add(new ScriptBundle("~/Content/FormValidationJS").Include(
                    "~/Content/assets/lib/form-validation/js/formValidation.min.js",
                    "~/Content/assets/lib/form-validation/validate/bootstrap.min.js"
                    ));

            bundles.Add(new StyleBundle("~/Content/FormValidationCSS").Include(
                      "~/Content/assets/lib/form-validation/js/formValidation.min.css"));

            bundles.Add(new ScriptBundle("~/Content/ManagerJS/Check").Include(
                    "~/Content/assets/js/jsCheckInput.js"));

            bundles.Add(new ScriptBundle("~/Content/ManagerJS/parsley").Include(
                    "~/Content/assets/lib/parsley/parsley.min.js"));

            bundles.Add(new StyleBundle("~/bundles/css/counselingDetail").Include(
                "~/Content/assets/css/select2.min.css",
                "~/Content/assets/css/style-counseling-detail.css",
                "~/Content/Adminty/assets/timepicker/css/bootstrap-timepicker.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/javascript/counselingDetail").Include(
                "~/Content/assets/js/select2.full.min.js",
                "~/Content/assets/js/select2-custom.js",
                "~/Content/Adminty/assets/timepicker/js/bootstrap-timepicker.min.js",
                "~/Content/assets/js/js-counseling-detail.js"));

            bundles.Add(new StyleBundle("~/bundles/css/counselingSchedule").Include(
                "~/Content/assets/css/counseling-schedule.css",
                "~/Content/fullcalendar-4.2.0/packages/core/main.css",
                "~/Content/fullcalendar-4.2.0/packages/daygrid/main.css",
                "~/Content/fullcalendar-4.2.0/packages/timegrid/main.css",
                "~/Content/fullcalendar-4.2.0/packages/list/main.css",
                "~/Content/assets/css/select2.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/javascript/counselingSchedule").Include(
                "~/Content/fullcalendar-4.2.0/packages/core/main.js",
                "~/Content/fullcalendar-4.2.0/packages/interaction/main.js",
                "~/Content/fullcalendar-4.2.0/packages/daygrid/main.js",
                "~/Content/fullcalendar-4.2.0/packages/timegrid/main.js",
                "~/Content/fullcalendar-4.2.0/packages/list/main.js",
                "~/Content/fullcalendar-4.2.0/packages/core/popper.min.js",
                "~/Content/fullcalendar-4.2.0/packages/core/tooltip.min.js",
                "~/Content/assets/js/select2.full.min.js",
                "~/Content/assets/js/select2-custom.js"));
        }
    }
}
