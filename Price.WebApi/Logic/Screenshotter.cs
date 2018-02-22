using System;
using System.Diagnostics;
using System.IO;

namespace Price.WebApi.Logic
{
    public class Screenshotter
    {
        public static void RunByUri(Uri uri)
        {
            if (!File.Exists(AppGlobal.Screenshotter))
            {
                Logger.Log.Error($"Not exists {AppGlobal.Screenshotter}");
                return;
            }
            try
            {
                var filename = Path.Combine(AppGlobal.ScreenshotPath, $"{uri.GetHashCode()}.png");
                var arguments = $"/URL {uri.AbsoluteUri} /Filename \"{filename}\" /OpenImageAfterSave 0 /MaxBrowserWidth 2000 /MaxBrowserHeight 20000 /BrowserTimeout 2000";
                Logger.Log.Info($"{AppGlobal.Screenshotter}");
                Logger.Log.Info($"{arguments}");
                if(!File.Exists(filename)) Process.Start(AppGlobal.Screenshotter, arguments);
            }
            catch (Exception exception)
            {
                Logger.Log.Error($"{exception}");
            }
        }
    }
}