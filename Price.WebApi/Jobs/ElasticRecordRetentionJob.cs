using System;
using System.Collections.Generic;
using Price.WebApi.Logic;
using PriceCommon.Model;
using PricePipeCore;
using Quartz;

namespace Price.WebApi.Jobs
{
    // ReSharper disable once ClassNeverInstantiated.Global
    /// <summary>
    /// 
    /// </summary>
    public class ElasticRecordRetentionJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var indexList = new List<string> { "md_med", "md_prod" };
            foreach (var indexName in indexList)
            {
                try
                {
                    var elasticClient = ElasticClientFactory.GetElasticClient(indexName);
                    var utc = (long)DateTime.UtcNow.AddDays(-AppGlobal.ElasticRecordRetentionInDays).Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                    var response = elasticClient.DeleteByQuery<Content>(s => s
                        .Query(q => q
                            .Range(c => c
                                .Field(f => f.CollectedAt)
                                .LessThan(utc)
                               )
                        )
                    );
                    Logger.Log.Info($"Deleted old ES records: {response.Deleted}");
                }
                catch (Exception exception)
                {
                    Logger.Log.Error(exception);
                }
            }
        }
    }
}