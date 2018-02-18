using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Common.Dto.Model;
using Common.Dto.Model.Packet;
using Newtonsoft.Json;
using PriceCommon.Enums;

namespace Price.WebApi.Logic.Internet
{
    public class InternetSearcher
    {
        public static void Search(SearchItemParam searchItem, SearchItemDto searchItemDto)
        {
            // Run AnalystCon

            if (!File.Exists(AppGlobal.AnalystCon))
            {
                Logger.Log.Error($"Not exists {AppGlobal.AnalystCon}");
                return;
            }
            try
            {
                var inpFileFullPath = Path.Combine(AppGlobal.InternetSearchResultPath, $"_{searchItemDto.Key}.json");
                var list = new List<SearchItemParam> { searchItem };
                File.WriteAllText(inpFileFullPath, JsonConvert.SerializeObject(list));
                // save searchItem to json file
                var outFileFullPath = Path.Combine(AppGlobal.InternetSearchResultPath, $"_{searchItemDto.Key}.csv");
                var logFileFullPath = Path.Combine(AppGlobal.InternetSearchResultPath, $"_{searchItemDto.Key}.log");
                // check if file exists and not old
                if (File.Exists(outFileFullPath))
                {
                    var dedlineTime = DateTime.Now.AddSeconds(-AppGlobal.WaitUpdateSeconds);
                    if (File.GetLastWriteTime(outFileFullPath) >= dedlineTime)
                    {
                        var listContentDto = File.ReadAllLines(outFileFullPath, Encoding.Default)
                            .Select(ContentDto.FromAnalystCsv)
                            .ToList();
                        if (searchItemDto.Content == null)
                        {
                            searchItemDto.SetContent(listContentDto);
                        }
                        else
                        {
                            searchItemDto.SetContent(searchItemDto.Content.Where(z => z.PriceType != PriceType.Check));
                            searchItemDto.SetContent(searchItemDto.Content.Concat(listContentDto));
                        }
                        Logger.Log.Info($"{AppGlobal.AnalystCon} do not start");
                        return;
                    }
                    try
                    {
                        File.Delete(outFileFullPath);
                    }
                    catch (Exception exception)
                    {
                        Debug.WriteLine(exception);
                        //throw;
                    }
                }
                var arguments = $"-inp:\"{inpFileFullPath}\" -out:\"{outFileFullPath}\"";// > \"{logFileFullPath}\"";
                Logger.Log.Info($"{AppGlobal.AnalystCon}");
                Logger.Log.Info($"{arguments}");
                Debug.WriteLine($"{arguments}");

                var analystProcess = new Process
                {
                    StartInfo =
                    {
                        FileName = AppGlobal.AnalystCon,
                        Arguments = arguments,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
                        StandardOutputEncoding = Encoding.GetEncoding(866)
            }
                };
                analystProcess.OutputDataReceived += AnaystOutputHandler;
                analystProcess.Start();
                analystProcess.BeginOutputReadLine();
                searchItemDto.AnalystProcessId = analystProcess.Id;
                //Process.Start(AppGlobal.AnalystCon, arguments);
                Logger.Log.Info("Process.Start called.");

            }
            catch (Exception exception)
            {
                Logger.Log.Error($"{exception}");
            }
            // in File Watcher  - put result in searchItemDto.Content
            // in job trigger - Mark searchItemDto.Status and searchItemDto.ProcessedAt when result file not change 5 minutes or over 60 links in result
        }

        private static void AnaystOutputHandler(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Data)) return;

            //var data = Encoding.UTF8.GetString(Encoding.GetEncoding(866).GetBytes(e.Data));
            //var data = Encoding.GetEncoding(866).GetBytes(e.Data);
            //var utf8String = Encoding.UTF8.GetString(Encoding.Convert(Encoding.GetEncoding(866), Encoding.UTF8, data));
            var logFileFullPath = Path.Combine(AppGlobal.InternetSearchResultPath, $"AnalystCon_{DateTime.Today:yyyy-MM-dd}_.log");
            File.AppendAllLines(logFileFullPath, new[] { e.Data }, Encoding.GetEncoding(866));
        }
    }
}