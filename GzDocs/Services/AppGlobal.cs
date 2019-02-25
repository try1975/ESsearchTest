using System.Web.Configuration;

namespace GzDocs.Services
{
    public static class AppGlobal
    {
        static AppGlobal()
        {
            CachMinutes = int.TryParse(WebConfigurationManager.AppSettings[nameof(CachMinutes)], out var i) ? i : 15;
            GzFilesPathPrefix = WebConfigurationManager.AppSettings[nameof(GzFilesPathPrefix)];
        }

        /// <summary>
        /// </summary>
        public static int CachMinutes { get; }

        public static string GzFilesPathPrefix { get; }
    }
}