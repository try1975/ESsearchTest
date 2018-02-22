using System;
using Common.Dto.Logic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PriceCommon.Enums;

namespace Common.Dto.Model.NewApi
{
    public class SearchItemHeaderDto : IDto<string>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public long? StartProcessed { get; set; }
        public long? LastUpdate { get; set; }
        public long? ProcessedAt { get; set; }

        /// <summary>
        /// Статус объекта обработки
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public TaskStatus Status { get; set; }

        public string Source { get; set; }
        public string ExtId { get; set; }

        public string Normalizer { get; set; }

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

        [JsonIgnore]
        public string StatusString => PriceCommon.Utils.Utils.GetDescription(Status);

        public int ContentCount { get; set; }

        public void BeginProcess(long tick)
        {
            StartProcessed = tick;
            LastUpdate = tick;
            ProcessedAt = null;
            Status = TaskStatus.InProcess;
        }

    }
}