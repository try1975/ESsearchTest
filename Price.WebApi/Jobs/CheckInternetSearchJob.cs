﻿using System;
using System.Linq;
using Common.Dto.Logic;
using Price.Db.Postgress;
using Price.Db.Postgress.QueryProcessors;
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
                if ((entity.StartProcessed + AppGlobal.InternetSearchTimeoutSeconds ) < Utils.GetUtcNow())
                {
                    entity.ProcessedAt = Utils.GetUtcNow();
                    entity.Status = TaskStatus.BreakByTimeout;
                    query.UpdateEntity(entity);
                    AnalistService.TerminateSession(entity.InternetSessionId);
                    continue;
                }

                if (!string.IsNullOrEmpty(entity.InternetSessionId) && AnalistService.IsInternetSessionCompleted(entity.InternetSessionId))
                {
                    entity.ProcessedAt = Utils.GetUtcNow();
                    entity.Status = TaskStatus.Ok;
                    query.UpdateEntity(entity);
                }
            }
        }
    }
}