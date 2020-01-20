using Common.Dto.Model;
using System;
using System.Linq;

namespace Monitoring
{
    public static class MonitoringScheduleChecker
    {
        public static async void Execute()
        {
            var dataManager = new MonitoringScheduleDataManager();
            var isRepeate = true;
            while (isRepeate)
            {
                isRepeate = false;
                var dtos = await dataManager.GetMonitoringSchedules();
                var monitoringScheduleDtos = dtos as MonitoringScheduleDto[] ?? dtos.ToArray();
                Console.WriteLine($"Standing orders count: {monitoringScheduleDtos.Length}");
                foreach (var dto in monitoringScheduleDtos)
                {
                    if (!dto.IsActive)
                    {
                        Console.WriteLine("Skip Inactive...");
                        continue;
                    }
                    if (dto.LastDate.HasValue && dto.LastDate.Value.Date <= DateTime.UtcNow.Date)
                    {
                        Console.WriteLine($"Skip by LastDate {dto.LastDate.Value.Date}...");
                        continue;
                    }
                    var nextRequestDate = dto.NextRequestDate?.Date ?? dto.FirstDate.Date;
                    if (nextRequestDate.Date > DateTime.UtcNow.Date.AddDays(1))
                    {
                        Console.WriteLine($"Skip by NextDate {nextRequestDate.Date}...");
                        continue;
                    }
                    // отправить запрос на создание задания
                    Console.WriteLine($"Create request {nextRequestDate.Date} {dto.Id}...");
                    var result = dataManager.CreateRequest(dto.Id).Result;
                    if (result.Equals(Guid.Empty)) continue;
                    isRepeate = true;
                    break;
                }
            }
            Console.WriteLine("Completed...");
        }
    }
}
