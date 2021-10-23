using System.Web.Mvc;
using System.Web.Routing;

namespace MNPContactAPI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "GetRoute",
                url: "Contacts({id})/{action}",
                defaults: new { action = "Get" }
            );
        }
    }
}
