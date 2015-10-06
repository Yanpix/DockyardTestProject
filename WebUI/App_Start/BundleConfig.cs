using System.Web.Optimization;

namespace WebUI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            bundles.Add(new ScriptBundle("~/Bundles/scripts").Include(
                "~/Scripts/jquery-{version}.min.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/modernizr-2.6.2.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/sticky-footer.css",
                "~/Content/Site.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
