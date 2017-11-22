using System.Collections.Generic;
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
        public long? ProcessedAt { get; set; }

        /// <summary>
        /// Статус объекта обработки
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public TaskStatus Status { get; set; }
        [JsonIgnore]
        public SearchItemParam SearchItem { get; set; }
        public IEnumerable<ContentDto> Content { get; set; }
    }
}