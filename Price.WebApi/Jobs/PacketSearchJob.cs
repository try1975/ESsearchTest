using System;
using System.Diagnostics;
using System.Linq;
using Common.Dto.Model.Packet;
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

            Debug.WriteLine($"{nameof(PacketSearchJob)}---{DateTime.Now}----------------------------------------------");

            var searchItemDtos = SearchItemStore.Dictionary.Values.Where(z => z.Status == TaskStatus.InQueue).ToList();
            foreach (var searchItemDto in searchItemDtos)
            {
                searchItemDto.Status = TaskStatus.InProcess;
                Debug.WriteLine($"{searchItemDto.Id}--------------------------------------------------------------------");
                PacketItemSeacher.Search(searchItemDto.SearchItem, searchItemDto);
            }
            var tasks = SearchPacketTaskStore.Dictionary.Values.Where(z => z.ProcessedAt == null).ToList();
            foreach (var searchPacketTaskDto in tasks)
            {
                searchPacketTaskDto.UpdateStatistics();
            }
        }
    }
}