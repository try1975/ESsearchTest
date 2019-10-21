using System.Web.Http;
using System.Web.Http.OData.Extensions;
using System.Web.Http.Cors;

namespace Price.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional}
                );

            config.Formatters.XmlFormatter.UseXmlSerializer = true;
            //config.Formatters.JsonFormatter.UseDataContractJsonSerializer = true;

            // OData query options
            //config.EnableQuerySupport();
            config.AddODataQueryFilter();

            var corsAttr = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsAttr);
        }
    }
}