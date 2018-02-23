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
                entity.contact_url = $"{entity.Id}_{new Uri(entity.url).GetHashCode()}.{AppGlobal.ScreenshotExtension}";
                query.UpdateEntity(entity);
                var filename = Path.Combine(AppGlobal.ScreenshotPath, $"{entity.contact_url}");
                if (File.Exists(filename)) continue;
                var arguments = $"/URL {entity.url} /Filename \"{filename}\" {AppGlobal.ScreenshotterArgs}";
                Process.Start(AppGlobal.Screenshotter, arguments)?.WaitForExit(); ;
            }
        }
    }
}