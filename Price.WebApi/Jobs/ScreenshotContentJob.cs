﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Price.Db.Postgress;
using Price.Db.Postgress.QueryProcessors;
using Price.WebApi.Logic;
using Price.WebApi.Logic.Screenshot;
using Quartz;

namespace Price.WebApi.Jobs
{
    public class ScreenshotContentJob : IJob
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            var query = new ContentQuery(new PriceContext());
            var entities = query
                .GetEntities()
                .Where(z => string.IsNullOrEmpty(z.Screenshot))
                .OrderBy(z => z.CollectedAt)
                .Take(10)
                .ToList();
            foreach (var entity in entities)
            {
                try
                {
                    var uri = new Uri(entity.Uri);
                    entity.Screenshot = WebshotTool.GetWebshotName(entity.Id, uri);
                    query.UpdateEntity(entity);
                    if (uri.Host.ToLower() == "zakupki.gov.ru") continue;
                    var filename = Path.Combine(AppGlobal.ScreenshotPath, $"{entity.Screenshot}");
                    if (File.Exists(filename)) continue;
                    string arguments;
                    if (AppGlobal.Screenshotter.Contains("SiteShoter.exe"))
                    {
                        arguments = $"/URL {uri} /Filename \"{filename}\" {AppGlobal.ScreenshotterArgs}";
                    }
                    else
                    {
                        arguments = $"{uri} \"{filename}\" {AppGlobal.ScreenshotterArgs}";
                    }
                    Process.Start(AppGlobal.Screenshotter, arguments)?.WaitForExit();
                }
                catch (Exception exception)
                {
                    Logger.Log.Error(exception);
                }
            }

        }
    }
}