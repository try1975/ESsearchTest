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
                var list = new List<SearchItemParam> { searchItem };
                File.WriteAllText(inpFileFullPath, JsonConvert.SerializeObject(list));
                // save searchItem to json file
                var outFileFullPath = Path.Combine(AppGlobal.InternetSearchResultPath, $"_{searchItemDto.Key}.csv");
                // check if file exists and not old
                var dedlineTime = DateTime.Now.AddSeconds(-1000);
                if (File.Exists(outFileFullPath))
                {
                    if (File.GetLastWriteTime(outFileFullPath) >= dedlineTime)
                    {
                        var listContentDto = File.ReadAllLines(outFileFullPath, Encoding.Default)
                            .Select(ContentDto.FromCsv)
                            .ToList();
                        if (searchItemDto.Content == null)
                        {
                            searchItemDto.Content = listContentDto;
                        }
                        else
                        {
                            searchItemDto.Content = searchItemDto.Content.Where(z => z.PriceType == PriceType.Trusted);
                            searchItemDto.Content = searchItemDto.Content.Concat(listContentDto);
                        }
                        //searchItemDto.ProcessedAt = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                        //searchItemDto.Status = TaskStatus.Ok;
                        Logger.Log.Info($"{AppGlobal.AnalystCon} do not start");
                        return;
                    }
                    //else
                    //{
                    //    File.Delete(outFileFullPath);
                    //}
                }
                var arguments = $"-inp:\"{inpFileFullPath}\" -out:\"{outFileFullPath}\" -debug_log";
                Logger.Log.Info($"{AppGlobal.AnalystCon}");
                Logger.Log.Info($"{arguments}");
                Process.Start(AppGlobal.AnalystCon, arguments);
                Logger.Log.Info("Process.Start called.");
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