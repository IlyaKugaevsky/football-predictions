using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Predictions.FrontEnd.Views;
using Predictions.Windsor.Plumbing;


namespace Predictions
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer _container;

        private static void BootstrapContainer()
        {
            _container = new WindsorContainer()
                .Install(FromAssembly.This());
            var controllerFactory = new WindsorControllerFactory(_container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BootstrapContainer();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ViewEngines.Engines.Add(new CustomViewEngine());
        }

        protected void Application_End()
        {
            _container.Dispose();
        }
    }
}
