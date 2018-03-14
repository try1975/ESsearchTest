using System;
using System.Linq;
using System.Net;
using System.Text;
using Common.Dto.Logic;
using Newtonsoft.Json;
using Price.Db.MysSql;
using Price.Db.MysSql.QueryProcessors;
using Price.WebApi.Logic;
using Quartz;
using TaskStatus = PriceCommon.Enums.TaskStatus;

namespace Price.WebApi.Jobs
{
    /// <summary>
    /// 
    /// </summary>
    public class CheckInternetSearchJob : IJob
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            Logger.Log.Info($"{nameof(CheckInternetSearchJob)} - {DateTime.Now:HH:mm:ss}");
            var query = new SearchItemQuery(new PriceContext());
            var entities = query.GetEntities().Where(z => z.Status == TaskStatus.InProcess && z.Source.Contains("internet")).ToList();
            foreach (var entity in entities)
            {
                // Stop long tasks
                // TODO: call internet search service stop task
                if (entity.StartProcessed + (AppGlobal.CashSeconds/2) < Utils.GetUtcNow())
                {
                    entity.ProcessedAt = Utils.GetUtcNow();
                    entity.Status = TaskStatus.BreakByTimeout;
                    query.UpdateEntity(entity);
                    continue;
                }

                if (!string.IsNullOrEmpty(entity.InternetSessionId) && IsInternetSessionCompleted(entity.InternetSessionId))
                {
                    entity.ProcessedAt = Utils.GetUtcNow();
                    entity.Status = TaskStatus.Ok;
                    query.UpdateEntity(entity);
                }
            }
        }

        private static bool IsInternetSessionCompleted(string sessionId)
        {
            using (var client = new WebClient())
            {
                var en = Encoding.UTF8;
                var uri = new Uri($"{AppGlobal.InternetSearchHost}/GetSessionProgress/{sessionId}");
                var result = client.DownloadData(uri);
                var text = en.GetString(result);
                var o = JsonConvert.DeserializeObject<AnalystSessionProgress>(text);
                return o.result[0].percent == "100";
            }
        }
    }



    public class AnalystSessionProgress
    {
        public AnalystSessionProgressResult[] result { get; set; }
    }

    public class AnalystSessionProgressResult
    {
        public string percent { get; set; }
    }


}