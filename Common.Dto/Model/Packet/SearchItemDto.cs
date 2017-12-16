using System;
using System.Collections.Generic;
using System.Linq;
using Common.Dto.Logic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Common.Dto.Model.Packet
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchItemDto
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public string Source { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public string Key => $"{Id}{Source}";

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        public long? StartProcessed { get; set; }
        public long? LastUpdate { get; set; }
        public long? ProcessedAt { get; set; }

        [JsonIgnore]
        public DateTime? StartProcessedDateTime
        {
            get
            {
                var unixTimeStamp = StartProcessed ?? 0;
                return Utils.UnixTimeStampToDateTime(unixTimeStamp);
            }
        }
        [JsonIgnore]
        public DateTime? LastUpdateDateTime
        {
            get
            {
                var unixTimeStamp = LastUpdate ?? 0;
                return Utils.UnixTimeStampToDateTime(unixTimeStamp);
            }
        }
        [JsonIgnore]
        public DateTime? ProcessedAtDateTime
        {
            get
            {
                var unixTimeStamp = ProcessedAt ?? 0;
                return Utils.UnixTimeStampToDateTime(unixTimeStamp);
            }
        }

        /// <summary>
        /// Статус объекта обработки
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public TaskStatus Status { get; set; }

        [JsonIgnore]
        public string StatusString => Utils.GetDescription(Status);

        [JsonIgnore]
        public SearchItemParam SearchItem { get; set; }
        public IEnumerable<ContentDto> Content { get; set; }

        public int ContentCount
        {
            get
            {
                var cnt = 0;
                if (Content != null) cnt = Content.Count();
                return cnt;
            }
        }

        //public string Okpd2 { get; set; }

        public void SetContent(IEnumerable<ContentDto> content)
        {
            Content = content;
            LastUpdate = Utils.GetUtcNow();
        }
        public void BeginProcess(long tick)
        {
            StartProcessed = tick;
            LastUpdate = tick;
            ProcessedAt = null;
            Status = TaskStatus.InProcess;
        }
        public void SuccessEndProcess(long tick)
        {
            ProcessedAt = tick;
            Status = TaskStatus.Ok;
        }
        public void ErrorEndProcess(long tick)
        {
            ProcessedAt = tick;
            Status = TaskStatus.Error;
        }

        public void SetInQueue()
        {
            if (Status == TaskStatus.InProcess) return;
            ProcessedAt = null;
            Status = TaskStatus.InQueue;
            Content = null;
        }

        public bool InCash(long cashSeconds)
        {
            var inCash = ProcessedAt != null && Utils.GetUtcNow() - ProcessedAt <= cashSeconds;
            return inCash;
        }


    }
}