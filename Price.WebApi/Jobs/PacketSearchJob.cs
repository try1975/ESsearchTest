using System;
using System.Diagnostics;
using System.Linq;
using Common.Dto;
using Common.Dto.Logic;
using Common.Dto.Model.Packet;
using Price.WebApi.Logic;
using Price.WebApi.Logic.Packet;
using Quartz;

namespace Price.WebApi.Jobs
{
    public class PacketSearchJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            #region MyRegion

            //var key = context.JobDetail.Key;
            //var dataMap = context.JobDetail.JobDataMap;
            //var jobSays = dataMap.GetString("id");
            //var msgString = $"Instance {key} of DumbJob says: {jobSays}";
            //Debug.WriteLine(msgString);
            //Console.Error.WriteLine(msgString);

            #endregion

            Debug.WriteLine($"Job start---{DateTime.Now}", $"{nameof(PacketSearchJob)}");

            var cnt = SearchItemStore.Dictionary.Values.Count(z => z.Status == TaskStatus.InProcess) - 4;
            if (cnt < 0)
            {
                cnt = -1 * cnt;
            }
            else
            {
                cnt = 0;
            }

            var searchItemDtos = SearchItemStore.Dictionary.Values.Where(z => z.Status == TaskStatus.InQueue)
                .Take(cnt)
                .ToList();
            foreach (var searchItemDto in searchItemDtos)
            {
                searchItemDto.BeginProcess(Utils.GetUtcNow());
                Debug.WriteLine($"Mark {searchItemDto.Id}", $"{nameof(PacketSearchJob)}");
            }
            foreach (var searchItemDto in searchItemDtos)
            {
                Debug.WriteLine($"Search {searchItemDto.Id}", $"{nameof(PacketSearchJob)}");
                PacketItemSeacher.Search(searchItemDto.SearchItem, searchItemDto);
            }

            var tasks = SearchPacketTaskStore.Dictionary.Values.Where(z => z.ProcessedAt == null).ToList();
            foreach (var searchPacketTaskDto in tasks)
            {
                searchPacketTaskDto.UpdateStatistics(AppGlobal.WaitUpdateSeconds);
            }
        }
    }
}