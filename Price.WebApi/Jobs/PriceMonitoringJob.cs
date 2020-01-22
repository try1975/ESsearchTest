using System;
using System.Linq;
using Quartz;
using Price.Db.Postgress;
using Price.Db.Postgress.QueryProcessors;
using PriceCommon.Utils;

namespace Price.WebApi.Jobs
{
    public class PriceMonitoringJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            using (var priceContext = new PriceContext())
            {
                SetScheduleNextDate(priceContext);
            }
        }

        private void SetScheduleNextDate(PriceContext priceContext)
        {
            Logger.Log.Info($"{nameof(PriceMonitoringJob)} - {DateTime.Now:HH:mm:ss}");
            var moment = DateTime.UtcNow.Date;
            var query = new ScheduleQuery(priceContext);
            var entities = query
                .GetEntities()
                .Where(z => z.IsActive && z.NextDate < moment)
                .ToList();
            foreach (var entity in entities)
            {
                entity.NextDate = Utils.LoopNextFrequencyDate(moment, entity.NextDate, entity.Frequency);
                query.UpdateEntity(entity);
            }
        }


    }
}