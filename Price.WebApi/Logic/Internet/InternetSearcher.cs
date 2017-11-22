using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Common.Dto.Model;
using Common.Dto.Model.Packet;
using Newtonsoft.Json;

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
                var list = new List<SearchItemParam>() { searchItem };
                File.WriteAllText(inpFileFullPath, JsonConvert.SerializeObject(list));
                // save searchItem to json file
                var outFileFullPath = Path.Combine(AppGlobal.InternetSearchResultPath, $"_{searchItemDto.Key}.csv");
                // check if file exists and not old
                var dedlineTime = DateTime.Now.AddMinutes(-60);
                if (File.Exists(outFileFullPath) && File.GetLastWriteTime(outFileFullPath) >= dedlineTime)
                {
                    searchItemDto.Content = File.ReadAllLines(outFileFullPath, Encoding.Default)
                        .Select(ContentDto.FromCsv)
                        .ToList();
                    Logger.Log.Info($"{AppGlobal.AnalystCon} do not start");
                    return;
                }
                var arguments = $"-inp:\"{inpFileFullPath}\" -out:\"{outFileFullPath}\"";
                Logger.Log.Info($"{AppGlobal.AnalystCon}");
                Logger.Log.Info($"{arguments}");
                Process.Start(AppGlobal.AnalystCon, arguments);
            }
            catch (Exception exception)
            {
                Logger.Log.Error($"{exception}");
            }
            // in File Watcher  - put result in searchItemDto.Content
            // in job trigger - Mark searchItemDto.Status and searchItemDto.ProcessedAt when result file not change 5 minutes or over 60 links in result
        }
    }
}