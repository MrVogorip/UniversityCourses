using Ninject.Modules;
using Ninject;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TourAgency.Web.Ioc;
using Ninject.Web.Mvc;
using TourAgency.Bll.Infrastructure;
using TourAgency.Web.Helpers;

namespace TourAgency.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            SLogger.StartLog();
            NinjectModule orderModule = new TourAgencyModule();
            NinjectModule serviceModule = new ServiceModule("DefaultConnection");
            var kernel = new StandardKernel(orderModule, serviceModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
