using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Predictions.ViewModels.Basis
{
    public class ActionLinkParams
    {
        public string LinkText { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public object RouteValues { get; set; }
        public object HtmlAttributes { get; set; }

        public ActionLinkParams()
        { }

        public ActionLinkParams(string linkText, string actionName, string controllerName, object routeValues = null, object htmlAttributes = null)
        {
            LinkText = linkText;
            ActionName = actionName;
            ControllerName = controllerName;
            RouteValues = routeValues;
            HtmlAttributes = htmlAttributes;
        }
    }
}