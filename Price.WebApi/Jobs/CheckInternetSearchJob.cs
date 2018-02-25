using System;
using System.Linq;
using System.Net;
using System.Text;
using Common.Dto.Logic;
using Newtonsoft.Json;
using Price.Db.MysSql;
using Price.Db.MysSql.QueryProcessors;
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
            var query = new SearchItemQuery(new PriceContext());
            var entities = query.GetEntities().Where(z => z.Status == TaskStatus.InProcess && z.Source.Contains("internet")).ToList();
            foreach (var entity in entities)
            {
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
                var uri = new Uri($"http://localhost:8080/datasnap/rest/TAnalyst_Service/GetSessionProgress/{sessionId}");
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