using System.Web;
using System.Web.Optimization;

namespace Proyecto2._0
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios.  De esta manera estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/Plugin/css").Include(
                      "~/Content/dataTable/css/jquery.dataTables.min.css",
                      "~/Content/fontawesome-free/css/all.css",
                      "~/Content/dataTable/css/buttons.dataTables.min.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/Plugin/js").Include(
                      "~/Content/dataTable/js/jquery.dataTables.min.js",
                      "~/Content/fontawesome-free/js/all.js",
                      "~/Content/dataTable/js/dataTables.buttons.min.js"
                      ));
        }
    }
}
