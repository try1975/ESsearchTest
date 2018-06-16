using System.IO;
using System.Web;
using System.Web.Configuration;

namespace Price.WebApi.Logic
{
    public static class AppGlobal
    {
        static AppGlobal()
        {
            Screenshots = WebConfigurationManager.AppSettings[nameof(Screenshots)];
            if (string.IsNullOrEmpty(Screenshots)) Screenshots = nameof(Screenshots);
            ScreenshotPath = Path.Combine(HttpRuntime.AppDomainAppPath, Screenshots);
            Directory.CreateDirectory(ScreenshotPath);
            ScreenshotterArgs = WebConfigurationManager.AppSettings[nameof(ScreenshotterArgs)];
            if (string.IsNullOrEmpty(ScreenshotterArgs)) ScreenshotterArgs = "/OpenImageAfterSave 0 /MaxBrowserWidth 2000 /MaxBrowserHeight 10000 /BrowserTimeout 2000 /JpegQuality 50 /ImageSizePerCent 75 /DisableScrollBars 1";
            ScreenshotExtension = WebConfigurationManager.AppSettings[nameof(ScreenshotExtension)];
            if (string.IsNullOrEmpty(ScreenshotExtension)) ScreenshotExtension = "jpg";

            ExternalToken = WebConfigurationManager.AppSettings[nameof(ExternalToken)];

            Screenshotter = Path.Combine(HttpRuntime.AppDomainAppPath, @"SiteShoter\SiteShoter.exe");

            int tempInt;
            CashSeconds = int.TryParse(WebConfigurationManager.AppSettings[nameof(CashSeconds)],
                out tempInt) ? tempInt : 86400;

            InternetSearchHost = WebConfigurationManager.AppSettings[nameof(InternetSearchHost)];

            
            SearchResultRetentionInDays = int.TryParse(WebConfigurationManager.AppSettings[nameof(SearchResultRetentionInDays)],
                out tempInt) ? tempInt : 30;
            SearchResultRetentionTriggerRateInHours = int.TryParse(WebConfigurationManager.AppSettings[nameof(SearchResultRetentionTriggerRateInHours)],
                out tempInt) ? tempInt : 24;

            ElasticRecordRetentionInDays = int.TryParse(WebConfigurationManager.AppSettings[nameof(ElasticRecordRetentionInDays)],
                out tempInt) ? tempInt : 30;
            ElasticRecordRetentionTriggerRateInHours = int.TryParse(WebConfigurationManager.AppSettings[nameof(ElasticRecordRetentionTriggerRateInHours)],
                out tempInt) ? tempInt : 24;
        }

        public static string Screenshots { get; }
        public static string ScreenshotPath { get; }
        public static string ScreenshotterArgs { get; }
        public static string ScreenshotExtension { get; }
        public static string ExternalToken { get; }

        public static string Screenshotter { get; }

        public static int CashSeconds { get; }

        public static string InternetSearchHost { get; }

        public static int SearchResultRetentionInDays { get; }
        public static int SearchResultRetentionTriggerRateInHours { get;}
        public static int ElasticRecordRetentionTriggerRateInHours { get;}
        public static int ElasticRecordRetentionInDays { get; }
    }
}