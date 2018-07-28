using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Newtonsoft.Json;
using Price.WebApi.Jobs;

namespace Price.WebApi
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {

            var win1251Encoding = Encoding.GetEncoding("windows-1251");
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedEncodings.Add(win1251Encoding);
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedEncodings.RemoveAt(0);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var config = GlobalConfiguration.Configuration;
            //
            config.Formatters.JsonFormatter
                .SerializerSettings
                .ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            // запуск выполнения работы
            TopolScheduler.Start();
        }
    }
}