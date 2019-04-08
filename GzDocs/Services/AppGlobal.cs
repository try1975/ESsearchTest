using System.Web.Configuration;

namespace GzDocs.Services
{
    public static class AppGlobal
    {
        static AppGlobal()
        {
            CachMinutes = int.TryParse(WebConfigurationManager.AppSettings[nameof(CachMinutes)], out var cachMinutes) ? cachMinutes : 15;
            GzFilesPathPrefix = WebConfigurationManager.AppSettings[nameof(GzFilesPathPrefix)];
            GzAttachmentsPathPrefix = WebConfigurationManager.AppSettings[nameof(GzAttachmentsPathPrefix)];
            FileStorePath = $"file:{GzAttachmentsPathPrefix}";
            GzMaxDocCount = int.TryParse(WebConfigurationManager.AppSettings[nameof(GzMaxDocCount)], out var gzMaxDocCount) ? gzMaxDocCount : 20;
        }

        /// <summary>
        /// </summary>
        public static int CachMinutes { get; }

        public static string GzFilesPathPrefix { get; }
        public static string GzAttachmentsPathPrefix { get; }
        public static string FileStorePath { get; }
        public static int GzMaxDocCount { get; }
    }
}