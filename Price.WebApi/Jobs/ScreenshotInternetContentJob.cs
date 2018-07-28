using System;
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
    public class ScreenshotInternetContentJob : IJob
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            var query = new InternetContentQuery(new PriceContext());
            var entities = query
                .GetEntities()
                .Where(z => string.IsNullOrEmpty(z.contact_url))
                .OrderBy(z => z.dt)
                .Take(10)
                .ToList();
            foreach (var entity in entities)
            {
                try
                {
                    var uri = new Uri(entity.url);
                    entity.contact_url = WebshotTool.GetWebshotName(entity.Id, uri);
                    query.UpdateEntity(entity);
                    var filename = Path.Combine(AppGlobal.ScreenshotPath, $"{entity.contact_url}");
                    if (File.Exists(filename)) continue;
                    if (entity.screenshot != null && entity.screenshot.Length > 3)
                    {
                        File.WriteAllBytes(filename, entity.screenshot);
                    }
                    else
                    {
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
                }
                catch (Exception exception)
                {
                    Logger.Log.Error(exception);
                    entity.contact_url = $"bad.{AppGlobal.ScreenshotExtension}";
                    query.UpdateEntity(entity);
                }
            }
        }
    }
}
