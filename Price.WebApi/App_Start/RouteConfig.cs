using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Swashbuckle.Application;

namespace Price.WebApi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                name: "swagger_root",
                routeTemplate: "",
                defaults: null,
                constraints: null,
                handler: new RedirectHandler((message => message.RequestUri.ToString()), "swagger"));

            //routes.MapRoute(
            //    "Default",
            //    "{controller}/{action}/{id}",
            //    new {controller = "Home", action = "Index", id = UrlParameter.Optional}
            //    );
        }
    }
}