using System;
using System.IO;
using System.Web;
using System.Web.Configuration;

namespace Price.WebApi.Logic
{
    public static class AppGlobal
    {
        static AppGlobal()
        {
            ElangPath = WebConfigurationManager.AppSettings[nameof(ElangPath)];
            if (string.IsNullOrEmpty(ElangPath)) ElangPath = nameof(ElangPath);
            ElangPath = Path.Combine(HttpRuntime.AppDomainAppPath, ElangPath);
            Directory.CreateDirectory(ElangPath);

            CsvPath = WebConfigurationManager.AppSettings[nameof(CsvPath)];
            if (string.IsNullOrEmpty(CsvPath)) CsvPath = nameof(CsvPath);
            CsvPath = Path.Combine(HttpRuntime.AppDomainAppPath, CsvPath);
            Directory.CreateDirectory(CsvPath);

            UrlPath = WebConfigurationManager.AppSettings[nameof(UrlPath)];
            if (string.IsNullOrEmpty(UrlPath)) UrlPath = nameof(UrlPath);
            UrlPath = Path.Combine(HttpRuntime.AppDomainAppPath, UrlPath);
            Directory.CreateDirectory(UrlPath);

            UrlStatePath = WebConfigurationManager.AppSettings[nameof(UrlStatePath)];
            if (string.IsNullOrEmpty(UrlStatePath)) UrlStatePath = nameof(UrlStatePath);
            UrlStatePath = Path.Combine(HttpRuntime.AppDomainAppPath, UrlStatePath);
            Directory.CreateDirectory(UrlStatePath);

            InternetSearchResultPath = WebConfigurationManager.AppSettings[nameof(InternetSearchResultPath)];
            if (string.IsNullOrEmpty(InternetSearchResultPath)) InternetSearchResultPath = nameof(InternetSearchResultPath);
            InternetSearchResultPath = Path.Combine(HttpRuntime.AppDomainAppPath, InternetSearchResultPath);
            Directory.CreateDirectory(InternetSearchResultPath);

            ScreenshotPath = WebConfigurationManager.AppSettings[nameof(ScreenshotPath)];
            if (string.IsNullOrEmpty(ScreenshotPath)) UrlStatePath = nameof(ScreenshotPath);
            ScreenshotPath = Path.Combine(HttpRuntime.AppDomainAppPath, ScreenshotPath);
            Directory.CreateDirectory(ScreenshotPath);

            ExternalToken = WebConfigurationManager.AppSettings[nameof(ExternalToken)];

            ProductParser = Path.Combine(HttpRuntime.AppDomainAppPath, @"ProductParser\ProductParserCon.exe");
            Screenshotter = Path.Combine(HttpRuntime.AppDomainAppPath, @"SiteShoter\SiteShoter.exe");
            AnalystCon = Path.Combine(HttpRuntime.AppDomainAppPath, @"AnalystCon\AnalystCon.exe");
            //AnalystCon = WebConfigurationManager.AppSettings[nameof(AnalystCon)];

            CashSeconds = Convert.ToInt32(WebConfigurationManager.AppSettings[nameof(CashSeconds)]); 
        }

        public static string ElangPath { get; }
        public static string CsvPath { get; }
        public static string UrlPath { get; }
        public static string UrlStatePath { get; }

        public static string ScreenshotPath { get; }
        public static string ExternalToken { get; }

        public static string ProductParser { get; }
        public static string Screenshotter { get; }
        public static string AnalystCon { get; }
        public static string InternetSearchResultPath { get; set; }

        public static int CashSeconds { get; set; }
    }
}