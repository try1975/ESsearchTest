using System;
using System.Linq;
using Price.Db.MysSql;
using Price.Db.MysSql.QueryProcessors;
using Price.WebApi.Logic;
using Price.WebApi.Maintenance.Classes;
using PriceCommon.Enums;
using Quartz;

namespace Price.WebApi.Jobs
{
    /// <summary>
    /// Delete not checked old search results - retention policy
    /// </summary>
    public class SearchResultRetentionJob : IJob
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            var moment = DateTime.UtcNow.AddDays(-AppGlobal.SearchResultRetentionInDays);
            var momentUtc = (long)moment.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            using (var priceContext = new PriceContext())
            {
                DeleteContent(momentUtc, priceContext);
                DeleteInternetContent(moment, priceContext);
            }
        }

        private static void DeleteContent(long momentUtc, PriceContext priceContext)
        {
            var query = new ContentQuery(priceContext);
            var entities = query
                .GetEntities()
                .Where(z => z.CollectedAt < momentUtc && z.PriceStatus == PriceStatus.NotChecked)
                .ToList();
            var api = new ContentApi(query);
            foreach (var entity in entities)
            {
                try
                {
                    api.RemoveItem(entity.Id);
                }
                catch (Exception exception)
                {
                    Logger.Log.Error(exception);
                }
            }
        }

        private static void DeleteInternetContent(DateTime moment, PriceContext priceContext)
        {
            var query = new InternetContentQuery(priceContext);
            var entities = query
                .GetEntities()
                .Where(z => z.dt < moment && z.PriceStatus == PriceStatus.NotChecked)
                .ToList();
            var api = new InternetContentApi(query);
            foreach (var entity in entities)
            {
                try
                {
                    api.RemoveItem(entity.Id);
                }
                catch (Exception exception)
                {
                    Logger.Log.Error(exception);
                }
            }
        }
    }
}