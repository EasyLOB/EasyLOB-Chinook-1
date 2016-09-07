using EasyLOB.Library.Mvc;
using System.Web.Optimization;

namespace Chinook.Mvc
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles) // !!!
        {
            Bundle bundle;

            // Force Bundles in Debug Mode
            //BundleTable.EnableOptimizations = true;

            // CSS

            bundles.Add(new StyleBundle("~/content/z-css.css")
                .Include("~/Content/bootstrap.css") // .css + .min.css
                .Include("~/Content/bootstrap-theme.css") // .css + .min.css
                .Include("~/Content/site.css")
            );

            bundles.Add(new StyleBundle("~/content/ej/web/z-syncfusion.css")
                .Include("~/Content/ej/web/ej.widgets.core.css") // .css + .min.css
                .Include("~/Content/ej/web/ej.widgets.core.bootstrap.css") // .css + .min.css
            );

            bundles.Add(new StyleBundle("~/content/ej/web/" + EasyLOB.AppDefaults.SyncfusionTheme + "/z-syncfusion-theme.css")
                .Include("~/Content/ej/web/" + EasyLOB.AppDefaults.SyncfusionTheme + "/ej.theme.css") // .css + .min.css
            );

            bundles.Add(new StyleBundle("~/content/jQuery.FileUpload/css/z-jquery-fileupload.css")
                .Include("~/Content/jQuery.FileUpload/css/jquery.fileupload.css")
                .Include("~/Content/jQuery.FileUpload/css/jquery.fileupload-ui.css")
            );

            bundles.Add(new StyleBundle("~/content/easylob/z-easylob.css")
                .Include("~/Content/easylob/easylob.css")
            );

            // JavaScript

            // Bootstrap

            bundle = new Bundle("~/scripts/z-bootstrap.js");
            bundle.Orderer = new AsIsBundleOrderer();
            bundle
                .Include("~/Scripts/bootstrap.js") // .js + .min.js
                .Include("~/Scripts/respond.js") // .js + .min.js
                .Include("~/Scripts/respond.matchmedia.addListener.js"); // .js + .min.js
            bundles.Add(bundle);

            // jQuery

            bundle = new Bundle("~/scripts/z-jquery.js");
            bundle.Orderer = new AsIsBundleOrderer();
            bundle
                // 1st
                .Include("~/Scripts/jquery-1.10.2.js") // .js + .min.js
                .Include("~/Scripts/jquery.easing.1.3.js")
                // 2nd
                .Include("~/Scripts/jquery.validate.js") // .js + .min.js
                .Include("~/Scripts/jquery.validate.unobtrusive.js") // .js + .min.js
                // 3rd
                .Include("~/Scripts/globalize/globalize.js")
                .Include("~/Scripts/globalize/cultures/globalize.culture.en-US.js")
                .Include("~/Scripts/globalize/cultures/globalize.culture.pt-BR.js");
            bundles.Add(bundle);

            // Modernizr

            bundles.Add(new ScriptBundle("~/scripts/z-modernizr.js")
                .Include("~/Scripts/modernizr-2.8.3.js")
            );

            // Syncfusion

            bundle = new Bundle("~/scripts/z-syncfusion.js");
            bundle.Orderer = new AsIsBundleOrderer();
            bundle
                .Include("~/Scripts/syncfusion/jsrender.js") // .js + .min.js
                .Include("~/Scripts/ej/ej.web.all.js") // .js + .min.js
                .Include("~/Scripts/ej/ej.globalize.js") // .js + .min.js
                .Include("~/Scripts/ej/ej.unobtrusive.js") // .js + .min.js
                // en-US
                .Include("~/Scripts/ej/cultures/ej.culture.en-US.js") // .js + .min.js
                .Include("~/Scripts/ej/cultures/ej.excelfilter.locale.en-US.js")
                .Include("~/Scripts/ej/cultures/ej.grid.locale.en-US.js")
                .Include("~/Scripts/ej/cultures/ej.pager.locale.en-US.js")
                .Include("~/Scripts/ej/cultures/ej.reportviewer.locale.en-US.js")
                // pt-BR
                .Include("~/Scripts/ej/cultures/ej.culture.pt-BR.js") // .js + .min.js
                .Include("~/Scripts/ej/cultures/ej.excelfilter.locale.pt-BR.js")
                .Include("~/Scripts/ej/cultures/ej.grid.locale.pt-BR.js")
                .Include("~/Scripts/ej/cultures/ej.pager.locale.pt-BR.js")
                .Include("~/Scripts/ej/cultures/ej.reportviewer.locale.pt-BR.js");
            bundles.Add(bundle);

            // EasyLOB

            bundle = new Bundle("~/scripts/z-easylob.js");
            bundle.Orderer = new AsIsBundleOrderer();
            bundle
                .Include("~/Scripts/easylob/easylob.js");
            bundles.Add(bundle);
        }
    }
}
