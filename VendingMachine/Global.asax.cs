using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using VendingMachine.Repository;

namespace VendingMachine
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var dbContext = new VendingMachineContext();
            dbContext.Database.Initialize(true);
            dbContext.Database.CreateIfNotExists();
        }
    }
}
