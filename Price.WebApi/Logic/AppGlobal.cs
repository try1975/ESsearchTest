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

            Screenshots = WebConfigurationManager.AppSettings[nameof(Screenshots)];
            if (string.IsNullOrEmpty(Screenshots)) Screenshots = nameof(Screenshots);
            ScreenshotPath = Path.Combine(HttpRuntime.AppDomainAppPath, Screenshots);
            Directory.CreateDirectory(ScreenshotPath);
            ScreenshotterArgs = WebConfigurationManager.AppSettings[nameof(ScreenshotterArgs)];
            if (string.IsNullOrEmpty(ScreenshotterArgs)) ScreenshotterArgs = "/OpenImageAfterSave 0 /MaxBrowserWidth 2000 /MaxBrowserHeight 10000 /BrowserTimeout 5000 /JpegQuality 50 /ImageSizePerCent 75 /DisableScrollBars 1";
            ScreenshotExtension = WebConfigurationManager.AppSettings[nameof(ScreenshotExtension)];
            if (string.IsNullOrEmpty(ScreenshotExtension)) ScreenshotExtension = "jpg";

            ExternalToken = WebConfigurationManager.AppSettings[nameof(ExternalToken)];

            ProductParser = Path.Combine(HttpRuntime.AppDomainAppPath, @"ProductParser\ProductParserCon.exe");
            Screenshotter = Path.Combine(HttpRuntime.AppDomainAppPath, @"SiteShoter\SiteShoter.exe");
            AnalystCon = Path.Combine(HttpRuntime.AppDomainAppPath, @"AnalystCon\AnalystCon.exe");

            CashSeconds = Convert.ToInt32(WebConfigurationManager.AppSettings[nameof(CashSeconds)]);
            WaitUpdateSeconds = Convert.ToInt32(WebConfigurationManager.AppSettings[nameof(WaitUpdateSeconds)]);
            MaxItemsCount = Convert.ToInt32(WebConfigurationManager.AppSettings[nameof(MaxItemsCount)]);

            InternetSearchHost = WebConfigurationManager.AppSettings[nameof(InternetSearchHost)];
        }

        public static string ElangPath { get; }
        public static string CsvPath { get; }
        public static string UrlPath { get; }
        public static string UrlStatePath { get; }

        public static string Screenshots { get; }
        public static string ScreenshotPath { get; }
        public static string ScreenshotterArgs { get; }
        public static string ScreenshotExtension { get; }
        public static string ExternalToken { get; }

        public static string ProductParser { get; }
        public static string Screenshotter { get; }
        public static string AnalystCon { get; }
        public static string InternetSearchResultPath { get; set; }

        public static int CashSeconds { get; set; }
        public static int WaitUpdateSeconds { get; set; }
        public static int MaxItemsCount { get; set; }

        public static string InternetSearchHost { get; }
    }
}