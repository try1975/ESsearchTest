using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PriceCommon.Enums;

namespace Common.Dto.Model.NewApi
{
    public class SearchItemExtDto : IDto<string>
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
        //[JsonIgnore]
        public string InternetSessionId { get; set; }

        public string Normalizer { get; set; }

        //[JsonIgnore]
        public string JsonText { get; set; }
        //public int ContentCount
        //{
        //    get
        //    {
        //        var cnt = 0;
        //        if (Contents != null) cnt = Contents.Count;
        //        return cnt;
        //    }
        //}
        public List<ContentExtDto> Contents { get; set; }

        public void BeginProcess(long tick)
        {
            StartProcessed = tick;
            LastUpdate = tick;
            ProcessedAt = null;
            Status = TaskStatus.InProcess;
        }

    }
}
