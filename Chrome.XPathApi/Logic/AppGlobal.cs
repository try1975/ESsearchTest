using System.IO;
using System.Web;
using System.Web.Configuration;

namespace Chrome.XPathApi.Logic
{
    public static class AppGlobal
    {
        static AppGlobal()
        {
            ElangPath = WebConfigurationManager.AppSettings[nameof(ElangPath)];
            if (string.IsNullOrEmpty(ElangPath)) ElangPath = nameof(ElangPath);
            ElangPath = Path.Combine(HttpRuntime.AppDomainAppPath, ElangPath);
            //ElangPath = System.Web.Hosting.HostingEnvironment.MapPath($"~/{ElangPath}");
            Directory.CreateDirectory(ElangPath);

            //CsvPath = WebConfigurationManager.AppSettings[nameof(CsvPath)];
            //if (string.IsNullOrEmpty(CsvPath)) CsvPath = nameof(CsvPath);
            //CsvPath = Path.Combine(HttpRuntime.AppDomainAppPath, CsvPath);
            //Directory.CreateDirectory(CsvPath);

            //UrlPath = WebConfigurationManager.AppSettings[nameof(UrlPath)];
            //if (string.IsNullOrEmpty(UrlPath)) UrlPath = nameof(UrlPath);
            //UrlPath = Path.Combine(HttpRuntime.AppDomainAppPath, UrlPath);
            //Directory.CreateDirectory(UrlPath);

            //UrlStatePath = WebConfigurationManager.AppSettings[nameof(UrlStatePath)];
            //if (string.IsNullOrEmpty(UrlStatePath)) UrlStatePath = nameof(UrlStatePath);
            //UrlStatePath = Path.Combine(HttpRuntime.AppDomainAppPath, UrlStatePath);
            //Directory.CreateDirectory(UrlStatePath);

            //ScreenshotPath = WebConfigurationManager.AppSettings[nameof(ScreenshotPath)];
            //if (string.IsNullOrEmpty(ScreenshotPath)) UrlStatePath = nameof(ScreenshotPath);
            //ScreenshotPath = Path.Combine(HttpRuntime.AppDomainAppPath, ScreenshotPath);
            //Directory.CreateDirectory(ScreenshotPath);

            //ExternalToken = WebConfigurationManager.AppSettings[nameof(ExternalToken)];

            //ProductParser = Path.Combine(HttpRuntime.AppDomainAppPath, @"ProductParser\ProductParserCon.exe");
            //Screenshotter = Path.Combine(HttpRuntime.AppDomainAppPath, @"SiteShoter\SiteShoter.exe");
        }

        public static string ElangPath { get; }
        public static string CsvPath { get; }
        public static string UrlPath { get; }
        public static string UrlStatePath { get; }

        public static string ScreenshotPath { get; }
        public static string ExternalToken { get; }

        public static string ProductParser { get; }
        public static string Screenshotter { get; }
    }
}