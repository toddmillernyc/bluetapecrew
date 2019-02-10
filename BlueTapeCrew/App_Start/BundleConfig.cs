using System.Web.Optimization;

namespace BlueTapeCrew
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bluetape/homecss")
                .Include(
                    "~/Content/fontawesome-all.css",
                    "~/Content/bootstrap.min.css")
                .Include("~/Content/jquery.fancybox.css", new CssRewriteUrlTransform())
                .Include("~/Content/OwlCarousel/owl.carousel.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bluetape/productcss")
                .Include(
                         "~/Content/fontawesome-all.min.css",
                         "~/Content/bootstrap.min.css")
                .Include("~/Content/jquery.fancybox.css", new CssRewriteUrlTransform())
                .Include("~/Content/OwlCarousel/owl.carousel.css")
                .Include("~/Content/Themes/uniformjs/default/css/uniform.default.css", new CssRewriteUrlTransform())
                .Include("~/Content/rateit.css",new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bluetape/css")
                .Include("~/Content/Themes/uniformjs/default/css/uniform.default.css", new CssRewriteUrlTransform())
                .Include(
                    "~/Content/fontawesome-all.css",
                    "~/Content/bootstrap.min.css")
                    .Include("~/Content/jquery.fancybox.css", new CssRewriteUrlTransform())
                    .Include(
                    "~/Content/OwlCarousel/owl.carousel.css", new CssRewriteUrlTransform())
                .Include(
                    "~/Content/rateit.css",new CssRewriteUrlTransform())
                 .IncludeDirectory("~/Content/css","*.css",false));

            bundles.Add(new ScriptBundle("~/bluetape/homejs")
                .Include(
                    "~/Scripts/jquery.validate.js",
                    "~/Scripts/jquery.validate.unobtrusive.js",
                    "~/Scripts/bootstrap.min.js",
                    "~/Scripts/back-to-top.js",
                    "~/Scripts/jquery.slimscroll.min.js",
                    "~/Scripts/jquery.fancybox.pack.js",
                    "~/Scripts/owl.carousel.min.js",
                    "~/Scripts/bootstrap-touchspin/bootstrap.touchspin.js",
                    "~/Scripts/layout.js",
                    "~/Scripts/app.js"));

            bundles.Add(new ScriptBundle("~/bluetape/productjs")
                .Include(
                    "~/Scripts/bootstrap.min.js",
                    "~/Scripts/back-to-top.js",
                    "~/Scripts/jquery.slimscroll.min.js",
                    "~/Scripts/jquery.fancybox.pack.js",
                    "~/Scripts/owl.carousel.min.js",
                    "~/Scripts/zoom/jquery.zoom.min.js",
                    "~/Scripts/bootstrap-touchspin/bootstrap.touchspin.js")
                .Include("~/Scripts/jquery.uniform.min.js")
                .Include(
                    "~/Scripts/jquery.rateit.min.js",
                    "~/Scripts/layout.js",
                    "~/Scripts/app.js"));

            bundles.Add(new ScriptBundle("~/bluetape/js")
                .Include(
                    "~/Scripts/bootstrap.min.js",
                    "~/Scripts/jquery.slimscroll.min.js",
                    "~/Scripts/back-to-top.js",
                    "~/Scripts/jquery.fancybox.pack.js",
                    "~/Scripts/owl.carousel.min.js",
                    "~/Scripts/zoom/jquery.zoom.min.js",
                    "~/Scripts/bootstrap-touchspin/bootstrap.touchspin.js")
                .Include("~/Scripts/jquery.uniform.min.js")
                .Include(
                    "~/Scripts/jquery.rateit.min.js",
                    "~/Scripts/layout.js",
                    "~/Scripts/app.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}





