using Quartz;
using Quartz.Impl;

namespace Price.WebApi.Jobs
{
    public class PacketSearchScheduler
    {
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
                //.WithIdentity(id, "group1")     // идентифицируем триггер с именем и группой
                .WithIdentity("trigger02", "group1")     // идентифицируем триггер с именем и группой
                .StartNow()                            // запуск сразу после начала выполнения
                .WithSimpleSchedule(x => x
                    //.WithRepeatCount(1))            // настраиваем выполнение действия
                    .WithIntervalInSeconds(100)          // через 2 секунды
                    .RepeatForever())                   // бесконечное повторение
                .Build();
            //scheduler.ScheduleJob(checkInternetSearchJob, checkInternetSearchJobTrigger); // начинаем выполнение работы
        }
    }
}