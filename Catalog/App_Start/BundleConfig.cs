using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace Catalog
{
  public  class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js")
                .Include("~/Content/js/bootstrap.js", "~/Content/js/jquery.js"));
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                    "~/Content/css/bootstrap.css", "~/Content/css/shop-homepage.css"));
        }
    }
}
