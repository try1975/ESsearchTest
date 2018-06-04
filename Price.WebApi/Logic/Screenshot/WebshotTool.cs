using System;

namespace Price.WebApi.Logic.Screenshot
{
    public static class WebshotTool
    {
        public static string GetWebshotName(int id, Uri uri)
        {
            var webshotName = $"{id}_{uri.GetHashCode()}.{AppGlobal.ScreenshotExtension}";
            return webshotName;
        }
        public static string GetWebshotName(int id, string uri)
        {
            var webshotName = $"{id}_{new Uri(uri).GetHashCode()}.{AppGlobal.ScreenshotExtension}";
            return webshotName;
        }
    }
}