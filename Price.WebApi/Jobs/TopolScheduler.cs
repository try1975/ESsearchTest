using Nest;
using Quartz;
using Quartz.Impl;

namespace Price.WebApi.Jobs
{
    /// <summary>
    /// 
    /// </summary>
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

            var packetSearchJob = JobBuilder.Create<PacketSearchJob>()
                //.WithIdentity(id, "group1")
                //.UsingJobData(nameof(id), id)
                .Build();


            var packetSearchJobTrigger = TriggerBuilder.Create()  // создаем триггер
                //.WithIdentity(id, "group1")     // идентифицируем триггер с именем и группой
                .WithIdentity("trigger01", "group1")     // идентифицируем триггер с именем и группой
                .StartNow()                            // запуск сразу после начала выполнения
                .WithSimpleSchedule(x => x
                //.WithRepeatCount(1))            // настраиваем выполнение действия
                .WithIntervalInSeconds(2)          // через 2 секунды
                .RepeatForever())                   // бесконечное повторение
                .Build();                               // создаем триггер

            //scheduler.ScheduleJob(packetSearchJob, packetSearchJobTrigger); // начинаем выполнение работы

            var checkInternetSearchJob = JobBuilder.Create<CheckInternetSearchJob>()
                .Build();

            var checkInternetSearchJobTrigger = TriggerBuilder.Create()  // создаем триггер
                .WithIdentity("trigger02", "group1")     // идентифицируем триггер с именем и группой
                .StartNow()                            // запуск сразу после начала выполнения
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(20)          
                    .RepeatForever())                   // бесконечное повторение
                .Build();
            scheduler.ScheduleJob(checkInternetSearchJob, checkInternetSearchJobTrigger); 

            var screenshotInternetContentJob = JobBuilder.Create<ScreenshotInternetContentJob>()
                .Build();

            var screenshotInternetContentJobTrigger = TriggerBuilder.Create() 
                .WithIdentity("trigger03", "group2")
                .StartNow()                         
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(7)
                    .WithMisfireHandlingInstructionIgnoreMisfires()
                    .RepeatForever())               
                .Build();
            scheduler.ScheduleJob(screenshotInternetContentJob, screenshotInternetContentJobTrigger);

            var screenshotContentJob = JobBuilder.Create<ScreenshotContentJob>()
                .Build();

            var screenshotContentJobTrigger = TriggerBuilder.Create()  
                .WithIdentity("trigger04", "group2")     
                .StartNow()                            
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(6)
                    .WithMisfireHandlingInstructionIgnoreMisfires() 
                    .RepeatForever())                  
                .Build();
            scheduler.ScheduleJob(screenshotContentJob, screenshotContentJobTrigger); 
        }
    }
}