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

            var job = JobBuilder.Create<PacketSearchJob>()
                //.WithIdentity(id, "group1")
                //.UsingJobData(nameof(id), id)
                .Build();


            var trigger = TriggerBuilder.Create()  // создаем триггер
                //.WithIdentity(id, "group1")     // идентифицируем триггер с именем и группой
                .WithIdentity("trigger01", "group1")     // идентифицируем триггер с именем и группой
                .StartNow()                            // запуск сразу после начала выполнения
                .WithSimpleSchedule(x => x
                //.WithRepeatCount(1))            // настраиваем выполнение действия
                .WithIntervalInSeconds(2)          // через 2 секунды
                .RepeatForever())                   // бесконечное повторение
                .Build();                               // создаем триггер

            scheduler.ScheduleJob(job, trigger); // начинаем выполнение работы
        }
    }
}