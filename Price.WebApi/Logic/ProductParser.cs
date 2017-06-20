using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Price.WebApi.Logic
{
    public static class ProductParser
    {
        private static void Run(string elangPath, string sourceName, string urlPath, string urlStatePath,
            string csvPath)
        {
            if (!File.Exists(AppGlobal.ProductParser))
            {
                Logger.Log.Error($"Not exists {AppGlobal.ProductParser}");
                return;
            }
            try
            {
                var arguments = $"-elang:\"{elangPath}\" -nm:\"{sourceName}\" -urllist:\"{urlPath}\" -urlstate:\"{urlStatePath}\" -out:\"{csvPath}\" -utf8";
                Logger.Log.Info($"{AppGlobal.ProductParser}");
                Logger.Log.Info($"{arguments}");
                Process.Start(AppGlobal.ProductParser, arguments);
            }
            catch (Exception exception)
            {
                Logger.Log.Error($"{exception}");
            }
        }

        public static void PrepareAndRun(string host, string taskId, IEnumerable<string> uriList, string elangPath,
            string sourceName)
        {
            var partialFileName = PathService.GetFileNameFromHostName(host);
            var csvPath = PathService.GetCsvPath(partialFileName, taskId);
            var urlPath = PathService.GetUrlPath(partialFileName, taskId);
            var urlStatePath = PathService.GetUrlStatePath(partialFileName, taskId);
            File.WriteAllLines(urlPath, uriList);
            Run(elangPath, sourceName, urlPath, urlStatePath, csvPath);
        }
    }
}