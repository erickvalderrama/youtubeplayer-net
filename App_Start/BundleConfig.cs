using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace AFEXChile
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/WebFormsJs").Include(
                            "~/Content/Scripts/WebForms/WebForms.js",
                            "~/Content/Scripts/WebForms/WebUIValidation.js",
                            "~/Content/Scripts/WebForms/MenuStandards.js",
                            "~/Content/Scripts/WebForms/Focus.js",
                            "~/Content/Scripts/WebForms/GridView.js",
                            "~/Content/Scripts/WebForms/DetailsView.js",
                            "~/Content/Scripts/WebForms/TreeView.js",
                            "~/Content/Scripts/WebForms/WebParts.js"));

            // El orden es muy importante para el funcionamiento de estos archivos ya que tienen dependencias explícitas
            bundles.Add(new ScriptBundle("~/bundles/MsAjaxJs").Include(
                    "~/Content/Scripts/WebForms/MsAjax/MicrosoftAjax.js",
                    "~/Content/Scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.js",
                    "~/Content/Scripts/WebForms/MsAjax/MicrosoftAjaxTimer.js",
                    "~/Content/Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.js"));

            // Use la versión de desarrollo de Modernizr para desarrollar y aprender. Luego, cuando esté listo
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                            "~/Content/Scripts/modernizr-*"));
        }
    }
}