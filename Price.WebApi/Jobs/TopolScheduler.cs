using Price.WebApi.Logic;
using Quartz;
using Quartz.Impl;

namespace Price.WebApi.Jobs
{
    /// <summary>
    /// 
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class TopolScheduler
    {
        /// <summary>
        /// 
        /// </summary>
        public static void Start()
        {
            //return;
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            var checkInternetSearchJob = JobBuilder.Create<CheckInternetSearchJob>().Build();
            var checkInternetSearchJobTrigger = TriggerBuilder.Create()  // создаем триггер
                .WithIdentity("trigger02", "group1")     // идентифицируем триггер с именем и группой
                .StartNow()                            // запуск сразу после начала выполнения
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(40)
                    .WithMisfireHandlingInstructionIgnoreMisfires()
                    .RepeatForever())                   // бесконечное повторение
                .Build();
            scheduler.ScheduleJob(checkInternetSearchJob, checkInternetSearchJobTrigger);

            var screenshotInternetContentJob = JobBuilder.Create<ScreenshotInternetContentJob>().Build();
            var screenshotInternetContentJobTrigger = TriggerBuilder.Create()
                .WithIdentity("trigger03", "group2")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(7)
                    .WithMisfireHandlingInstructionIgnoreMisfires()
                    .RepeatForever())
                .Build();
            scheduler.ScheduleJob(screenshotInternetContentJob, screenshotInternetContentJobTrigger);

            var screenshotContentJob = JobBuilder.Create<ScreenshotContentJob>().Build();
            var screenshotContentJobTrigger = TriggerBuilder.Create()
                .WithIdentity("trigger04", "group2")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(6)
                    .WithMisfireHandlingInstructionIgnoreMisfires()
                    .RepeatForever())
                .Build();
            scheduler.ScheduleJob(screenshotContentJob, screenshotContentJobTrigger);

            var elasticRecordRetentionJob = JobBuilder.Create<ElasticRecordRetentionJob>().Build();
            var elasticRecordRetentionTrigger = TriggerBuilder.Create()
                .WithIdentity(nameof(ElasticRecordRetentionJob), "group3")
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(AppGlobal.ElasticRecordRetentionTriggerRateInHours)
                    .WithMisfireHandlingInstructionIgnoreMisfires()
                    .RepeatForever())
                .Build();
            scheduler.ScheduleJob(elasticRecordRetentionJob, elasticRecordRetentionTrigger);

            var searchResultRetentionJob = JobBuilder.Create<SearchResultRetentionJob>().Build();
            var searchResultRetentionTrigger = TriggerBuilder.Create()
                .WithIdentity(nameof(SearchResultRetentionJob), "group3")
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(AppGlobal.SearchResultRetentionTriggerRateInHours)
                    .WithMisfireHandlingInstructionIgnoreMisfires()
                    .RepeatForever())
                .Build();
            scheduler.ScheduleJob(searchResultRetentionJob, searchResultRetentionTrigger);

        }
    }
}