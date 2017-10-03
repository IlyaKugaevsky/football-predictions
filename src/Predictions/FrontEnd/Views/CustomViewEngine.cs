using System.Web.Mvc;

namespace Predictions.FrontEnd.Views
{
    public class CustomViewEngine: RazorViewEngine
    {
        public CustomViewEngine()
        {
            var viewLocations = new[] {
            //"~/Views/{1}/{0}.cshtml ",
            //"~/Views/Shared/{0}.cshtml ",
            "~/FrontEnd/Views/{1}/{0}.cshtml",
            "~/FrontEnd/Views/Shared/{0}.cshtml"
        };

            this.PartialViewLocationFormats = viewLocations;
            this.ViewLocationFormats = viewLocations;
        }
    }
}