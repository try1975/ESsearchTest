using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Price.WebApi.GetFiles;
using Price.WebApi.Logic.Interfaces;
using Price.WebApi.Models.UpdatePrice;
using PriceCommon.Utils;

namespace Price.WebApi.Logic.UpdatePrice
{
    public class UpdatePriceWatcher : IUpdatePriceWatcher
    {
        private static readonly Regex RegexObj;

        static UpdatePriceWatcher()
        {
            RegexObj = new Regex(@"_([0-9]+)_([0-9,A-z]+)\.txt",
                RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        }

        public UpdatePriceWatcher()
        {
            var watcher = new FileSystemWatcher
            {
                Path = AppGlobal.UrlStatePath,
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                               | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                Filter = "*.txt"
            };
            watcher.Changed += OnChanged;
            watcher.EnableRaisingEvents = true;
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed) return;
            try
            {
                if (string.IsNullOrEmpty(e.FullPath) || !RegexObj.IsMatch(e.FullPath)) return;

                var taskId =
                    $"{RegexObj.Match(e.FullPath).Groups[1].Value}_{RegexObj.Match(e.FullPath).Groups[2].Value}";
                var taskDto = UpdatePriceTaskStore.Get(taskId);
                if (taskDto == null) return;

                var key = e.FullPath.GetHashCode();
                var readedCount = 0;
                if (StateFiles.Dictionary.ContainsKey(key))
                {
                    readedCount = StateFiles.Dictionary[key];
                }
                else
                {
                    StateFiles.Dictionary[key] = readedCount;
                }

                var allLines = Utils.ReadLines(e.FullPath).ToArray();
                var newLines = readedCount == 0
                    ? allLines
                    : allLines.Skip(readedCount).Take(allLines.Length - readedCount);
                StateFiles.Dictionary[key] = allLines.Length;
                var processedAt = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                foreach (var newLine in newLines)
                {
                    Debug.WriteLine(newLine);
                    var uri = newLine.Substring(2, newLine.Length - 2);
                    var updatePriceDto = UpdatePriceStore.Get(new Uri(uri));
                    updatePriceDto.ProcessedAt = processedAt;
                    updatePriceDto.Status = newLine[0].Equals('1') ? UpdatePriceStatus.Ok : UpdatePriceStatus.Error;
                    // сделать скриншот
                    if (updatePriceDto.Status == UpdatePriceStatus.Ok && taskDto.CreateScreenshots)
                    {
                        var screenshotLink = $"{taskDto.BaseUri}/GetFiles/{nameof(GetFile)}.ashx?id={updatePriceDto.Uri.GetHashCode()}";
                        updatePriceDto.ScreenshotLink = new Uri(screenshotLink);
                        Screenshotter.Run(updatePriceDto.Uri);
                    }
                }
                taskDto.UpdateStatistics();
            }
            catch (ArgumentException exception)
            {
                Logger.Log.Error($"{exception}");
            }
        }
    }
}