using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Price.Db.MysSql;
using Price.Db.MysSql.QueryProcessors;
using Price.WebApi.Logic;
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
                    entity.Screenshot = $"{entity.CollectedAt}_{entity.ElasticId}.{AppGlobal.ScreenshotExtension}";
                    query.UpdateEntity(entity);
                    var filename = Path.Combine(AppGlobal.ScreenshotPath, $"{entity.Screenshot}");
                    if (File.Exists(filename)) continue;
                    var arguments = $"/URL {entity.Uri} /Filename \"{filename}\" {AppGlobal.ScreenshotterArgs}";
                    Process.Start(AppGlobal.Screenshotter, arguments)/*?.WaitForExit()*/;

                }
                catch (Exception exception)
                {
                    Logger.Log.Error(exception);
                }
            }

        }
    }
}