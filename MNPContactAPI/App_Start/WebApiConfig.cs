using System.Web.Http;

namespace MNPContactAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Get",
                routeTemplate: "api/{controller}({id})",
                defaults: new { controller = "Contacts", action = "Get" }
            );

            config.Routes.MapHttpRoute(
                name: "Update",
                routeTemplate: "api/{controller}({id})/Post",
                defaults: new { controller = "Contacts", action = "Post" }
            );

            config.Routes.MapHttpRoute(
                name: "List",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { action = "List" }
            );
        }
    }
}
